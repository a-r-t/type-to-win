using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using type_to_win.Database;

namespace type_to_win.Levels
{
    public class LevelWord
    {
        public string Word { get; set; }
        public int Size { get; set; }
        public int Difficulty { get; set; }

        public LevelWord(string word, int size, int difficulty)
        {
            Word = word;
            Size = size;
            Difficulty = difficulty;
        }
    }
}
