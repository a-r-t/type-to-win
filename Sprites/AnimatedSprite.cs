using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Sprites
{
    /// <summary>
    /// Base class for an Animated Sprite, which is a series of 2D images
    /// This class will be used heavily by anything that has an animated graphic
    /// Expected to get larger as time goes on and more draw options are needed
    /// </summary>
    public class AnimatedSprite : Sprite
    {
        public Dictionary<string, Animation> Animations { get; set; }   // Animations dictionary -- key: animation name, value: Animation object
        public string CurrentAnimationName { get; set; }                // Current Animation that sprite is on
        public int CurrentFrameIndex { get; set; }                      // Current Frame of Current Animation that sprite is on
        private int delayCounter;                                       // Keeps track of how many ticks have went by during each Frame of an animation

        /// <summary>
        /// Gets current animation that sprite is on
        /// </summary>
        /// <returns>Current Animation object</returns>
        public Animation CurrentAnimation()
        {
            return Animations[CurrentAnimationName];
        }
        /// <summary>
        /// Gets current frame of current animation that sprite is on
        /// </summary>
        /// <returns>Current Frame object</returns>
        public Frame CurrentFrame()
        {
            return CurrentAnimation().Frames[CurrentFrameIndex];
        }

        /// <summary>
        /// Animated Sprite Constructor
        /// Many of the other Sprite options (e.g. SpriteEffect, Scale) are not needed here as they will be stored as Frame object data
        /// </summary>
        /// <param name="spriteSheetName">name of spritesheet containing image</param>
        /// <param name="spriteSize">size of sprite on spritesheet (usually divisible by 8)</param>
        /// <param name="startPositionX">sprite's start X position on screen</param>
        /// <param name="startPositionY">sprite's start Y position on screen</param>
        public AnimatedSprite(string spriteSheetName, Vector2 spriteSize, int startPositionX = 0, int startPositionY = 0)
            : base(spriteSheetName, spriteSize, startPositionX: startPositionX, startPositionY: startPositionY)
        {
            Animations = new Dictionary<string, Animation>();
            CurrentFrameIndex = 0;
        }

        public override void Update(GameTime gameTime)
        {
            // set sprite attributes to current Frame's attributes
            SourceRectangle = SpriteSheet.GetSprite(
                CurrentAnimation().SpriteSheetRowNumber,
                CurrentFrame().SpriteSheetColumnNumber
            );
            SpriteEffect = CurrentFrame().SpriteEffect;
            Scale = CurrentFrame().Scale;

            base.Update(gameTime);

            // increase tick counter
            delayCounter++;

            // if tick counter is over Current Animation's delay timer, it's time to transition to next frame of Animation
            if (delayCounter >= CurrentAnimation().Delay)
            {
                delayCounter = 0;
                CurrentFrameIndex++;

                // if current frame is greater the total amount of frames, reset to first frame in animation
                if (CurrentFrameIndex >= CurrentAnimation().NumberOfFrames())
                {
                    CurrentFrameIndex = 0;
                }
            }
        }
    }
}
