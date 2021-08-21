using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using type_to_win.Sprites;
using type_to_win.Sprites.Enemies;
using type_to_win.Levels;
using type_to_win.Database;
using System.Linq;

namespace type_to_win.Levels
{
    /// <summary>
    /// Class for level1
    /// </summary>
    public class Level1 : Level
    {
        const int ENEMY_COUNT = 25;

        public Level1(Rectangle cameraSize)
            : base(cameraSize, Difficulty.HARD, 4)
        {

        }

        public override void Initialize()
        {
            // define background image
            BackgroundImage = new BackgroundImage("underwater_background", new Vector2(378, 252), CameraSize);

            base.Initialize();
        }

        /// <summary>
        /// Creates list of enemies for a level using an element of randomness
        /// </summary>
        /// <returns>List of Enemy objects</returns>
        public override List<Enemy> GetEnemies()
        {
            List<Enemy> enemies = new List<Enemy>();
            int spawnAtTick = 0;                   // starting tick
            int interval = 1;                      // tick interval for enemy groupings
            int threshold = 250;                   // how many ticks between groups of enemies
            int maxNumberOfEnemiesInTickGroup = 4; // how many enemies max can be in one group of ticks
            Dictionary<int, int> tickCount = new Dictionary<int, int>(); // keeps track of number of enemies for each tick

            // each loop creates a new enemy up to the total number of enemies defined in ENEMY_COUNT
            for (int i = 0; i < ENEMY_COUNT; i++)
            {
                // decide which enemy type will be created
                int enemyNumber = new Random().Next(0, 3); // three enemy choices
                Enemy enemy;

                switch(enemyNumber)
                {
                    case 0:
                        enemy = new SharkEnemy(validPositionBounds: CameraSize);
                        break;
                    case 1:
                        enemy = new EelEnemy(validPositionBounds: CameraSize);
                        break;
                    case 2:
                    default:
                        enemy = new JellyfishEnemy(validPositionBounds: CameraSize);
                        break;
                }

                // define max start position y that an enemy can have based on camera bounds and sprite size (don't want sprite going off screen)
                int maxStartPositionY = (int)((enemy.ValidPositionBounds.Y + enemy.ValidPositionBounds.Height) - enemy.SpriteSheet.SpriteSize.Y);

                // set enemy position with the y position being randomly generated between 0 and max y position
                enemy.Position = new Vector2(START_POSITION_X, new Random().Next(0, maxStartPositionY));
                enemy.SpawnAtTick = spawnAtTick;

                // add enemy to enemies list
                enemies.Add(enemy);

                // calculate next spawn tick for next enemy
                // also determine if there are too many enemies in one group
                spawnAtTick += new Random().Next(0, threshold); // randomly add a number between 0 and tick grouping value to existing spawn at tick

                // add to dictionary the tick and the number of enemies in that tick
                if (!tickCount.ContainsKey(spawnAtTick))
                {
                    tickCount[spawnAtTick] = 1;
                }
                else
                {
                    tickCount[spawnAtTick]++;
                }

                // check range of ticks in dictionary tickCount to see how many enemies are in current tick grouping
                int tickRangeCount = 0;
                for (int j = interval * threshold; j < interval * threshold + threshold; j++)
                {
                    if (tickCount.ContainsKey(j)) {
                        tickRangeCount += tickCount[j];
                    }
                }

                // if too many enemies in one tick grouping, create new tick grouping
                if (tickRangeCount >= maxNumberOfEnemiesInTickGroup)
                {
                    spawnAtTick += threshold;
                    tickCount.Clear();
                    interval++;
                }
            }
            return enemies;
        }

        /// <summary>
        /// Loads words for enemies in level
        /// </summary>
        public override void LoadEnemyWords()
        {
            // create instance of database handler for words table
            LevelWordsDatabaseHandler levelWordsDatabaseHandler = new LevelWordsDatabaseHandler();
          
            // get all LevelWords less than or equal to threshold level difficulty
            List<LevelWord> levelWords = levelWordsDatabaseHandler.GetLevelWords((int)MaxWordDifficulty);

            // for each enemy, assign a word from the level appropriate list of words
            foreach (Enemy enemy in LevelEnemiesHandler.Enemies)
            {
                // get size appropriate words for specific enemy
                List<LevelWord> sizeAppropriateWords = levelWords.Where(levelWord => levelWord.Size <= (int)enemy.EnemySize).ToList();

                // choose random word from size appropriate words for enemy as well as number of words enemy has
                List<LevelWord> chosenRandomWords = new List<LevelWord>();
                int numberOfWords = new Random().Next(1, 9) != 8 ? 1 : 2;
                for (int i = 0; i < numberOfWords; i++)
                {
                    LevelWord levelWord = sizeAppropriateWords.ElementAt(new Random().Next(0, sizeAppropriateWords.Count));
                    chosenRandomWords.Add(levelWord);
                    sizeAppropriateWords.Remove(levelWord);
                }

                // finally set enemy words
                enemy.SetEnemyWords(chosenRandomWords.Select(levelWord => levelWord.Word).ToArray());
            }
        }


        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

    }
}
