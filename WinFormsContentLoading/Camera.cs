///
/// ModelViewer for FBX/X
/// 
/// Copyright (C) 2019-2013 Dr.JIRO Software. All rights reserved.
///
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace WinFormsContentLoading
{
    /// <summary>
    /// カメラ。
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// ビュー変換行列。
        /// </summary>
        public Matrix ViewMatrix
        {
            get;
            protected set;
        }

        /// <summary>
        /// 射影変換行列。
        /// </summary>
        public Matrix ProjectionMatrix
        {
            get;
            protected set;
        }

        /// <summary>
        /// カメラの位置。視点。
        /// </summary>
        public Vector3 Eye;

        /// <summary>
        /// カメラの対象点。
        /// </summary>
        public Vector3 At;

        /// <summary>
        /// カメラの上向きベクトル。
        /// </summary>
        public Vector3 Up;

        /// <summary>
        /// カメラの水平軸。
        /// </summary>
        public Vector3 HorAxis;

        /// <summary>
        /// 画角。FOVh
        /// </summary>
        public float Fov;

        /// <summary>
        /// カメラのアスペクト比。
        /// </summary>
        public float AspectRatio;

        /// <summary>
        /// カメラの近くのクリップ面。
        /// </summary>
        public float NearClip;

        /// <summary>
        /// カメラの遠くのクリップ面。
        /// </summary>
        public float FarClip;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public Camera()
        {
            Eye = new Vector3(0, 0, 100);
            At = Vector3.Zero;
            Up = Vector3.Up;
            HorAxis = Vector3.UnitX;

            // デフォルトは45度
            Fov = 45;
            AspectRatio = 4.0f / 3.0f;
            NearClip = 0.1f;
            FarClip = 1000.0f;
        }

        /// <summary>
        /// ビュー行列を作成する。
        /// </summary>
        public void SetupView()
        {
            HorAxis = Vector3.Cross(Up, Eye);
            HorAxis.Normalize();
            Up = Vector3.Cross(Eye, HorAxis);
            Up.Normalize();
            
            ViewMatrix = Matrix.CreateLookAt(
                 Eye, At, Up);
        }

        /// <summary>
        /// 射影行列を作成する。
        /// </summary>
        public void SetupProjection()
        {
             ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                 MathHelper.ToRadians(Fov),
                 AspectRatio,
                 NearClip,
                 FarClip);
        }

        /// <summary>
        /// 水平方向に回転する。
        /// </summary>
        /// <param name="d">回転角。(度)</param>
        public void RotateHorizontally(float d)
        {
            Matrix rot = Matrix.CreateFromAxisAngle(Up,
                            MathHelper.ToRadians(d));
            Eye = Vector3.Transform(Eye, rot);
        }

        /// <summary>
        /// 垂直方向に回転する。
        /// </summary>
        /// <param name="d">回転角。(度)</param>
        public void RotateVirtically(float d)
        {
            Matrix rot = Matrix.CreateFromAxisAngle(
                            HorAxis, MathHelper.ToRadians(d));
            Eye = Vector3.Transform(Eye, rot);
        }

        /// <summary>
        /// ズームする。1～180度の間のみ。
        /// </summary>
        /// <param name="d">角度。(度)</param>
        public void Zoom(float d)
        {
            Fov += d;
            if (Fov < 1.0f)
            {
                Fov = 1.0f;
            }
            else if (Fov > 180.0f)
            {
                Fov = 180.0f;
            }
        }
    }
}
