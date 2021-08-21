using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Sprites
{
    /// <summary>
    /// Frame Class to represent a Frame in an Animation
    /// </summary>
    public class Frame
    {
        public int SpriteSheetColumnNumber { get; private set; }     // column number in spritesheet corresponding with this frame
        public Vector2 PositionOffset { get; private set; }          // Position offset for Frame (not yet implemented)
        public SpriteEffects SpriteEffect { get; private set; }      // Sprite Effect of Frame
        public int Scale { get; private set; }                       // Scale of Frame

        /// <summary>
        /// Frame Constructor
        /// </summary>
        /// <param name="spriteSheetColumnNumber">column number in spritesheet corresponding with this frame</param>
        /// <param name="offsetPositionX">X Position offset for Frame</param>
        /// <param name="offsetPositionY">Y Position offset for Frame</param>
        /// <param name="spriteEffect">Sprite Effect of Frame</param>
        /// <param name="scale">Scale of Frame</param>
        public Frame(int spriteSheetColumnNumber, int offsetPositionX = 0, int offsetPositionY = 0, SpriteEffects spriteEffect = SpriteEffects.None, int scale = 1)
        {
            SpriteSheetColumnNumber = spriteSheetColumnNumber;
            PositionOffset = new Vector2(offsetPositionX, offsetPositionY);
            SpriteEffect = spriteEffect;
            Scale = scale;
        }
    }
}
