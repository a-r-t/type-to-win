using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Extension_Methods
{
    public static class StringExtensionMethods
    {
        public static string Slice(this string str, int startIndex, int endIndex)
        {
            Console.WriteLine(str);
            Console.WriteLine(startIndex + " " + endIndex);
            Console.WriteLine(str.Substring(startIndex, str.Length - endIndex + 1));
            return str.Substring(startIndex, str.Length - endIndex);
        }

        public static Color FromName(this Color value, string colorName)
        {
            System.Drawing.Color systemColor = System.Drawing.Color.FromName(colorName);
           
            return new Color(systemColor.R, systemColor.G, systemColor.B, systemColor.A); //Here Color is Microsoft.Xna.Framework.Graphics.Color
        }
    }
}
