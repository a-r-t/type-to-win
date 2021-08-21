using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using type_to_win.Levels;

namespace type_to_win.Sprites.Enemies
{
    public class JellyfishEnemy : Enemy
    {
        private Direction currentDirection;
        private int tickCounter;

        public JellyfishEnemy(Rectangle validPositionBounds, int spawnAtTick = 0, int startPositionX = 0, int startPositionY = 0)
            : base(validPositionBounds, "jellyfish", new Vector2(32, 40), EnemySize.TINY, spawnAtTick: spawnAtTick, startPositionX: startPositionX, startPositionY: startPositionY)
        {
            if (startPositionY > (validPositionBounds.Y + validPositionBounds.Height) / 2)
            {
                currentDirection = Direction.UP;
            }
            else
            {
                currentDirection = Direction.DOWN;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            CurrentAnimationName = "move";
        }

        public override void LoadAnimations()
        {
            // define animation and each frame in animation to be added to Sprite's Animations dictionary
            Animations.Add("move", new Animation(0, new Frame[] {
                new Frame(0),
                new Frame(1),
                new Frame(2),
                new Frame(3),
                new Frame(4),
                new Frame(5)
            }, 10));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override Vector2 GetMovement()
        {
            Vector2 tempPosition = new Vector2(Position.X, Position.Y);
            Vector2 movement = new Vector2(0, 0);

            if (tickCounter > 30)
            {
                if (currentDirection == Direction.UP)
                {
                    currentDirection = Direction.DOWN;
                }
                else
                {
                    currentDirection = Direction.UP;
                }
                tickCounter = 0;
            }

            if (currentDirection == Direction.UP)
            {
                tempPosition = new Vector2(tempPosition.X - 1, tempPosition.Y - 1);
                if (tempPosition.Y >= ValidPositionBounds.Y)
                {
                    movement = new Vector2(-1.5f, -1);
                    tickCounter++;
                }
                else
                {
                    currentDirection = Direction.DOWN;
                    movement = new Vector2(-1.5f, 1);
                    tickCounter = 0;
                }
            }
            else if (currentDirection == Direction.DOWN)
            {
                tempPosition = new Vector2(tempPosition.X - 1, tempPosition.Y + 1);
                if (tempPosition.Y + SpriteSheet.SpriteSize.Y <= ValidPositionBounds.Y + ValidPositionBounds.Height)
                {
                    movement = new Vector2(-1.5f, 1);
                    tickCounter++;
                }
                else
                {
                    currentDirection = Direction.UP;
                    movement = new Vector2(-1.5f, -1);
                    tickCounter = 0;
                }
            }
            return movement;
        }

        public override Vector2 GetWordPosition()
        {
            // centers text SpriteFont within sprite
            Vector2 fontSize = CurrentWord().Font.MeasureString(CurrentWord().GetTextWithColorsStripped());
            Vector2 middlePoint = new Vector2(Position.X + Dimensions.X / 2, Position.Y + Dimensions.Y / 2);
            Vector2 textMiddlePoint = new Vector2(fontSize.X / 2, fontSize.Y / 2);
            return new Vector2((int)(middlePoint.X - textMiddlePoint.X), (int)(middlePoint.Y - textMiddlePoint.Y));
        }
    }
}
