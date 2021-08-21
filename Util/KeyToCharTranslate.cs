using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Levels
{
    /// <summary>
    /// Global class to translate a Keys object to a character
    /// Many of the non-alpha keys on the keyboard, such as Space, toString as "Space" rather than the character ' '
    /// This class (and the function in it GetCharFromKey) rectifies this situation
    /// </summary>
    public static class KeyToCharTranslate
    {
        /// <summary>
        /// Converts a Keys object to a singular character
        /// </summary>
        /// <param name="key">key object to translate</param>
        /// <returns>translated character</returns>
        public static char GetCharFromKey(Keys key)
        {
            switch(key.ToString())
            {
                case "Space":
                    return ' ';
                case "D1":
                    return '1';
                case "D2":
                    return '2';
                case "D3":
                    return '3';
                case "D4":
                    return '4';
                case "D5":
                    return '5';
                case "D6":
                    return '6';
                case "D7":
                    return '7';
                case "D8":
                    return '8';
                case "D9":
                    return '9';
                case "D0":
                    return '0';
                default:
                    return key.ToString()[0];
            }
        }
    }
}
