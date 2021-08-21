using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Sprites
{
    public class Animation
    {
        public int SpriteSheetRowNumber { get; set; }     // row number in spritesheet corresponding with this animation
        public bool IsLooping { get; set; }               // whether animation should loop (not implemented yet)
        public bool IsPlayOnce { get; set; }              // whether animation should play only once (not implemented yet)
        public Frame[] Frames { get; set; }               // Frames in animation
        public int Delay { get; set; }                    // How long each frame lasts before transitioning to next frame in animation

        /// <summary>
        /// Gets total number of Frames in animation
        /// </summary>
        /// <returns></returns>
        public int NumberOfFrames()
        {
            return Frames.Length;
        }

        /// <summary>
        /// Animation Constructor
        /// </summary>
        /// <param name="spriteSheetRowNumber">row number in spritesheet</param>
        /// <param name="frames">frames in animation</param>
        /// <param name="delay">how long each frame lasts before transitioning to next frame</param>
        public Animation(int spriteSheetRowNumber, Frame[] frames, int delay)
        {
            SpriteSheetRowNumber = spriteSheetRowNumber;
            Frames = frames;
            Delay = delay;
        }
    }
}
