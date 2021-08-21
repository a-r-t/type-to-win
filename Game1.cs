using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using type_to_win.Database;
using type_to_win.Levels;

namespace type_to_win
{
    /// <summary>
    /// Main Game Loop
    /// </summary>
    public class Game1 : Game
    {
        // define content manager and spritebatch drawer
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // define level1 (temporarily here, later will be refactored out)
        LevelManager levelManager;
        SaveInformation saveInformation;

        /// <summary>
        /// Sets up Game1 class
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
        }

        /// <summary>
        /// Initialize any objects as needed before main game loop cycle begins
        /// </summary>
        protected override void Initialize()
        {
            // ensure code has reached initialize method
            Logger<Game1>.Log(this, Initialize);

            // get save information in order to load in correct level
            saveInformation = readInSaveInformation();

            if (!File.Exists(ConfigurationManager.AppSettings["databaseFile"]))
            {
                SetupDatabase.SetUp(Content);
            }

            // create level manager object which handles loading in the proper level
            levelManager = new LevelManager(GraphicsDevice.PresentationParameters.Bounds, Content, currentLevelIndex: saveInformation.CurrentLevelIndex);
            base.Initialize();
        }

        /// <summary>
        /// Tries to parse an existing save.json file to SaveInformation object
        /// If it fails, will create a "new game" save configuration
        /// </summary>
        /// <returns>SaveInformation object</returns>
        public SaveInformation readInSaveInformation()
        {
            SaveInformation saveInformation;

            try
            {
                /*
                using (StreamReader file = File.OpenText(@"save.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    SaveInformation saveInformation2 = (SaveInformation)serializer.Deserialize(file, typeof(SaveInformation));
                }
                */

                // try to read in an already existing valid save.json file
                // and parse it to a SaveInformation object
                saveInformation = JsonConvert.DeserializeObject<SaveInformation>(File.ReadAllText("save.json"));
            }
            catch (Exception ex)
            {
                String errorMessage = "";
                // if file save.json not found...
                if (ex is FileNotFoundException)
                {
                    errorMessage = "Save file not found";
                }
                // if file save.json is an invalid json file
                else if (ex is JsonReaderException)
                {
                    errorMessage = "Save file found but is invalid JSON";
                }
                // if unknown error...
                else
                {
                    errorMessage = "Something unexpected went wrong";
                }
                Console.WriteLine(String.Format("{0} -- creating new save file...:\n{1}", errorMessage, ex.ToString()));

                // create "new game" save configuration file
                saveInformation = SaveInformation.getEmptySaveInformation();
                File.WriteAllText("save.json", JsonConvert.SerializeObject(saveInformation));
            }
            return saveInformation;
        }

        /// <summary>
        /// Load any content as needed before main game loop cycle begins
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Main Game Loop cycles between Update and Draw functions until termination
        /// All GameObjects have their own Update functions
        /// Update is used to handle all logic for a GameObject
        /// </summary>
        /// <param name="gameTime">delta time is used when doing anything time related in order to keep game consistent for computers with different frame rates</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            levelManager.Update(gameTime);

            if (levelManager.CurrentLevelIndex > saveInformation.CurrentLevelIndex)
            {
                saveInformation.CurrentLevelIndex = levelManager.CurrentLevelIndex;
                File.WriteAllText("save.json", JsonConvert.SerializeObject(saveInformation));

                /*
                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(@"save.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, saveInformation);
                }
                */
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Main Game Loop cycles between Update and Draw functions until termination
        /// All GameObjects have their own Draw functions
        /// Draw is used to handle physically drawing a Sprite to screen
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // setsup spritebatch for 2D sprites
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            levelManager.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}