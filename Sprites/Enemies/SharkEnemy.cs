using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace type_to_win.Sprites.Enemies
{
    /// <summary>
    /// Shark Enemy Class
    /// </summary>
    public class SharkEnemy : Enemy
    {
        /// <summary>
        /// Shark Enemy Constructor
        /// </summary>
        public SharkEnemy(Rectangle validPositionBounds, int spawnAtTick = 0, int startPositionX = 0, int startPositionY = 0)
            : base(validPositionBounds, "shark", new Vector2(256, 90), EnemySize.MEDIUM, spawnAtTick: spawnAtTick, startPositionX: startPositionX, startPositionY: startPositionY)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            // set current animation
            CurrentAnimationName = "move";
        }

        /// <summary>
        /// Loads Animations for Enemy animated sprite
        /// </summary>
        public override void LoadAnimations()
        {
            // define animation and each frame in animation to be added to Sprite's Animations dictionary
            Animations.Add("move", new Animation(0, new Frame[] {
                new Frame(2, spriteEffect: SpriteEffects.FlipHorizontally),
                new Frame(1, spriteEffect: SpriteEffects.FlipHorizontally),
                new Frame(0, spriteEffect: SpriteEffects.FlipHorizontally),
                new Frame(1, spriteEffect: SpriteEffects.FlipHorizontally),
                new Frame(2, spriteEffect: SpriteEffects.FlipHorizontally),
                new Frame(3, spriteEffect: SpriteEffects.FlipHorizontally),
                new Frame(4, spriteEffect: SpriteEffects.FlipHorizontally),
                new Frame(3, spriteEffect: SpriteEffects.FlipHorizontally)
            }, 5));
        }

        /// <summary>
        /// Defines movement for SharkEnemy, which is to slowly move from right to left
        /// </summary>
        /// <returns>movement vector</returns>
        public override Vector2 GetMovement()
        {
            // slowly moves from right to left
            return new Vector2(-1, 0);
        }

        /// <summary>
        /// Gets spritefont word position on SharkEnemy's sprite
        /// </summary>
        /// <returns>position vector</returns>
        public override Vector2 GetWordPosition()
        {
            // centers text SpriteFont within sprite
            Vector2 fontSize = CurrentWord().Font.MeasureString(CurrentWord().GetTextWithColorsStripped());
            Vector2 middlePoint = new Vector2(Position.X + Dimensions.X / 2, Position.Y + Dimensions.Y / 2);
            Vector2 textMiddlePoint = new Vector2(fontSize.X / 2, fontSize.Y / 2);
            return new Vector2((int)(middlePoint.X - textMiddlePoint.X), (int)(middlePoint.Y - textMiddlePoint.Y) + 5);
        }
    }
}
