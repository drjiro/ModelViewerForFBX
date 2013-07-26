#region File Description
//-----------------------------------------------------------------------------
// MainForm.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WinFormsContentLoading
{
    /// <summary>
    /// このカスタム フォームは、プログラムのメイン ユーザー インターフェイスを
    /// 提供します。このサンプルではデザイナーを使用して、[File] / [Open] 
    /// オプションを表示するメニュー バーを除くフォーム全体を ModelViewerControl で
    /// 埋めました。
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// コンテンツコンパイラ。
        /// </summary>
        private ContentBuilder contentBuilder;

        /// <summary>
        /// コンテンツ管理。XNA
        /// </summary>
        private ContentManager contentManager;

        /// <summary>
        /// エフェクトパラメータ設定フォーム。
        /// </summary>
        public ParameterForm ParameterForm
        {
            get;
            private set;
        }

        /// <summary>
        /// メイン フォームを構築します。
        /// </summary>
        public MainForm()
        {
            // コンポーネントを初期化する。
            InitializeComponent();

            // コンテンツコンパイラを作成する。
            contentBuilder = new ContentBuilder();

            // コンテンツ管理を作成する。
            contentManager = new ContentManager(modelViewerControl.Services,
                                                contentBuilder.OutputDirectory);

            /// 最初にフォームを表示するときに、自動的に [Load Model] ダイアログを表示します。
            /// +=はC#の文法。イベントの登録。
            //this.Shown += OpenMenuClicked;
        }


        /// <summary>
        /// [Exit] メニュー オプションのイベント ハンドラー。
        /// </summary>
        void ExitMenuClicked(object sender, EventArgs e)
        {
            // 終了。
            Close();
        }


        /// <summary>
        /// [Open] メニュー オプションのイベント ハンドラー。
        /// </summary>
        void OpenMenuClicked(object sender, EventArgs e)
        {
            // 「ファイルを開く」ダイアログを作成する。
            OpenFileDialog fileDialog = new OpenFileDialog();

            // 既定でコンテンツ ファイルを含むディレクトリに設定します。
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Content");
            string contentPath = Path.GetFullPath(relativePath);

            // ダイアログに設定する。
            // 最初のフォルダの位置。
            fileDialog.InitialDirectory = contentPath;

            fileDialog.Title = "モデルをロードする。";

            // フィルタの指定。表示する文字列1|フィルタ1
            fileDialog.Filter = "モデルファイル (*.fbx;*.x)|*.fbx;*.x|" +
                                "FBXファイル (*.fbx)|*.fbx|" +
                                "Xファイル (*.x)|*.x|" +
                                "すべてのファイル (*.*)|*.*";

            // ダイアログを表示する。
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                // ＯＫなら、モデルをロードする。
                LoadModel(fileDialog.FileName);
            }
        }


        /// <summary>
        /// 新しい 3D モデル ファイルを ModelViewerControl に読み込みます。
        /// </summary>
        void LoadModel(string fileName)
        {
            // 砂時計のカーソルにする。
            Cursor = Cursors.WaitCursor;

            // 既存のモデルをすべてアンロードします。
            modelViewerControl.Model = null;
            contentManager.Unload();

            // ContentBuilder にビルドする対象を指示します。
            contentBuilder.Clear();
            // "Model"は名前。"ModelProcessor"は処理するプログラム名。
            contentBuilder.Add(fileName, "xxx", null, "ModelProcessor");

            // この新しいモデル データをビルドします。
            string buildError = contentBuilder.Build();

            if (string.IsNullOrEmpty(buildError))
            {
                // ビルドが成功した場合、ContentManager を使用して、
                // 作成したばかりの一時 .xnb ファイルを読み込みます。
                modelViewerControl.Model = contentManager.Load<Model>("xxx");
            }
            else
            {
                // ビルドが失敗した場合、エラー メッセージを表示します。
                MessageBox.Show(buildError, "Error");
            }

            // 通常のカーソルにする。
            Cursor = Cursors.Arrow;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ParameterForm = new ParameterForm(this);
            ParameterForm.Initialize(modelViewerControl.DefaultEffect);
            ParameterForm.ResetParams();
            ParameterForm.Show();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        public void ShowParameters(Camera camera)
        {
            eyeXtextBox.Text = camera.Eye.X.ToString();
            eyeYtextBox.Text = camera.Eye.Y.ToString();
            eyeZtextBox.Text = camera.Eye.Z.ToString();
            eyeAtXtextBox.Text = camera.At.X.ToString();
            eyeAtYtextBox.Text = camera.At.Y.ToString();
            eyeAtZtextBox.Text = camera.At.Z.ToString();
            eyeUpXtextBox.Text = camera.Up.X.ToString();
            eyeUpYtextBox.Text = camera.Up.Y.ToString();
            eyeUpZtextBox.Text = camera.Up.Z.ToString();
            fovtextBox.Text = camera.Fov.ToString();
            aspecttextBox.Text = camera.AspectRatio.ToString();
            nearCliptextBox.Text = camera.NearClip.ToString();
            farCliptextBox.Text = camera.FarClip.ToString();
        }
    }
}
