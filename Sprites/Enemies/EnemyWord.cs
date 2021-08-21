using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using type_to_win.Sprites;
using type_to_win.Util;

namespace type_to_win.Sprites.Enemies
{
    public class EnemyWord : MultiColorFontRender
    {
        public int NextIndexToBeTyped { get; set; }                 // next character index in Text to be typed
        public Color LettersNotTypedColor { get; private set; }     // font color for letters not typed yet on enemy (coming soon)
        public Color LettersTypedColor { get; private set; }        // font color for letters typed already on enemy (coming soon)

        /// <summary>
        /// Gets current character that needs to be typed next on word
        /// </summary>
        /// <returns>next character to be typed</returns>
        public char CurrentCharacter()
        {
            return GetTextWithColorsStripped()[NextIndexToBeTyped];
        }

        /// <summary>
        /// EnemyWord constructor
        /// </summary>
        /// <param name="word">word that is associated with enemy</param>
        /// <param name="lettersNotTypedColor">font color for letters not typed yet on enemy</param>
        /// <param name="lettersTypedColor">font color for letters typed already on enemy (coming soon)</param>
        public EnemyWord(string word, Color? lettersNotTypedColor = null, Color? lettersTypedColor = null, Color? outlineColor = null)
            : base(fontName: "EnemyWord", defaultOutlineColor: Color.Black)
        {
            Text = word;
            NextIndexToBeTyped = 0;

            // if no color is specified, default to white/yellow
            LettersNotTypedColor = lettersNotTypedColor != null ? (Color)lettersNotTypedColor : Color.White;
            LettersTypedColor = lettersTypedColor != null ? (Color)lettersTypedColor : Color.Green;
            OutlineColor = outlineColor != null ? (Color)outlineColor : Color.Transparent;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Validates a character typed against the current word of enemy
        /// if character typed is correct, the enemy is "harmed" and the next character in word is on deck
        /// </summary>
        /// <param name="characterTyped">the character typed by the user</param>
        public bool ValidateTypedCharacter(char characterTyped)
        {
            // if the character matches the enemy's next character
            if (Char.ToLower(characterTyped) == Char.ToLower(CurrentCharacter()))
            {
                NextIndexToBeTyped++; // update to the next character to be typed in the enemy's word

                // set Text's color data to have already typed colors in Green and not yet typed colors in White
                Text = new StringBuilder()
                    .Append("{{")
                    .Append(new ColorWrapper(Color.Green).GetArgbRepresentation())
                    .Append("}}")
                    // takes from first character in string up to the last character typed successfully
                    .Append(GetTextWithColorsStripped().Substring(0, NextIndexToBeTyped)) 
                    .Append("{{")
                    .Append(new ColorWrapper(DefaultFontColor).GetArgbRepresentation())
                    .Append("}}")
                    // takes from the next character that needs to be typed to the end of the string
                    .Append(GetTextWithColorsStripped().Substring(NextIndexToBeTyped, GetTextWithColorsStripped().Length - NextIndexToBeTyped))
                    .ToString();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if entire current word is typed in full successfully
        /// </summary>
        /// <returns>if word is typed successfully or not</returns>
        public bool IsWordTypedSuccessfully()
        {
            return NextIndexToBeTyped >= GetTextWithColorsStripped().Length;
        }
        
    }
}
