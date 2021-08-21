using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using type_to_win.Interfaces;

namespace type_to_win.Sprites
{
    /// <summary>
    /// Base class for FontRendering
    /// Basically a SpriteFont handler class, allows for writing graphical text
    /// Because unlike regular fonts, games use "images" for fonts rather than vector graphics
    /// </summary>
    public class FontRender : GameObject
    {
        public string FontName { get; private set; }    // name of spritefont file (.spritefont extension, is basically an xml file)
        public SpriteFont Font { get; private set; }    // SpriteFont object (Loaded content from spritefont file)
        public virtual string Text { get; set; }        // display text of SpriteFont
        public Vector2 Position { get; set; }           // position on screen to be drawn to
        public Color FontColor { get; set; }            // color of font
        public Color OutlineColor { get; set; }
        public int Scale { get; set; }

        /// <summary>
        /// Constructor for FontRender
        /// </summary>
        /// <param name="fontName">name of spritefont file</param>
        /// <param name="text">display text</param>
        /// <param name="startPositionX">position to start at on X axis</param>
        /// <param name="startPositionY">position to start at on Y axis</param>
        /// <param name="scale">scale of spritefont</param>
        /// <param name="fontColor">font color</param>
        /// <param name="outlineColor">outline color</param>
        public FontRender(string fontName, string text = "",
             int startPositionX = 0, int startPositionY = 0, int scale = 1, Color? fontColor = null, Color? outlineColor = null)
        {
            FontName = fontName;
            Text = text;
            Position = new Vector2(startPositionX, startPositionY);
            FontColor = fontColor != null ? (Color)fontColor : Color.White;
            OutlineColor = outlineColor != null ? (Color)outlineColor : Color.Transparent; 
            Scale = scale;
        }

        public virtual void Initialize()
        {
        }

        public virtual void LoadContent(ContentManager content)
        {
            Font = content.Load<SpriteFont>(FontName);
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            // draw outline around SpriteFont
            if (OutlineColor != Color.Transparent)
            {
                spriteBatch.DrawString(Font, Text, Position + new Vector2(1 * Scale, 1 * Scale), Color.Black, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(Font, Text, Position + new Vector2(-1 * Scale, 1 * Scale), Color.Black, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(Font, Text, Position + new Vector2(-1 * Scale, -1 * Scale), Color.Black, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(Font, Text, Position + new Vector2(1 * Scale, -1 * Scale), Color.Black, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }


            // draws SpriteFont to screen
            spriteBatch.DrawString(Font, Text, Position, FontColor, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
