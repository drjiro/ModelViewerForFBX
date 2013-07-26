#region File Description
///-----------------------------------------------------------------------------
/// ModelViewer for FBX/X
///
/// Microsoft XNA Community Game Platform
/// Copyright (C) Microsoft Corporation. All rights reserved.
/// Copyright (C) 2019-2013 Dr.JIRO Software. All rights reserved.
/////-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace WinFormsContentLoading
{
    /// <summary>
    /// このコントロールは GraphicsDeviceControl から継承し、回転する 
    /// 3D モデルを表示します。メイン フォーム クラスがモデルの読み込みを
    /// 担うので、このコントロールはそれを表示するだけです。
    /// </summary>
    class ModelViewerControl : GraphicsDeviceControl
    {
        /// <summary>
        /// 現在のモデルを取得または設定します。
        /// </summary>
        public Model Model
        {
            get
            {
                return model; 
            }

            set
            {
                model = value;

                if (model != null)
                {
                    // モデルの大きさをはかる。
                    MeasureModel();
                    // 視点はモデルの中心。
                    camera.Eye = modelCenter;

                    // Z位置は半径X2、Y位置は半径
                    camera.Eye.Z += modelRadius * 2;
                    camera.Eye.Y += modelRadius;
                    camera.At = modelCenter;
                    camera.SetupView();

                    camera.NearClip = modelRadius / 100;
                    camera.FarClip = modelRadius * 100;
                    camera.SetupProjection();
                }
            }
        }

        /// <summary>
        /// モデル。
        /// </summary>
        private Model model;


        // モデルのサイズと位置に関する情報をキャッシュします。
        Matrix[] boneTransforms;
        Vector3 modelCenter;
        float modelRadius;


        // タイマーは回転速度を制御します。
        Stopwatch timer;

        /// <summary>
        /// デフォルトのエフェクト。
        /// </summary>
        public BasicEffect DefaultEffect;

        /// <summary>
        /// カメラ。
        /// </summary>
        private Camera camera;

        /// <summary>
        /// テキスト描画。
        /// </summary>
        private TextRenderer textRender;

        /// <summary>
        /// コントロールを初期化します。
        /// </summary>
        protected override void Initialize()
        {
            // アニメーション タイマーを開始します。
            timer = Stopwatch.StartNew();

            // アニメーションを定期的に再描画するためにアイドル イベントをフックします。
            Application.Idle += delegate { Invalidate(); };

            // デフォルトのエフェクトを取得する。
            DefaultEffect = new BasicEffect(GraphicsDevice, null);

            camera = new Camera();
            camera.AspectRatio = GraphicsDevice.Viewport.AspectRatio;
        }


        /// <summary>
        /// コントロールを描画します。
        /// </summary>
        protected override void Draw()
        {
            HandleInput();

            ((MainForm)Parent).ShowParameters(camera);

            // 既定のコントロールの背景色でクリアします。
            Color backColor = new Color(BackColor.R, BackColor.G, BackColor.B);

            GraphicsDevice.Clear(backColor);

            if (model != null)
            {
                // カメラの行列を計算します。
                //float rotation = (float)timer.Elapsed.TotalSeconds;

//                Matrix world = Matrix.CreateRotationY(rotation);
                Matrix world = Matrix.Identity;
                // モデルを描画します。
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = boneTransforms[mesh.ParentBone.Index] * world;
                        effect.View = camera.ViewMatrix;
                        effect.Projection = camera.ProjectionMatrix;

                        // フォームの値を反映する。
                        ((MainForm)Parent).ParameterForm.SetParams(effect, GraphicsDevice);

                        //effect.EnableDefaultLighting();
                        //effect.PreferPerPixelLighting = true;
                        //effect.SpecularPower = 16;
                    }

                    mesh.Draw();
                }
            }
        }

        /// <summary>
        /// 入力ハンドラー。
        /// </summary>
        private void HandleInput()
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                camera.RotateHorizontally(1.0f);
            }
            else if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                camera.RotateHorizontally(-1.0f);
            }
            if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                camera.RotateVirtically(1.0f);
            }
            else if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                camera.RotateVirtically(-1.0f);
            }
            if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.PageUp))
            {
                camera.Zoom(0.1f);
            }
            else if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.PageDown))
            {
                camera.Zoom(-0.1f);
            }
            camera.SetupView();
            camera.SetupProjection();
        }

        /// <summary>
        /// 新しいモデルが選択されると、そのモデルの大きさと中心の位置を確認します。
        /// これにより、自動的に表示を拡大または縮小できるので、どのスケールの
        /// モデルでも正しく処理できます。
        /// </summary>
        void MeasureModel()
        {
            // このモデルの絶対ボーン トランスフォームを検索します。
            boneTransforms = new Matrix[model.Bones.Count];
            
            model.CopyAbsoluteBoneTransformsTo(boneTransforms);

            // すべてのメッシュの各境界球の中心を平均することによって、
            // モデルのおおよその中心位置を計算します。
            modelCenter = Vector3.Zero;

            foreach (ModelMesh mesh in model.Meshes)
            {
                BoundingSphere meshBounds = mesh.BoundingSphere;
                Matrix transform = boneTransforms[mesh.ParentBone.Index];
                Vector3 meshCenter = Vector3.Transform(meshBounds.Center, transform);

                modelCenter += meshCenter;
            }

            // 中心はすべてのメッシュの中心の平均値
            modelCenter /= model.Meshes.Count;

            // これで中心点がわかったので、メッシュの各境界球の半径を
            // 調べることによって、モデルの半径を計算できます。
            modelRadius = 0;

            foreach (ModelMesh mesh in model.Meshes)
            {
                BoundingSphere meshBounds = mesh.BoundingSphere;
                Matrix transform = boneTransforms[mesh.ParentBone.Index];
                Vector3 meshCenter = Vector3.Transform(meshBounds.Center, transform);

                float transformScale = transform.Forward.Length();
                
                float meshRadius = (meshCenter - modelCenter).Length() +
                                   (meshBounds.Radius * transformScale);

                modelRadius = Math.Max(modelRadius,  meshRadius);
            }
        }

    }
}
