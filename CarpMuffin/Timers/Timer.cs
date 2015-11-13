using System;
using CarpMuffin.Graphics;
using Microsoft.Xna.Framework;

namespace CarpMuffin.Timers
{
    /// <summary>
    /// Helps maintain and track timings
    /// </summary>
    public class Timer
        : IUpdatable
    {
        private double _elapsed;

        public bool IsEnabled { get; set; }
        public int Length { get; set; }
        public Action<Timer> OnComplete { get; set; }
        public Action OnTick { get; set; }
        public bool IsDisposed { get; private set; }

        public Timer()
        {
            IsEnabled = true;
            IsDisposed = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsEnabled) return;

            _elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            OnTick?.Invoke();

            if (!(_elapsed > Length)) return;
            IsDisposed = true;
            IsEnabled = false;
            OnComplete?.Invoke(this);
        }

        public void Start()
        {
            IsEnabled = true;
        }

        public void Stop()
        {
            IsEnabled = false;
        }

        public void Reset()
        {
            IsDisposed = false;
            IsEnabled = true;
            _elapsed = 0;
        }
    }
}