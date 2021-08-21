using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using type_to_win.Interfaces;
using type_to_win.Sprites;

namespace type_to_win.Levels
{
    /// <summary>
    /// Class for a HUD (heads up display)
    /// </summary>
    public class Hud
    {
        public FontRender LivesFont { get; private set; }  // draws how many lives player currently has left in level

        public Hud()
        {
           
        }

        public void Initialize()
        {
            // create new font render for Lives display
            LivesFont = new FontRender("EnemyWord", startPositionX: 0, startPositionY: 0, outlineColor: Color.Black);
        }

        public void LoadContent(ContentManager content)
        {
            LivesFont.LoadContent(content);
        }

        public void Update(GameTime gameTime, int lives)
        {
            // set lives number to how many lives uer has left
            LivesFont.Text = string.Format("Lives: {0}", lives);
            LivesFont.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            LivesFont.Draw(gameTime, spriteBatch);
        }

    }
}
