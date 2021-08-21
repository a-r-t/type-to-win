using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Util
{
    public class ColorWrapper
    {
        public Color Color { get; set; }

        public ColorWrapper(Color color)
        {
            Color = color;
        }

        public string GetArgbRepresentation()
        {
            return new StringBuilder()
                .Append(Color.R.ToString())
                .Append(",")
                .Append(Color.G.ToString())
                .Append(",")
                .Append(Color.B.ToString())
                .Append(",")
                .Append(Color.A.ToString())
                .ToString();
        }
    }
}
