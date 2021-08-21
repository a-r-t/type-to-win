using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Levels
{
    /// <summary>
    /// Class to store parsed SaveInformation from save.json config file
    /// </summary>
    public class SaveInformation
    {
        // current level player is on
        [JsonProperty(PropertyName = "currentLevelIndex")]
        public int CurrentLevelIndex { get; set; }

        /// <summary>
        /// If no save file exists, this static method will create a "default" SaveInformation object as if the player started a new game
        /// </summary>
        /// <returns></returns>
        public static SaveInformation getEmptySaveInformation()
        {
            return new SaveInformation()
            {
                CurrentLevelIndex = 0
            };
        }
    }
}
