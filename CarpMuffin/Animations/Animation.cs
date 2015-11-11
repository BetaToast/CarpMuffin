using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CarpMuffin.Animations
{
    public class Animation
    {
        private double _elapsed;

        public string Name { get; set; }
        public List<Rectangle> Frames { get; set; } = new List<Rectangle>();
        public int CurrentIndex { get; set; }
        public double FrameLength { get; set; }

        public Rectangle CurrentFrame => Frames[CurrentIndex];

        public Animation()
        {
            CurrentIndex = 0;
            FrameLength = 100;
        }

        public void Update(GameTime gameTime)
        {
            _elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapsed > FrameLength)
            {
                CurrentIndex++;
                _elapsed -= FrameLength;
            }

            if (CurrentIndex >= Frames.Count) CurrentIndex = 0;
        }
    }
}