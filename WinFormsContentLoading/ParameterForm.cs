#region File Description
//-----------------------------------------------------------------------------
// ParameterForm.cs
//
// エフェクトパラメータ設定フォーム。
//
// Copyright (C) 2009 WADA Takao. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WinFormsContentLoading
{
    /// <summary>
    /// エフェクトのパラメータを設定するためのフォーム。
    /// ここで変更した値が描画に反映される。
    /// </summary>
    public partial class ParameterForm : Form
    {
        private MainForm mainForm;
        private BasicEffect defaultEffect;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="mainForm">メインフォーム。</param>
        public ParameterForm(MainForm mainForm)
        {
            this.mainForm = mainForm;

            InitializeComponent();
        }

        /// <summary>
        /// 初期化する。
        /// </summary>
        /// <param name="effect">デフォルトのエフェクト。</param>
        public void Initialize(BasicEffect effect)
        {
            this.defaultEffect = effect;
        }

        /// <summary>
        /// フォームで指定したパラメータをエフェクトに反映する。
        /// </summary>
        /// <param name="effect">設定するエフェクト。</param>
        public void SetParams(BasicEffect effect, GraphicsDevice device)
        {
            // デフォルトのライティングを有効にする。
            effect.EnableDefaultLighting();

            // フォームで設定した値をエフェクトに設定する。
            effect.DirectionalLight0.Enabled = light1EnabledCheckBox.Checked;
            effect.DirectionalLight0.DiffuseColor = new Vector3(
                (int)light1DiffuseRNumericUpDown.Value / 255.0f,
                (int)light1DiffuseGNumericUpDown.Value / 255.0f,
                (int)light1DiffuseBNumericUpDown.Value / 255.0f);
            effect.DirectionalLight0.SpecularColor = new Vector3(
                (int)light1SpecularRNumericUpDown.Value / 255.0f,
                (int)light1SpecularGNumericUpDown.Value / 255.0f,
                (int)light1SpecularBNumericUpDown.Value / 255.0f);
            effect.DirectionalLight0.Direction = Vector3.Normalize(new Vector3(
                (float)light1DirectionXNumericUpDown.Value,
                (float)light1DirectionYNumericUpDown.Value,
                (float)light1DirectionZNumericUpDown.Value));
            effect.DirectionalLight1.Enabled = light2EnabledCheckBox.Checked;
            effect.DirectionalLight1.DiffuseColor = new Vector3(
                (int)light2DiffuseRNumericUpDown.Value / 255.0f,
                (int)light2DiffuseGNumericUpDown.Value / 255.0f,
                (int)light2DiffuseBNumericUpDown.Value / 255.0f);
            effect.DirectionalLight1.SpecularColor = new Vector3(
                (int)light2SpecularRNumericUpDown.Value / 255.0f,
                (int)light2SpecularGNumericUpDown.Value / 255.0f,
                (int)light2SpecularBNumericUpDown.Value / 255.0f);
            effect.DirectionalLight1.Direction = Vector3.Normalize(new Vector3(
                (float)light2DirectionXNumericUpDown.Value,
                (float)light2DirectionYNumericUpDown.Value,
                (float)light2DirectionZNumericUpDown.Value));
            effect.DirectionalLight2.Enabled = light3EnabledCheckBox.Checked;
            effect.DirectionalLight2.DiffuseColor = new Vector3(
                (int)light3DiffuseRNumericUpDown.Value / 255.0f,
                (int)light3DiffuseGNumericUpDown.Value / 255.0f,
                (int)light3DiffuseBNumericUpDown.Value / 255.0f);
            effect.DirectionalLight2.SpecularColor = new Vector3(
                (int)light3SpecularRNumericUpDown.Value / 255.0f,
                (int)light3SpecularGNumericUpDown.Value / 255.0f,
                (int)light3SpecularBNumericUpDown.Value / 255.0f);
            effect.DirectionalLight2.Direction = Vector3.Normalize(new Vector3(
                (float)light3DirectionXNumericUpDown.Value,
                (float)light3DirectionYNumericUpDown.Value,
                (float)light3DirectionZNumericUpDown.Value));
            effect.AmbientLightColor = new Vector3(
                (int)ambientLightRNumericUpDown.Value / 255.0f,
                (int)ambientLightGNumericUpDown.Value / 255.0f,
                (int)ambientLightBNumericUpDown.Value / 255.0f);
            effect.Alpha = (int)alphaNumericUpDown.Value / 255.0f;
            effect.DiffuseColor = new Vector3(
                (int)diffuseColorRNumericUpDown.Value / 255.0f,
                (int)diffuseColorGNumericUpDown.Value / 255.0f,
                (int)diffuseColorBNumericUpDown.Value / 255.0f);
            effect.EmissiveColor = new Vector3(
                (int)emissiveColorRNumericUpDown.Value / 255.0f,
                (int)emissiveColorGNumericUpDown.Value / 255.0f,
                (int)emissiveColorBNumericUpDown.Value / 255.0f);
            effect.FogColor = new Vector3(
                (int)fogColorRNumericUpDown.Value / 255.0f,
                (int)fogColorGNumericUpDown.Value / 255.0f,
                (int)fogColorBNumericUpDown.Value / 255.0f);
            effect.FogEnabled = fogEnabledCheckBox.Checked;
            effect.FogEnd = (float)fogEndNumericUpDown.Value;
            effect.FogStart = (float)fogStartNumericUpDown.Value;
            effect.LightingEnabled = lightingEnabledCheckBox.Checked;
            effect.PreferPerPixelLighting = preferPerPixelLightingCheckBox.Checked;
            effect.SpecularColor = new Vector3(
                (int)specularColorRNumericUpDown.Value / 255.0f,
                (int)specularColorGNumericUpDown.Value / 255.0f,
                (int)specularColorBNumericUpDown.Value / 255.0f);
            effect.SpecularPower = (int)specularPowerNumericUpDown.Value;
            effect.TextureEnabled = textureEnabledCheckBox.Checked;
            effect.VertexColorEnabled = vertexColorEnabledCheckBox.Checked;
            device.RenderState.AlphaBlendEnable = alphaBlendingEnabledCheckBox.Checked;
        }

        /// <summary>
        /// パラメータをリセットする。
        /// </summary>
        public void ResetParams()
        {
            // デフォルトのライティングを有効にする。
            defaultEffect.EnableDefaultLighting();

            // デフォルトの値をフォームのコントロールに設定する。
            light1EnabledCheckBox.Checked = defaultEffect.DirectionalLight0.Enabled;
            light1DiffuseRNumericUpDown.Value = (int)(defaultEffect.DirectionalLight0.DiffuseColor.X * 255);
            light1DiffuseGNumericUpDown.Value = (int)(defaultEffect.DirectionalLight0.DiffuseColor.Y * 255);
            light1DiffuseBNumericUpDown.Value = (int)(defaultEffect.DirectionalLight0.DiffuseColor.Z * 255);
            light1SpecularRNumericUpDown.Value = (int)(defaultEffect.DirectionalLight0.SpecularColor.X * 255);
            light1SpecularGNumericUpDown.Value = (int)(defaultEffect.DirectionalLight0.SpecularColor.Y * 255);
            light1SpecularBNumericUpDown.Value = (int)(defaultEffect.DirectionalLight0.SpecularColor.Z * 255);
            light1DirectionXNumericUpDown.Value = (int)(defaultEffect.DirectionalLight0.Direction.X * 100);
            light1DirectionYNumericUpDown.Value = (int)(defaultEffect.DirectionalLight0.Direction.Y * 100);
            light1DirectionZNumericUpDown.Value = (int)(defaultEffect.DirectionalLight0.Direction.Z * 100);

            light2EnabledCheckBox.Checked = defaultEffect.DirectionalLight1.Enabled;
            light2DiffuseRNumericUpDown.Value = (int)(defaultEffect.DirectionalLight1.DiffuseColor.X * 255);
            light2DiffuseGNumericUpDown.Value = (int)(defaultEffect.DirectionalLight1.DiffuseColor.Y * 255);
            light2DiffuseBNumericUpDown.Value = (int)(defaultEffect.DirectionalLight1.DiffuseColor.Z * 255);
            light2SpecularRNumericUpDown.Value = (int)(defaultEffect.DirectionalLight1.SpecularColor.X * 255);
            light2SpecularGNumericUpDown.Value = (int)(defaultEffect.DirectionalLight1.SpecularColor.Y * 255);
            light2SpecularBNumericUpDown.Value = (int)(defaultEffect.DirectionalLight1.SpecularColor.Z * 255);
            light2DirectionXNumericUpDown.Value = (decimal)(defaultEffect.DirectionalLight1.Direction.X * 100);
            light2DirectionYNumericUpDown.Value = (decimal)(defaultEffect.DirectionalLight1.Direction.Y * 100);
            light2DirectionZNumericUpDown.Value = (decimal)(defaultEffect.DirectionalLight1.Direction.Z * 100);

            light3EnabledCheckBox.Checked = defaultEffect.DirectionalLight2.Enabled;
            light3DiffuseRNumericUpDown.Value = (int)(defaultEffect.DirectionalLight2.DiffuseColor.X * 255);
            light3DiffuseGNumericUpDown.Value = (int)(defaultEffect.DirectionalLight2.DiffuseColor.Y * 255);
            light3DiffuseBNumericUpDown.Value = (int)(defaultEffect.DirectionalLight2.DiffuseColor.Z * 255);
            light3SpecularRNumericUpDown.Value = (int)(defaultEffect.DirectionalLight2.SpecularColor.X * 255);
            light3SpecularGNumericUpDown.Value = (int)(defaultEffect.DirectionalLight2.SpecularColor.Y * 255);
            light3SpecularBNumericUpDown.Value = (int)(defaultEffect.DirectionalLight2.SpecularColor.Z * 255);
            light3DirectionXNumericUpDown.Value = (int)(defaultEffect.DirectionalLight2.Direction.X * 100);
            light3DirectionYNumericUpDown.Value = (int)(defaultEffect.DirectionalLight2.Direction.Y * 100);
            light3DirectionZNumericUpDown.Value = (int)(defaultEffect.DirectionalLight2.Direction.Z * 100);

            ambientLightRNumericUpDown.Value = (int)(defaultEffect.AmbientLightColor.X * 255);
            ambientLightGNumericUpDown.Value = (int)(defaultEffect.AmbientLightColor.Y * 255);
            ambientLightBNumericUpDown.Value = (int)(defaultEffect.AmbientLightColor.Z * 255);

            alphaNumericUpDown.Value = (int)(defaultEffect.Alpha * 255);
            diffuseColorRNumericUpDown.Value = (int)(defaultEffect.DiffuseColor.X * 255);
            diffuseColorGNumericUpDown.Value = (int)(defaultEffect.DiffuseColor.Y * 255);
            diffuseColorBNumericUpDown.Value = (int)(defaultEffect.DiffuseColor.Z * 255);
            emissiveColorRNumericUpDown.Value = (int)(defaultEffect.EmissiveColor.X * 255);
            emissiveColorGNumericUpDown.Value = (int)(defaultEffect.EmissiveColor.Y * 255);
            emissiveColorBNumericUpDown.Value = (int)(defaultEffect.EmissiveColor.Z * 255);
            fogColorRNumericUpDown.Value = (int)(defaultEffect.FogColor.X * 255);
            fogColorGNumericUpDown.Value = (int)(defaultEffect.FogColor.Y * 255);
            fogColorBNumericUpDown.Value = (int)(defaultEffect.FogColor.Z * 255);
            fogEnabledCheckBox.Checked = defaultEffect.FogEnabled;
            fogEndNumericUpDown.Value = (decimal)defaultEffect.FogEnd;
            fogStartNumericUpDown.Value = (decimal)defaultEffect.FogStart;
            lightingEnabledCheckBox.Checked = defaultEffect.LightingEnabled;
            preferPerPixelLightingCheckBox.Checked = defaultEffect.PreferPerPixelLighting;
            specularColorRNumericUpDown.Value = (int)(defaultEffect.SpecularColor.X * 255);
            specularColorGNumericUpDown.Value = (int)(defaultEffect.SpecularColor.Y * 255);
            specularColorBNumericUpDown.Value = (int)(defaultEffect.SpecularColor.Z * 255);
            specularPowerNumericUpDown.Value = (int)(defaultEffect.SpecularPower);
            textureEnabledCheckBox.Checked = defaultEffect.TextureEnabled;
            vertexColorEnabledCheckBox.Checked = defaultEffect.VertexColorEnabled;
            alphaBlendingEnabledCheckBox.Checked = false;
        }

        /// <summary>
        /// リセットボタンが押された。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetParams();
        }
    }
}