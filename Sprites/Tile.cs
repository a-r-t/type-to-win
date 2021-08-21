using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Sprites
{
    /// <summary>
    /// Tile Class
    /// Will be for drawing tiles on screen
    /// Not implemented yet!!!
    /// </summary>
    public class Tile : AnimatedSprite
    {
        public Tile(string spriteSheetName, Vector2 spriteSize, int startPositionX = 0, int startPositionY = 0)
            : base(spriteSheetName, spriteSize, startPositionX: startPositionX, startPositionY: startPositionY)
        {

        }
    }
}
