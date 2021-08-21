
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace type_to_win.Database
{
    /// <summary>
    /// Setsup Database for Type To Win game
    /// </summary>
    public class SetupDatabase
    {
        /// <summary>
        /// Creates and populates Database
        /// </summary>
        /// <param name="content"></param>
        public static async void SetUp(ContentManager content)
        {
            // create file
            Console.WriteLine("Creating file");
            SQLiteConnection.CreateFile(ConfigurationManager.AppSettings["databaseFile"]);
            DatabaseHandler databaseHandler = new DatabaseHandler();

            // create words table
            Console.WriteLine("Creating table");
            databaseHandler.RunNonQuery("CREATE TABLE words (text VARCHAR(20), difficulty INT, size INT)");

            WebClient webClient = new WebClient();
            webClient.Headers.Add("X-RapidAPI-Host", "wordsapiv1.p.rapidapi.com");
            webClient.Headers.Add("X-RapidAPI-Key", "17a05eee8fmsh60cb82784b6dfe5p1dfd45jsndd4272b511ed");
            webClient.QueryString.Add("lettersMin", "1");
            int page = 1;
            JObject result;
            // Continually call API through every page and insert returned data into database as a word in the words table
            Console.WriteLine("Getting data from API");
            do
            {
                webClient.QueryString.Set("page", page.ToString());
                // make request to words api
                result = await WebClientCall(webClient);
                Console.WriteLine(result.ToString());
                foreach (string word in result["results"]["data"])
                {
                    int size = GetWordSize(word, content);
                    int difficulty = GetWordDifficulty(word);
                    // insert word into words table
                    string sql = "INSERT INTO words (text, difficulty, size) values ($text, $difficulty, $size)";
                    Dictionary<string, object> dbParams = new Dictionary<string, object>()
                    {
                        { "$text", word },
                        { "$difficulty", difficulty },
                        { "$size", size }
                    };
                    databaseHandler.RunNonQuery(sql, dbParams);
                }
                page++;
            } while (result["results"]["data"].Any());
            Console.WriteLine("Finished");
            databaseHandler.DbConnection.Close();
        }

        static async Task<JObject> WebClientCall(WebClient webClient)
        {
            return JObject.Parse(webClient.DownloadString("https://wordsapiv1.p.mashape.com/words/"));
        }

        /// <summary>
        /// Calculates size of a word
        /// Size is NOT how many letters it has, but rather the measure of the string as a spritefont
        /// </summary>
        /// <param name="word">word to get size of</param>
        /// <param name="content">content manager in order to load proper spritefont</param>
        /// <returns>word size (int)</returns>
        static int GetWordSize(string word, ContentManager content)
        {
            SpriteFont font = content.Load<SpriteFont>("EnemyWord");
            Vector2 fontSize = font.MeasureString(word);
            switch (fontSize.X)
            {
                case float n when n >= 0 && n <= 10: // k
                    return 0;
                case float n when n > 10 && n <= 60: // whens
                    return 1;
                case float n when n > 60 && n <= 120: // me and you
                    return 2;
                default:
                    return 3;
            }

        }

        /// <summary>
        /// Calculates a word's difficulty based on several criteria
        /// </summary>
        /// <param name="word">word to get difficulty of</param>
        /// <returns>difficulty (int)</returns>
        static int GetWordDifficulty(string word)
        {
            int difficulty = 0;
            if (word.Length > 2)
            {
                difficulty++;
            }
            if (word.Length > 2 && word.Length <= 6)
            {
                difficulty++;
            }
            if (word.Length > 6)
            {
                difficulty++;
            }
            if (word.Any(char.IsDigit))
            {
                difficulty++;
            }
            if (word.Any(ch => !Char.IsLetterOrDigit(ch)))
            {
                difficulty++;
            }
            return difficulty;
        }
    }
}

