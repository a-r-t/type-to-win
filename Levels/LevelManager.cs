using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using type_to_win.Interfaces;

namespace type_to_win.Levels
{
    public class LevelManager : GameObject
    {
        ContentManager Content { get; set; }
        public Level[] Levels { get; set; }            // levels in game
        public int CurrentLevelIndex { get; set; }     // current level index

        // Current Level player is on
        public Level CurrentLevel()
        {
            return Levels[CurrentLevelIndex];
        }

        public LevelManager(Rectangle cameraSize, ContentManager content, int currentLevelIndex = 0)
        {
            Content = content;

            // define game levels
            Levels = new Level[]
            {
                new Level1(cameraSize)
            };

            /*
             * Since currentLevelIndex is an item passed in from the save config, 
             * this prevents a player from tampering with it by putting a number higher than the total number of levels in the game
             * which would cause a indexoutofbounds exception or the like
             */
            CurrentLevelIndex = currentLevelIndex <= Levels.Length - 1 ? currentLevelIndex : Levels.Length - 1;
            CurrentLevel().Initialize();
            CurrentLevel().LoadContent(Content);
        }

        public void ResetCurrentLevel()
        {
            CurrentLevel().LivesLeft = CurrentLevel().Lives;
            CurrentLevel().DefineEnemies();
            CurrentLevel().LevelEnemiesHandler.Initialize();
            CurrentLevel().LevelEnemiesHandler.LoadContent(Content);
            CurrentLevel().CurrentTick = 0;
        }

        public void Initialize()
        {
        }

        public void LoadContent(ContentManager content)
        {
        }

        public void Update(GameTime gameTime)
        {
            CurrentLevel().Update(gameTime);
            GameState gameState = CurrentLevel().GetGameState();
            if (gameState == GameState.WON)
            {
                CurrentLevelIndex++;
                CurrentLevel().Initialize();
                CurrentLevel().LoadContent(Content);
            }
            else if (gameState == GameState.LOST)
            {
                ResetCurrentLevel();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentLevel().Draw(gameTime, spriteBatch);
        }
    }
}
