using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using type_to_win.Levels;

namespace type_to_win.Database
{
    /// <summary>
    /// Class for interacting with Words table in SQLite Database
    /// </summary>
    public class LevelWordsDatabaseHandler : DatabaseHandler
    {
        public LevelWordsDatabaseHandler()
            : base()
        {
            
        }

        /// <summary>
        /// Gets all words from Words table where word's difficulty is less than or equal to maxWordDifficulty
        /// </summary>
        /// <param name="maxWordDifficulty">word difficulty threshold</param>
        /// <returns></returns>
        public List<LevelWord> GetLevelWords(int maxWordDifficulty)
        {
            string sql = "SELECT * FROM words WHERE difficulty <= $difficulty";

            Dictionary<string, object> dbParams = new Dictionary<string, object>()
            {
                { "$difficulty", maxWordDifficulty }
            };

            SQLiteDataReader records = RunQuery(sql, dbParams);
            List<LevelWord> levelWords = MapRecordsToLevelWords(records);
            DbConnection.Close();
            return levelWords;

        }

        /// <summary>
        /// Maps records from words table to a List of LevelWords objects
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        public List<LevelWord> MapRecordsToLevelWords(SQLiteDataReader records)
        {
            List<LevelWord> levelWords = new List<LevelWord>();
            while (records.Read())
            {
                string word = (string)records[0];
                int difficulty = (int)records[1];
                int size = (int)records[2];

                levelWords.Add(new LevelWord(
                      word, 
                      difficulty, 
                      size
                ));
            }
            records.Close();
            return levelWords;
        }

    }
}
