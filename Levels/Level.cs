using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using type_to_win.Interfaces;
using type_to_win.Sprites;
using type_to_win.Levels;
using type_to_win.Sprites.Enemies;

namespace type_to_win.Levels
{
    /// <summary>
    /// Base class for a Level
    /// Handles the core gameplay logic -- typing and validating
    /// </summary>
    public abstract class Level : GameObject
    {
        public Rectangle CameraSize { get; private set; }                   // size of camera bounds (as of now is the entire window)
        public int CurrentTick { get; set; }                                // level "tick" counter -- this for now emulates the passage of time and each enemy spawns at different ticks
        public LevelEnemiesHandler LevelEnemiesHandler { get; set; }        // all enemies in a level
        public BackgroundImage BackgroundImage { get; set; }                // background image for level
        public Difficulty MaxWordDifficulty { get; set; }
        public const int START_POSITION_X = 800;
        public int Lives { get; set; }
        public int LivesLeft { get; set; }
        public Hud Hud { get; set; }

        /// <summary>
        /// Level Constructor
        /// </summary>
        /// <param name="cameraSize">Size of camera -- basically the playable game area bounds</param>
        public Level(Rectangle cameraSize, Difficulty maxWordDifficulty, int lives)
        {
            CameraSize = cameraSize;
            CurrentTick = 0;
            LevelEnemiesHandler = new LevelEnemiesHandler();
            MaxWordDifficulty = maxWordDifficulty;
            Lives = lives;
            LivesLeft = Lives;
            Hud = new Hud();
        }

        public GameState GetGameState()
        {
            if (LivesLeft <= 0)
            {
                return GameState.LOST;
            }
            else if (LevelEnemiesHandler.AreAllEnemiesDead())
            {
                return GameState.WON;
            }
            else
            {
                return GameState.IN_PROGRESS;
            }
        }

        public virtual void Initialize()
        {
            DefineEnemies();

            LevelEnemiesHandler.Initialize();
            Hud.Initialize();
        }

        public void DefineEnemies()
        {
            // define LevelEnemies for Level1
            LoadEnemies();

            LoadEnemyWords();
        }

        public virtual void LoadContent(ContentManager content)
        {
            // load content into pipeline for each enemy in level
            LevelEnemiesHandler.LoadContent(content);
            BackgroundImage.LoadContent(content);
            Hud.LoadContent(content);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (GetGameState() == GameState.IN_PROGRESS)
            {
                // get keyboard state
                KeyboardState keyboardState = Keyboard.GetState();

                // run update for every active enemy 
                LevelEnemiesHandler.Update(gameTime, CurrentTick, keyboardState);

                LevelEnemiesHandler.Enemies.ForEach(enemy =>
                {
                    if (enemy.Position.X <= enemy.ValidPositionBounds.X && !enemy.HasAttacked)
                    {
                        enemy.HasAttacked = true;
                        LivesLeft -= 1;
                    }
                });

                // increment tick counter
                CurrentTick++;

                Hud.Update(gameTime, LivesLeft);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // draw background image
            BackgroundImage.Draw(gameTime, spriteBatch);

            Hud.Draw(gameTime, spriteBatch);

            LevelEnemiesHandler.Draw(gameTime, spriteBatch, CurrentTick);
        }

        /// <summary>
        /// Loads in LevelEnemy objects for Level1
        /// </summary>
        public void LoadEnemies()
        {
            LevelEnemiesHandler.Enemies = GetEnemies();
        }

        public abstract List<Enemy> GetEnemies();

        public abstract void LoadEnemyWords();
    }
}
