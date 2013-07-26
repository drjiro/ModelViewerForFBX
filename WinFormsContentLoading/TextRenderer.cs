using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsContentLoading
{
    class TextRenderer
    {
        /// <summary>
        /// スプライトバッチ。
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// スプライトフォント。
        /// </summary>
        public SpriteFont spriteFont
        {
            get;
            set;
        }
    }
}
