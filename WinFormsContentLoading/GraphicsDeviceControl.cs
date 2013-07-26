#region File Description
//-----------------------------------------------------------------------------
// GraphicsDeviceControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WinFormsContentLoading
{
    // System.Drawing と XNA Framework の両方で、Color 型と Rectangle 型が
    // 定義されています。競合を避けるために、どちらを使用するか正確に指定します。
    using Color = System.Drawing.Color;
    using Rectangle = Microsoft.Xna.Framework.Rectangle;


    /// <summary>
    /// このカスタム コントロールは、XNA Framework GraphicsDevice を使用して、
    /// Windows フォームにレンダリングします。派生クラスは、Initialize メソッドと 
    /// Draw メソッドをオーバーライドして、独自の描画コードを追加できます。
    /// </summary>
    abstract public class GraphicsDeviceControl : Control
    {
        #region フィールド


        // 使用する GraphicsDeviceControl インスタンスの数に関係なく、背後では、
        // すべてこのヘルパー サービスで管理される同一の GraphicsDevice を共有します。
        GraphicsDeviceService graphicsDeviceService;


        #endregion

        #region プロパティ


        /// <summary>
        /// このコントロールへの描画に使用できる GraphicsDevice を取得します。
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            // getプロパティの使い方
            // GraphicsDevice device = xxxx.GraphicsDevice;
            // setプロパティの使い方。この場合は使えない。
            // xxxx.GraphicsDevice = yyyy;
            get
            {
                return graphicsDeviceService.GraphicsDevice; 
            }
        }


        /// <summary>
        /// このサンプルの IGraphicsDeviceService を含む IServiceProvider を取得します。
        /// これは、ContentManager などのコンポーネントで使用できます。これらの
        /// コンポーネントはこのサービスを使用して、GraphicsDevice を取得します。
        /// </summary>
        public ServiceContainer Services
        {
            get
            {
                return services; 
            }
        }

        ServiceContainer services = new ServiceContainer();


        #endregion

        #region Initialization


        /// <summary>
        /// コントロールを初期化します。
        /// </summary>
        protected override void OnCreateControl()
        {
            // デザイナー内で実行している場合は、
            // グラフィック デバイスを初期化しません。
            if (!DesignMode)
            {
                graphicsDeviceService = GraphicsDeviceService.AddRef(Handle,
                                                                     ClientSize.Width,
                                                                     ClientSize.Height);

                // ContentManager などのコンポーネントから検出できるように、サービスを登録します。
                services.AddService<IGraphicsDeviceService>(graphicsDeviceService);

                // 派生クラスに自身を初期化する機会を与えます。
                Initialize();
            }

            base.OnCreateControl();
        }


        /// <summary>
        /// コントロールを破棄します。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (graphicsDeviceService != null)
            {
                graphicsDeviceService.Release(disposing);
                graphicsDeviceService = null;
            }

            base.Dispose(disposing);
        }


        #endregion

        #region Paint


        /// <summary>
        /// WinForms ペイント メッセージに応答してコントロールを再描画します。
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            string beginDrawError = BeginDraw();

            // IsNullOrEmptyは、nullか""
            if (string.IsNullOrEmpty(beginDrawError))
            {
                // GraphicsDevice を使用してコントロールを描画します。
                Draw();
                EndDraw();
            }
            else
            {
                // BeginDraw が失敗した場合、System.Drawing を使用してエラー メッセージを表示します。
                PaintUsingSystemDrawing(e.Graphics, beginDrawError);
            }
        }


        /// <summary>
        /// コントロールの描画を開始しようとします。グラフィック デバイスがロストしていたり、
        /// フォーム デザイナー内部で実行している場合に開始できないことがありますが、
        /// この場合は、エラー メッセージ文字列を返します。
        /// </summary>
        string BeginDraw()
        {
            // グラフィック デバイスがない場合は、デザイナー内で実行しています。
            if (graphicsDeviceService == null)
            {
                return Text + "\n\n" + GetType();
            }

            // グラフィック デバイスが十分に大きく、ロストしていないことを確認します。
            string deviceResetError = HandleDeviceReset();

            if (!string.IsNullOrEmpty(deviceResetError))
            {
                return deviceResetError;
            }

            // 多数の GraphicsDeviceControl インスタンスが、同一の GraphicsDevice を
            // 共有できます。デバイス バックバッファーは、これらのうち最大のコントロール
            // に合わせてサイズが変更されます。では、現在より小さなコントロールを
            // 描画している場合はどうでしょうか。不要に引き伸ばされることを避けるため、
            // フル バックバッファーの左上部分だけを使用するように、ビューポートを設定します。
            Viewport viewport = new Viewport();

            viewport.X = 0;
            viewport.Y = 0;

            viewport.Width = ClientSize.Width;
            viewport.Height = ClientSize.Height;

            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            GraphicsDevice.Viewport = viewport;

            return null;
        }


        /// <summary>
        /// コントロールの描画を終了します。これは、派生クラスが Draw メソッドを終了した
        /// 後に呼び出され、完了したイメージを画面上に表示する役割を担います。
        /// 適切な WinForms コントロール ハンドルを使用して、正しい位置に
        /// 表示されるようにします。
        /// </summary>
        void EndDraw()
        {
            try
            {
                Rectangle sourceRectangle = new Rectangle(0, 0, ClientSize.Width,
                                                                ClientSize.Height);

                GraphicsDevice.Present(sourceRectangle, null, this.Handle);
            }
            catch
            {
                // Present は、デバイスが描画中に失われた場合にスローする場合があります。
                // ロストしたデバイスは、次の BeginDraw で処理されるので、例外は受け入れるだけで、
                // 特別な処理はしません。
            }
        }


        /// <summary>
        /// BeginDraw により使用されるヘルパー。これは、グラフィック デバイスのステータスを
        /// チェックして、現在のコントロールの描画に十分大きく、デバイスがロストしていない
        /// ことを確認します。デバイスをリセットできなかった場合、エラー文字列を返します。
        /// </summary>
        string HandleDeviceReset()
        {
            bool deviceNeedsReset = false;

            switch (GraphicsDevice.GraphicsDeviceStatus)
            {
                case GraphicsDeviceStatus.Lost:
                    // グラフィック デバイスがロストしている場合は、まったく使用できません。
                    return "Graphics device lost";

                case GraphicsDeviceStatus.NotReset:
                    // デバイスがリセットされていない状態の場合は、リセットを試みる必要があります。
                    deviceNeedsReset = true;
                    break;

                default:
                    // デバイスの状態が問題ない場合、十分に大きいかどうかをチェックします。
                    PresentationParameters pp = GraphicsDevice.PresentationParameters;

                    deviceNeedsReset = (ClientSize.Width > pp.BackBufferWidth) ||
                                       (ClientSize.Height > pp.BackBufferHeight);
                    break;
            }

            // デバイスをリセットする必要がありますか。
            if (deviceNeedsReset)
            {
                try
                {
                    graphicsDeviceService.ResetDevice(ClientSize.Width,
                                                      ClientSize.Height);
                }
                catch (Exception e)
                {
                    return "Graphics device reset failed\n\n" + e;
                }
            }

            return null;
        }


        /// <summary>
        /// 有効なグラフィック デバイスがない場合 (たとえば、デバイスが失われていたり、
        /// フォーム デザイナー内で実行している場合)、通常の System.Drawing 
        /// メソッドを使用して、ステータス メッセージを表示する必要があります。
        /// </summary>
        protected virtual void PaintUsingSystemDrawing(Graphics graphics, string text)
        {
            graphics.Clear(Color.CornflowerBlue);

            using (Brush brush = new SolidBrush(Color.Black))
            {
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    graphics.DrawString(text, Font, brush, ClientRectangle, format);
                }
            }
        }


        /// <summary>
        /// 背景の描画を指示する、WinForms のメッセージを無視します。既定の実装では、
        /// コントロールを現在の背景色でクリアします。続いて OnPaint 実装が XNA 
        /// Framework の GraphicsDevice を使用して直ちに別の色を重ねて描画するので、
        /// 画面をちらつかせてしまいます。
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }


        #endregion

        #region Abstract Methods


        /// <summary>
        /// 派生クラスは、これをオーバーライドして、描画コードを初期化します。
        /// </summary>
        protected abstract void Initialize();


        /// <summary>
        /// 派生クラスは、これをオーバーライドして、GraphicsDevice を使用して自身を描画します。
        /// </summary>
        protected abstract void Draw();


        #endregion
    }
}
