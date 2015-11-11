using System;
using CarpMuffin.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Sprites
{
    /// <summary>
    /// Displays an animated image on the screen
    /// </summary>
    public class AnimatedSprite
        : Sprite
    {
        private readonly Timer _timer;

        public int FrameCount { get; set; }
        public int CurrentFrame { get; set; }
        public int FrameLength { get; set; }

        public Action OnComplete { get; set; }

        public AnimatedSprite(Texture2D texture, Vector2 size, int frameCount, int frameLength)
            : base(texture, size)
        {
            FrameCount = frameCount;
            FrameLength = frameLength;

            _timer = new Timer
            {
                Length = frameLength
            };
            _timer.OnComplete += Tick;
        }

        public AnimatedSprite(SpriteSheet spriteSheet, int frameCount, int frameLength)
            : base(spriteSheet)
        {
            FrameCount = frameCount;
            FrameLength = frameLength;

            _timer = new Timer
            {
                Length = frameLength
            };
            _timer.OnComplete += Tick;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsEnabled) return;
            _timer.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsVisible) return;
            if (Texture == null) return;
            spriteBatch.Draw(Texture, Bounds, SourceRectangle, Tint);
        }

        protected virtual void Tick(Timer timer)
        {
            timer.Reset();
            CurrentFrame++;
            if (CurrentFrame >= FrameCount)
            {
                CurrentFrame = 0;
                OnComplete?.Invoke();
            }
            Index = CurrentFrame;
        }

        public void Start()
        {
            IsEnabled = true;
        }

        public void Stop()
        {
            IsEnabled = false;
            CurrentFrame = 0;
            Index = CurrentFrame;
        }

        public void Pause()
        {
            IsEnabled = false;
        }

        public void Resume()
        {
            Start();
        }

        public void ResetTimer(int frameLength)
        {
            _timer.Length = frameLength;
        }
    }
}