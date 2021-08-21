using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using type_to_win.Sprites.Enemies;

namespace type_to_win.Levels
{
    public class LevelEnemiesHandler
    {
        public List<Enemy> Enemies { get; set; }               // all enemies in a level
        public HashSet<Keys> LockedKeys { get; private set; }  // keys that cannot be pressed until the key has been let go of

        public LevelEnemiesHandler()
        {
            Enemies = new List<Enemy>();
            LockedKeys = new HashSet<Keys>();
        }

        public virtual void Initialize()
        {
            Enemies.ForEach(levelEnemy => levelEnemy.Initialize());
        }

        public virtual void LoadContent(ContentManager content)
        {
            Enemies.ForEach(levelEnemy => levelEnemy.LoadContent(content));
        }

        public virtual void Update(GameTime gameTime, int currentTick, KeyboardState keyboardState)
        {
            Keys[] pressedKeys = keyboardState.GetPressedKeys();

            // unlock keys if the key is up (no longer being held down)
            LockedKeys.RemoveWhere(key => keyboardState.IsKeyUp(key));

            // loop through each currently pressed key
            foreach (Keys pressedKey in pressedKeys)
            {
                /*
                 * If key pressed is not None and is not currently locked (meaning it was already counted as pressed last tick)
                 * check if key press successfully harms an enemy
                 * Keys will lock after being pressed, and will unlock after being released
                 * (prevents holding down a key from counting as a key press for multiple ticks) 
                 */
                if (pressedKey != Keys.None && !LockedKeys.Contains(pressedKey))
                {
                    Console.WriteLine(pressedKey.ToString()); // log key pressed

                    AttackEnemy(currentTick, pressedKey);

                    // add key pressed to locked keys
                    LockedKeys.Add(pressedKey);
                }
            }

            Enemies.ForEach(enemy => {
                if (enemy.IsActive(currentTick))
                {
                    enemy.Update(gameTime);
                }
            });
        }

        public void AttackEnemy(int currentTick, Keys pressedKey)
        {
            /*
             * If there is a current enemy being targetted
             * See if key pressed will successfully match with its word's next character
             */
            Enemy targetEnemy = GetTargetEnemy(currentTick, pressedKey);
            if (targetEnemy != null)
            {
                // get character translation of key press
                char characterTyped = KeyToCharTranslate.GetCharFromKey(pressedKey);

                // validate key pressed against enemies word
                bool successfulAttack = targetEnemy.CurrentWord().ValidateTypedCharacter(characterTyped);

                if (successfulAttack)
                {
                    Vector2 knockback = new Vector2(5, 0);
                    targetEnemy.Position += knockback;
                    targetEnemy.CurrentWord().Position += knockback;
                }

                // if this key pressed finshed off the enemy's current word, enemy is no longer the targetted enemy
                if (targetEnemy.CurrentWord().IsWordTypedSuccessfully())
                {
                    targetEnemy.IsTargetEnemy = false;
                }
            }
        }

        /// <summary>
        /// Gets the targetted enemy that is currently being "fought" through typing
        /// </summary>
        /// <returns>LevelEnemy being targetted currently</returns>
        public Enemy GetTargetEnemy(int currentTick, Keys pressedKey)
        {
            /* Finds enemy that is currently active AND is currently targetted, 
             * if none match criteria it will return null
             */
            Enemy targetEnemy = Enemies.Find(levelEnemy => levelEnemy.IsActive(currentTick) && levelEnemy.IsTargetEnemy);
            if (targetEnemy != null && !targetEnemy.IsActive(currentTick))
            {
                targetEnemy.IsTargetEnemy = false;
                targetEnemy = null;
            }

            if (targetEnemy == null)
            {

                // find an active enemy whose first letter of its first word matches the pressed key
                List<Enemy> matchingEnemies = GetActiveEnemiesWithMatchingFirstCharacter(currentTick, pressedKey);

                if (matchingEnemies.Count > 0)
                {
                    targetEnemy = GetClosestEnemyToEnd(matchingEnemies);

                    // if a matching enemy was found, set it as the targetted enemy
                    targetEnemy.IsTargetEnemy = true;

                    /*
                     * set targetted enemy to last in enemy array 
                     * to ensure it will always be drawn on top of any other enemy sprite
                     * draw order has anything drawn after something else overlap
                     */
                    PrioritizeEnemyDrawOrder(targetEnemy);
                }
            }
            return targetEnemy;
        }

        private List<Enemy> GetActiveEnemiesWithMatchingFirstCharacter(int currentTick, Keys pressedKey)
        {
            // find an active enemy whose first letter of its first word matches the pressed key
            return Enemies.FindAll(levelEnemy =>
                levelEnemy.IsActive(currentTick) &&
                Char.ToLower(levelEnemy.CurrentWord().CurrentCharacter()) == Char.ToLower(KeyToCharTranslate.GetCharFromKey(pressedKey))
            );
        }

        private Enemy GetClosestEnemyToEnd(List<Enemy> levelEnemies)
        {
            Enemy closestEnemy = null;
            levelEnemies.ForEach(levelEnemy =>
            {
                if (closestEnemy == null || levelEnemy.Position.X < closestEnemy.Position.X)
                {
                    closestEnemy = levelEnemy;
                }
            });
            return closestEnemy;
        }

        private void PrioritizeEnemyDrawOrder(Enemy prioritizedLevelEnemy)
        {
            int levelEnemyIndex = Enemies.FindIndex(levelEnemy => levelEnemy == prioritizedLevelEnemy);
            Enemies.RemoveAt(levelEnemyIndex);
            Enemies.Add(prioritizedLevelEnemy);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, int currentTick)
        {
            Enemies.ForEach(levelEnemy => {
                if (levelEnemy.IsActive(currentTick))
                {
                    levelEnemy.Draw(gameTime, spriteBatch);
                }
            });
        }

        public Boolean AreAllEnemiesDead()
        {
            return Enemies.All(enemy => enemy.IsDead());
        }
    }
}
