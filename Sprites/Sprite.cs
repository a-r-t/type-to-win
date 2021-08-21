using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using type_to_win.Interfaces;

namespace type_to_win.Sprites
{
    /// <summary>
    /// Base class for a Sprite, which is a 2D image
    /// This class will be used heavily by anything that has a graphic
    /// Expected to get larger as time goes on and more draw options are needed
    /// </summary>
    public class Sprite : GameObject
    {
        public SpriteSheet SpriteSheet { get; private set; }    // spritesheet object (contains spritesheet texture)
        public Vector2 Position { get; set; }                   // position of sprite on screen
        public float Scale { get; set; }                        // how much to scale sprite by
        public Vector2 Dimensions { get; private set; }         // dimensions of sprite (includes scale)
        public SpriteEffects SpriteEffect { get; set; }         // sprite effect to take place upon drawing sprite, if any
        public Rectangle SourceRectangle { get; set; }          // source rectangle of sprite inside of sprite sheet (start x, start y, width, height)

        /// <summary>
        /// Sprite constructor
        /// </summary>
        /// <param name="spriteSheetName">name of spritesheet containing image</param>
        /// <param name="spriteSize">size of sprite</param>
        /// <param name="scale">how much to scale sprite by</param>
        /// <param name="startPositionX">sprite's start X position on screen</param>
        /// <param name="startPositionY">sprite's start Y position on screen</param>
        /// <param name="spriteEffect">sprite effect for drawn sprite</param>
        public Sprite(string spriteSheetName, Vector2 spriteSize,
            float scale = 1, int startPositionX = 0, int startPositionY = 0, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            SpriteSheet = new SpriteSheet(spriteSheetName, spriteSize);
            Scale = scale;
            Dimensions = new Vector2(spriteSize.X * Scale, spriteSize.Y * Scale);
            Position = new Vector2(startPositionX, startPositionY);
            SpriteEffect = spriteEffect;
        }

        public virtual void Initialize()
        {

        }

        public virtual void LoadContent(ContentManager content)
        {
            // load spritesheet content into pipeline
            SpriteSheet.LoadContent(content);
            SourceRectangle = SpriteSheet.GetSprite(0, 0);
        }

        public virtual void Update(GameTime gameTime)
        {

        }
        
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // draw sprite to screen
            spriteBatch.Draw(SpriteSheet.SpriteSheetTexture, Position, SourceRectangle, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffect, 0.0f);
        }


    }
}
