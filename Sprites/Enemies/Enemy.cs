using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using type_to_win.Levels;

namespace type_to_win.Sprites.Enemies
{
    /// <summary>
    /// Enemy Class
    /// </summary>
    public abstract class Enemy : AnimatedSprite
    {
        /// <summary>
        /// 
        /// </summary>
        protected EnemyWord[] enemyWords;

        public void SetEnemyWords(string[] words)
        {
            // create EnemyWord objects out of the array of words associated with enemy
            enemyWords = new EnemyWord[words.Length];
            for (int i = 0; i < enemyWords.Length; i++)
            {
                enemyWords[i] = new EnemyWord(words[i], outlineColor: Color.Black);
            }
        }   
        public int CurrentWordIndex { get; set; }            // current word that is being displayed in level on enemy
        public Rectangle ValidPositionBounds { get; set; }   // bounds defines area where enemy is allowed to move on screen
        public int SpawnAtTick { get; set; }                 // at what tick enemy will spawn
        public bool IsTargetEnemy { get; set; }              // if enemy is the "target" enemy or not (currently being attacked)
        public EnemySize EnemySize { get; set; }             // size of enemy
        public bool HasAttacked { get; set; }

        /// <summary>
        /// Gets current word being displayed in level on enemy
        /// </summary>
        /// <returns>current EnemyWord object</returns>
        public EnemyWord CurrentWord()
        {
            return enemyWords[CurrentWordIndex];
        }

        /// <summary>
        /// Enemy constructor
        /// </summary>
        /// <param name="words">words associated with enemy</param>
        /// <param name="validPositionBounds">bounds at which enemy is allowed to move within</param>
        public Enemy(Rectangle validPositionBounds, string spriteSheetName, Vector2 spriteSize, EnemySize enemySize,
            int spawnAtTick = 0, int startPositionX = 0, int startPositionY = 0) 
            : base(spriteSheetName, spriteSize, startPositionX: startPositionX, startPositionY: startPositionY)
        {
            CurrentWordIndex = 0;
            ValidPositionBounds = validPositionBounds;
            SpawnAtTick = spawnAtTick;
            IsTargetEnemy = false;
            EnemySize = enemySize;
            HasAttacked = false;
        }

        /// <summary>
        /// Checks if enemy is active
        /// </summary>
        /// <param name="currentTick">CurrentTick that the level is on</param>
        /// <returns>if enemy is active or not (basically has it spawned yet and is it not dead)</returns>
        public bool IsActive(int currentTick)
        {
            return SpawnAtTick <= currentTick && !IsDead() && !HasAttacked;
        }

        /// <summary>
        /// Checks if enemy has been killed
        /// </summary>
        /// <returns>if enemy is killed or not</returns>
        public bool IsDead()
        {
            // if all words have been typed successfully, enemy is dead
            return Array.TrueForAll(enemyWords, Word => Word.IsWordTypedSuccessfully());
        }

        public override void Initialize()
        {
            // load animations
            LoadAnimations();

            Array.ForEach(enemyWords, word => word.Initialize());

            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            // loads sprite content
            base.LoadContent(content);

            // load spritefont content for each EnemyWord
            Array.ForEach(enemyWords, word => word.LoadContent(content));

            // Position spritefont in proper spot relative to enemy
            CurrentWord().Position = GetWordPosition();
        }

        public override void Update(GameTime gameTime)
        {
            // if CurrentWord has been typed, move on to the next word
            if (CurrentWord().IsWordTypedSuccessfully())
            {
                CurrentWordIndex += 1;
                CurrentWord().Position = GetWordPosition();
            }

            // if enemy is not dead
            if (!IsDead())
            {
                // enemy's move method is called each frame to see if movement will be necessary
                Vector2 movement = GetMovement(); 
                Position += movement;
                CurrentWord().Position += movement;
                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // draw enemy sprite
            base.Draw(gameTime, spriteBatch);

            // draw EnemyWord spritefont
            CurrentWord().Draw(gameTime, spriteBatch);
        }

        // each enemy class that extends this one will need to implement its own move logic, 
        // as all enemies will move around the screen differently
        public abstract Vector2 GetMovement();

        // the EnemyWord spritefont will need to be rendered in an appropriate location relative to the sprite
        public abstract Vector2 GetWordPosition();

        public abstract void LoadAnimations();
    }
}
