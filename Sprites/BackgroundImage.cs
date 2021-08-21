using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Sprites
{
    /// <summary>
    /// Background Image Class
    /// Draws an image to size of window (will scale it for you)
    /// </summary>
    public class BackgroundImage : Sprite
    {
        public Rectangle WindowSize { get; set; }   // size of window to draw background image to

        /// <summary>
        /// Background Image Constructor
        /// </summary>
        /// <param name="spriteSheetName">name of spritesheet containing image</param>
        /// <param name="spriteSize">size of sprite on spritesheet (usually divisible by 8)</param>
        /// <param name="windowSize">size of window to draw background image to</param>
        /// <param name="startPositionX">sprite's start X position on screen</param>
        /// <param name="startPositionY">sprite's start Y position on screen</param>
        public BackgroundImage(string spriteSheetName, Vector2 spriteSize, Rectangle windowSize,
            int startPositionX = 0, int startPositionY = 0)
            : base(spriteSheetName, spriteSize, startPositionX: startPositionX, startPositionY: startPositionY)
        {
            WindowSize = windowSize;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // draw background image, auto scale it to size of WindowSize
            spriteBatch.Draw(SpriteSheet.SpriteSheetTexture, WindowSize, Color.White);
        }
    }
}
