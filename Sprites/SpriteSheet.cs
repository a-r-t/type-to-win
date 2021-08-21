using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Sprites
{
    /// <summary>
    /// SpriteSheet Class
    /// Contains spritesheet file and texture info
    /// Also allows for easily getting a sprite from a spritesheet by row/column number
    /// </summary>
    public class SpriteSheet
    {
        public string FileName { get; private set; }                  // name of sprite sheet file
        public Texture2D SpriteSheetTexture { get; private set; }     // texture of sprite sheet (the image loaded into the game)
        public Vector2 Dimensions { get; private set; }               // dimensions of sprite sheet texture image
        public Vector2 SpriteSize { get; private set; }               // size of each sprite on sprite sheet

        /// <summary>
        /// Total number of rows in spritesheet
        /// Based on total length of sprite sheet divided by length of each sprite
        /// </summary>
        /// <returns>total number of rows in sprite sheet</returns>
        public int NumberOfRows()
        {
            return (int)(Dimensions.Y / SpriteSize.Y);
        }

        /// <summary>
        /// SpriteSheet Constructor
        /// </summary>
        /// <param name="fileName">name of sprite sheet file</param>
        /// <param name="spriteSize">size of each sprite in sprite sheet</param>
        public SpriteSheet(string fileName, Vector2 spriteSize)
        {
            FileName = fileName;
            SpriteSize = spriteSize;
        }

        public void LoadContent(ContentManager content)
        {
            // load sprite sheet texture
            SpriteSheetTexture = content.Load<Texture2D>(FileName);
            Dimensions = new Vector2(SpriteSheetTexture.Width, SpriteSheetTexture.Height);
        }

        /// <summary>
        /// Gets a sprite's source rectangle from spritesheet
        /// </summary>
        /// <param name="rowNumber">row number of sprite in spritesheet</param>
        /// <param name="columnNumber">column number of sprite in spritesheet</param>
        /// <returns></returns>
        public Rectangle GetSprite(int rowNumber, int columnNumber)
        {
            return new Rectangle(
                (int)(columnNumber * SpriteSize.X + columnNumber), 
                (int)(rowNumber * SpriteSize.Y + rowNumber), 
                (int)SpriteSize.X, 
                (int)SpriteSize.Y
            );
        }
    }
}
