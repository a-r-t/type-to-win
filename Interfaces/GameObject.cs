using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Interfaces
{
    /// <summary>
    /// All GameObjects should have these four methods to go along with the main game loop
    /// This is Monogame Convention
    /// </summary>
    public interface GameObject
    {
        void Initialize();
        void LoadContent(ContentManager content);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
