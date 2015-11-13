using System;
using Microsoft.Xna.Framework;

namespace CarpMuffin.Animations
{
    public class Tween
    {
        private int _dir;

        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public double Value { get; set; }
        public double Duration { get; set; }
        public double Time { get; private set; }

        public bool IsRunning { get; private set; }
        public bool IsRepeating { get; set; }
        public bool SinousRepeating { get; set; }

        public Action<Tween> OnComplete { get; set; }

        public Tween()
        {
            Minimum = 0;
            Maximum = 1;
            Value = 0;
            Duration = 1000;
            IsRepeating = false;
            IsRunning = true;
        }

        public Tween(double min, double max, double duration, bool isRepeating = false)
        {
            Minimum = min;
            Maximum = max;
            Value = Minimum;
            Duration = duration;
            IsRepeating = isRepeating;
            IsRunning = true;
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Reset()
        {
            Value = 0;
            Time = 0f;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsRunning) return;
            if (SinousRepeating)
            {
                if (_dir == 0) Time += gameTime.ElapsedGameTime.TotalMilliseconds;
                else Time -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                Time += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (!SinousRepeating) Time = MathHelper.Min((float)Time, (float)Duration);

            Value = (Time / Duration) * Maximum;
            Value = MathHelper.Clamp((float)Value, (float)Minimum, (float)Maximum);

            if (Time >= Duration)
            {
                Stop();
                OnComplete?.Invoke(this);
            }
            if (IsRepeating && Time >= Duration)
            {
                if (!SinousRepeating) Reset();
                else
                {
                    _dir = _dir == 0 ? 1 : 0;
                }
                OnComplete?.Invoke(this);
                Start();
            }
            else if (IsRepeating && Time < 0)
            {
                if (!SinousRepeating) Reset();
                else
                {
                    _dir = _dir == 0 ? 1 : 0;
                }
                OnComplete?.Invoke(this);
                Start();
            }
        }
    }
}