#region File Description
//-----------------------------------------------------------------------------
// ContentBuilder.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Build.BuildEngine;
#endregion

namespace WinFormsContentLoading
{
    /// <summary>
    /// このクラスは、実行時に動的に XNA Framework コンテンツをビルドするために必要な 
    /// MSBuild 機能をラップします。メモリー内に一時 MSBuild プロジェクトを作成し、
    /// 任意で選択したコンテンツ ファイルをこのプロジェクトに追加します。
    /// 続いてプロジェクトをビルドします。これにより一時ディレクトリにコンパイル済みの .xnb 
    /// コンテンツ ファイルが作成されます。ビルドが終了した後、一般的な ContentManager 
    /// を使用して、通常どおりにこれらの一時 .xnb ファイルを読み込めます。
    /// </summary>
    class ContentBuilder : IDisposable
    {
        #region Fields


        // どのインポーターまたはプロセッサを読み込みますか。
        const string xnaVersion = ", Version=3.0.0.0, PublicKeyToken=6d5c3888ef60e27d";

        static string[] pipelineAssemblies =
        {
            "Microsoft.Xna.Framework.Content.Pipeline.FBXImporter" + xnaVersion,
            "Microsoft.Xna.Framework.Content.Pipeline.XImporter" + xnaVersion,
            "Microsoft.Xna.Framework.Content.Pipeline.TextureImporter" + xnaVersion,
            "Microsoft.Xna.Framework.Content.Pipeline.EffectImporter" + xnaVersion,
        };


        // 動的にコンテンツをビルドするために使用される MSBuild オブジェクト。
        Engine msBuildEngine;
        Project msBuildProject;
        ErrorLogger errorLogger;


        // コンテンツ ビルドで使用される一時ディレクトリ。
        string buildDirectory;
        string processDirectory;
        string baseDirectory;


        // 複数の ContentBuilder がある場合に、一意のディレクトリ名を生成します。
        static int directorySalt;


        // 処理が終わりましたか。
        bool isDisposed;


        #endregion

        #region Properties


        /// <summary>
        /// 出力ディレクトリを取得します。ここには、生成された .xnb ファイルが含まれます。
        /// </summary>
        public string OutputDirectory
        {
            get { return Path.Combine(buildDirectory, "bin/Content"); }
        }


        #endregion

        #region Initialization


        /// <summary>
        /// 新しい ContentBuilder のコンストラクター
        /// </summary>
        public ContentBuilder()
        {
            CreateTempDirectory();
            CreateBuildProject();
        }


        /// <summary>
        /// ContentBuilder のデコンストラクター
        /// </summary>
        ~ContentBuilder()
        {
            Dispose(false);
        }


        /// <summary>
        /// 必要がなくなったときに、ContentBuilder を破棄します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// 標準の .NET IDisposable パターンを実装します。
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                isDisposed = true;

