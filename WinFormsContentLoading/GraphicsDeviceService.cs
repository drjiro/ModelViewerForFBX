#region File Description
//-----------------------------------------------------------------------------
// GraphicsDeviceService.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
#endregion

// IGraphicsDeviceService インターフェイスは、DeviceCreated イベントを必要としますが、
// 常にコンストラクター内部でデバイスを作成するだけなので、そのイベントを発生させる
// 場所がありません。C# コンパイラから、イベントが使用されていない、と警告されますが、
// ここでは心配ないので、単にこの警告を無効にします。
#pragma warning disable 67

namespace WinFormsContentLoading
{
    /// <summary>
    /// GraphicsDevice の作成と管理を担当するヘルパー クラス。
    /// すべての GraphicsDeviceControl インスタンスは、同一の GraphicsDeviceService を
    /// 共有するので、多数のコントロールが存在する場合でも、背後の GraphicsDevice は 
    /// 1 つしか存在しません。このヘルパーは標準の IGraphicsDeviceService 
    /// インターフェイスを実装します。このインターフェイスは、デバイスがリセットまたは
    /// 破棄されたときにそれに関する通知イベントを生成します。
    /// </summary>
    class GraphicsDeviceService : IGraphicsDeviceService
    {
        #region Fields


        /// <summary>
        /// シングルトン デバイス サービス インスタンス。
        /// singletonはデザインパターンの１つ。
        /// static プログラム全体で１つ。
        /// </summary>
        static GraphicsDeviceService singletonInstance;


        // singletonInstance を共有しているコントロールの数を追跡します。
        static int referenceCount;


        /// <summary>
        /// 現在のグラフィック デバイスを取得します。
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return graphicsDevice; 
            }
        }

        GraphicsDevice graphicsDevice;


        // 現在のデバイス設定を格納します。
        PresentationParameters parameters;


        // IGraphicsDeviceService イベント。
        public event EventHandler DeviceCreated;
        public event EventHandler DeviceDisposing;
        public event EventHandler DeviceReset;
        public event EventHandler DeviceResetting;

        #endregion


        /// <summary>
        /// シングルトン クラスなのでコンストラクターはプライベートです。
        /// クライアント コントロールは、代わりにパブリックの AddRef メソッドを
        /// 使用する必要があります。
        /// 
        /// privateなコンストラクタなので、ほかのクラスでは
        /// newできない。
        /// </summary>
        private GraphicsDeviceService(IntPtr windowHandle, int width, int height)
        {
            parameters = new PresentationParameters();

            parameters.BackBufferWidth = Math.Max(width, 1);
            parameters.BackBufferHeight = Math.Max(height, 1);
            parameters.BackBufferFormat = SurfaceFormat.Color;

            parameters.EnableAutoDepthStencil = true;
            parameters.AutoDepthStencilFormat = DepthFormat.Depth24;

            // グラフィックスデバイスを作成する。
            graphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter,
                                                DeviceType.Hardware,
                                                windowHandle,
                                                parameters);
        }

        /// <summary>
        /// シングルトン インスタンスに対する参照を取得します。
        /// 
        /// staticメソッドでインスタンスを作成または取得する。
        /// GraphicsDeviceService device = GraphicsDeviceService.AddRef(・・・);
        /// </summary>
        public static GraphicsDeviceService AddRef(IntPtr windowHandle,
                                                   int width, int height)
        {
            // "デバイスを共有しているコントロールの数" カウンターを増やします。
            if (Interlocked.Increment(ref referenceCount) == 1)
            {
                // これが、デバイスを使用し始める最初のコントロールの場合、
                // シングルトン インスタンスを作成する必要があります。
                // ここでは、同じクラス内なのでnewできる。
                singletonInstance = new GraphicsDeviceService(windowHandle,
                                                              width, height);
            }

            return singletonInstance;
        }


        /// <summary>
        /// シングルトン インスタンスに対する参照を解放します。
        /// </summary>
        public void Release(bool disposing)
        {
            // "デバイスを共有するコントロール数" カウンターを減らします。
            if (Interlocked.Decrement(ref referenceCount) == 0)
            {
                // これが、デバイスを使用し終える最後のコントロールの場合、
                // シングルトン インスタンスを破棄する必要があります。
                if (disposing)
                {
                    if (DeviceDisposing != null)
                        DeviceDisposing(this, EventArgs.Empty);

                    // 破棄する。
                    graphicsDevice.Dispose();
                }

                graphicsDevice = null;
            }
        }


        /// <summary>
        /// グラフィック デバイスを、指定した解像度または現在保有している
        /// コントロール サイズの大きい方に合わせてリセットします。この動作は、デバイスが、
        /// すべての GraphicsDeviceControl クライアントのうち最大のものに応じて
        /// 大きくなることを意味します。
        /// </summary>
        public void ResetDevice(int width, int height)
        {
            if (DeviceResetting != null)
                DeviceResetting(this, EventArgs.Empty);

            parameters.BackBufferWidth = Math.Max(parameters.BackBufferWidth, width);
            parameters.BackBufferHeight = Math.Max(parameters.BackBufferHeight, height);

            graphicsDevice.Reset(parameters);

            if (DeviceReset != null)
                DeviceReset(this, EventArgs.Empty);
        }

    }
}
