using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using type_to_win.Extension_Methods;
using type_to_win.Util;

namespace type_to_win.Sprites
{
    /// <summary>
    /// Class for creating a Multi Color Sprite Font
    /// When setting Text field, colors can be specified by putting {{R,G,B,A}} inside the string at any point
    /// It will change the sprite font to that color up until it hits another color definition
    /// If no color specification, default font/outline is used
    /// Example: "hi my name is {{255,0,0,255}}Alex" will make "Alex" red font
    /// To specify outline color: "hi my name is {{255,0,0,255|0,255,0,255}}Alex" will make "Alex" red font with green outline
    /// </summary>
    public class MultiColorFontRender : FontRender
    {
        public Color DefaultFontColor { get; set; }          // default font color when specific color not specified
        public Color DefaultOutlineColor { get; set; }       // default outline color when specific color not specified

        private List<ColorStringPairing> colorStringPairings; // matches pieces of text in Text attribute to font and outline color

        /// <summary>
        /// Whenever Text field is set, parse string for color data and create colorStringPairings list
        /// </summary>
        public override string Text {
            get => base.Text;
            set {
                base.Text = value;

                // reset colorStringPairings list now that there may be updated color data
                colorStringPairings = new List<ColorStringPairing>();

                // start by splitting on {{ to get color data matched up with its corresponding piece of text (unformatted)
                List<string> colorPairings = Text.Split("{{").ToList();

                // loop through each item from the spilt above
                colorPairings.ForEach(colorPairing => {

                    // split each item again on }} in order to get the color data formatted and split up the color data from the text
                    List<string> colorSpecifying = colorPairing.Split("}}").ToList();

                    // if the split led to one item, it means there is no color data and the piece of text should use default color and outline
                    if (colorSpecifying.Count == 1) {
                        string text = colorSpecifying[0];

                        // create colors and add them to colorStringPairings in association with the piece of text
                        colorStringPairings.Add(new ColorStringPairing(text, DefaultFontColor, DefaultOutlineColor));
                    }
                    // if the split led to two items, it means there is color data for this piece of text
                    else if (colorSpecifying.Count == 2)
                    {
                        string text = colorSpecifying[1];

                        // if the resulting color data contains a |, it means there is both font AND outline color data
                        if (colorSpecifying[0].Contains("|"))
                        {
                            // split on | to separate font and outline color data
                            string[] colors = colorSpecifying[0].Split("|");

                            // get RGBA from font color
                            string[] fontColorArgb = colors[0].Split(",");

                            // get RGBA from outline color
                            string[] outlineColorArgb = colors[1].Split(",");

                            // create colors and add them to colorStringPairings in association with the piece of text
                            Color fontColor = new Color(float.Parse(fontColorArgb[0]), float.Parse(fontColorArgb[1]), float.Parse(fontColorArgb[2]), float.Parse(fontColorArgb[3]));
                            Color outlineColor = new Color(float.Parse(outlineColorArgb[0]), float.Parse(outlineColorArgb[1]), float.Parse(outlineColorArgb[2]), float.Parse(outlineColorArgb[3]));
                            colorStringPairings.Add(new ColorStringPairing(text, fontColor, outlineColor));
                        }
                        // if no |, then there is only font color data
                        else
                        {
                            // get RGBA from font color
                            string[] fontColorArgb = colorSpecifying[0].Split(",");

                            // create color and add them to colorStringPairings in association with the piece of text
                            Color fontColor = new Color(float.Parse(fontColorArgb[0]), float.Parse(fontColorArgb[1]), float.Parse(fontColorArgb[2]), float.Parse(fontColorArgb[3]));
                            colorStringPairings.Add(new ColorStringPairing(text, fontColor, DefaultOutlineColor));
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Strips out color data from Text field value
        /// </summary>
        /// <returns>Text field value without color data</returns>
        public string GetTextWithColorsStripped()
        {
            // remove all data from string that is between {{ and }} (also removes the {{ }})
            return Regex.Replace(Text, @"{{.*?}}", "");

        }

        /// <summary>
        /// Basic struct for storing a piece of text with a corresponding font and outline color
        /// </summary>
        struct ColorStringPairing
        {
            public string text;
            public Color fontColor;
            public Color outlineColor;

            public ColorStringPairing(string text, Color fontColor, Color outlineColor)
            {
                this.text = text;
                this.fontColor = fontColor;
                this.outlineColor = outlineColor;
            }
        }

        /// <summary>
        /// Constructor for MultiColorFontRender
        /// </summary>
        /// <param name="fontName">name of spritefont file</param>
        /// <param name="text">display text</param>
        /// <param name="startPositionX">position to start at on X axis</param>
        /// <param name="startPositionY">position to start at on Y axis</param>
        /// <param name="scale">scale of spritefont</param>
        /// <param name="defaultFontColor">default font color</param>
        /// <param name="defaultOutlineColor">default outline color</param>
        public MultiColorFontRender(string fontName, string text = "", 
            int startPositionX = 0, int startPositionY = 0, int scale = 1, Color? defaultFontColor = null, Color? defaultOutlineColor = null)
            : base(fontName, text, startPositionX: startPositionX, startPositionY: startPositionY, scale: scale)
        {
            DefaultFontColor = defaultFontColor != null ? (Color)defaultFontColor : Color.White;
            DefaultOutlineColor = defaultOutlineColor != null ? (Color)defaultOutlineColor : Color.Transparent;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // draw outline
            Vector2 outlineOffset = Vector2.Zero;

            foreach (ColorStringPairing colorStringPairing in colorStringPairings)
            {
                if (colorStringPairing.outlineColor != Color.Transparent)
                {
                    spriteBatch.DrawString(Font, colorStringPairing.text, Position + new Vector2(1 * Scale, 1 * Scale) + outlineOffset, colorStringPairing.outlineColor, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(Font, colorStringPairing.text, Position + new Vector2(-1 * Scale, 1 * Scale) + outlineOffset, colorStringPairing.outlineColor, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(Font, colorStringPairing.text, Position + new Vector2(-1 * Scale, -1 * Scale) + outlineOffset, colorStringPairing.outlineColor, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(Font, colorStringPairing.text, Position + new Vector2(1 * Scale, -1 * Scale) + outlineOffset, colorStringPairing.outlineColor, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);

                    outlineOffset.X += Font.MeasureString(colorStringPairing.text).X;
                }
            }
            
            // draw font
            Vector2 textOffset = Vector2.Zero;

            foreach (ColorStringPairing colorStringPairing in colorStringPairings)
            {
                if (colorStringPairing.fontColor != Color.Transparent)
                {
                    spriteBatch.DrawString(Font, colorStringPairing.text, Position + textOffset, colorStringPairing.fontColor, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);

                    textOffset.X += Font.MeasureString(colorStringPairing.text).X;
                }
            }
            
        }

    }
}