                DeleteTempDirectory();
            }
        }


        #endregion

        #region MSBuild


        /// <summary>
        /// 一時 MSBuild コンテンツ プロジェクトをメモリー内に作成します。
        /// </summary>
        void CreateBuildProject()
        {
            string projectPath = Path.Combine(buildDirectory, "content.contentproj");
            string outputPath = Path.Combine(buildDirectory, "bin");

            // ビルド エンジンを作成します。
            msBuildEngine = new Engine(RuntimeEnvironment.GetRuntimeDirectory());

            // カスタム エラー ロガーを登録します。
            errorLogger = new ErrorLogger();

            msBuildEngine.RegisterLogger(errorLogger);

            // ビルド プロジェクトを作成します。
            msBuildProject = new Project(msBuildEngine);

            msBuildProject.FullFileName = projectPath;

            msBuildProject.SetProperty("XnaPlatform", "Windows");
            msBuildProject.SetProperty("XnaFrameworkVersion", "v2.0");
            msBuildProject.SetProperty("Configuration", "Release");
            msBuildProject.SetProperty("OutputPath", outputPath);

            // カスタム インポーターまたはプロセッサを登録します。
            foreach (string pipelineAssembly in pipelineAssemblies)
            {
                msBuildProject.AddNewItem("Reference", pipelineAssembly);
            }

            // XNA Framework コンテンツのビルド方法を定義する標準ターゲット ファイルを含めます。
            msBuildProject.AddNewImport("$(MSBuildExtensionsPath)\\Microsoft\\XNA " +
                                        "Game Studio\\v3.0\\Microsoft.Xna.GameStudio" +
                                        ".ContentPipeline.targets", null);
        }


        /// <summary>
        /// 新しいコンテンツ ファイルを MSBuild プロジェクトに追加します。インポーターおよび
        /// プロセッサは省略可能です。インポーターを null のままにすると、インポーターは
        /// ファイル拡張子に基づいて自動的に検出されます。プロセッサを null のままにすると、
        /// データは処理されずにパス スルーされます。
        /// </summary>
        public void Add(string filename, string name, string importer, string processor)
        {
            BuildItem buildItem = msBuildProject.AddNewItem("Compile", filename);

            buildItem.SetMetadata("Link", Path.GetFileName(filename));
            buildItem.SetMetadata("Name", name);

            if (!string.IsNullOrEmpty(importer))
                buildItem.SetMetadata("Importer", importer);

            if (!string.IsNullOrEmpty(processor))
                buildItem.SetMetadata("Processor", processor);
        }


        /// <summary>
        /// すべてのコンテンツ ファイルを MSBuild プロジェクトから削除します。
        /// </summary>
        public void Clear()
        {
            msBuildProject.RemoveItemsByName("Compile");
        }


        /// <summary>
        /// プロジェクトに追加したすべてのコンテンツ ファイルをビルドし、
        /// OutputDirectory に .xnb ファイルを動的に生成します。
        /// ビルドが失敗した場合、エラー メッセージを返します。
        /// </summary>
        public string Build()
        {
            // 以前の記録したエラーをすべてクリアします。
            errorLogger.Errors.Clear();

            // プロジェクトをビルドします。
            if (!msBuildProject.Build())
            {
                // ビルドが失敗した場合、エラー文字列を返します。
                return string.Join("\n", errorLogger.Errors.ToArray());
            }

            return null;
        }


        #endregion

        #region Temp Directories


        /// <summary>
        /// コンテンツをビルドする一時ディレクトリを作成します。
        /// </summary>
        void CreateTempDirectory()
        {
            // ディレクトリの名前の基底は次のとおりです。
            //
            //  %temp%\WinFormsContentLoading.ContentBuilder
            baseDirectory = Path.Combine(Path.GetTempPath(), GetType().FullName);

            // 同時に実行するプログラムのコピーが複数ある場合は、
            // 次のようにプロセス ID を含めます。
            //
            //  %temp%\WinFormsContentLoading.ContentBuilder\<ProcessId>
            int processId = Process.GetCurrentProcess().Id;
            processDirectory = Path.Combine(baseDirectory, processId.ToString());

            // プログラムで複数の ContentBuilder インスタンスが作成される場合は、
            // 次のようにソルト値を含めます。
            //
            //  %temp%\WinFormsContentLoading.ContentBuilder\<ProcessId>\<Salt>
            directorySalt++;
            buildDirectory = Path.Combine(processDirectory, directorySalt.ToString());

            // 一時ディレクトリを作成します。
            Directory.CreateDirectory(buildDirectory);

            PurgeStaleTempDirectories();
        }


        /// <summary>
        /// 一時ディレクトリが必要なくなった場合、それを削除します。
        /// </summary>
        void DeleteTempDirectory()
        {
            Directory.Delete(buildDirectory, true);

            // 各自の一時ディレクトリをまだ使用している ContentBuilder のインスタンスが
            // ほかにない場合は、プロセス ディレクトリも削除できます。
            if (Directory.GetDirectories(processDirectory).Length == 0)
            {
                Directory.Delete(processDirectory);

                // 各自の一時ディレクトリをまだ使用しているプログラムのコピーが
                // ほかにない場合は、基本ディレクトリも削除できます。
                if (Directory.GetDirectories(baseDirectory).Length == 0)
                {
                    Directory.Delete(baseDirectory);
                }
            }
        }


        /// <summary>
        /// 使用する必要がなくなったときに一時ディレクトリを削除できることが、理想的です。
        /// DeleteTempDirectory メソッド (Dispose またはデコンストラクターのうち最初に発生する
        /// ものによって呼び出されます) がまさしくこれを行います。問題は、これらのクリーンアップ 
        /// メソッドがまったく実行されない場合があることです。たとえば、プログラムが
        /// クラッシュしたり、デバッガーで停止された場合、削除を行う機会が得られません。
        /// 次回に起動すると、このメソッドは、以前の実行で正しくシャットダウンできなかったために
        /// 残された一時ディレクトリをすべて調べます。これにより、これら孤立したディレクトリは、
        /// 永久に散乱したまま残されることがなくなります。
        /// </summary>
        void PurgeStaleTempDirectories()
        {
            // 基底の場所のサブディレクトリをすべて調べます。
            foreach (string directory in Directory.GetDirectories(baseDirectory))
            {
                // サブディレクトリ名は、作成したプロセスの ID になります。
                int processId;

                if (int.TryParse(Path.GetFileName(directory), out processId))
                {
                    try
                    {
                        // クリエーター プロセスはまだ実行していますか。
                        Process.GetProcessById(processId);
                    }
                    catch (ArgumentException)
                    {
                        // プロセスが存在しない場合、その一時ディレクトリを削除できます。
                        Directory.Delete(directory, true);
                    }
                }
            }
        }

        
        #endregion
    }
}
