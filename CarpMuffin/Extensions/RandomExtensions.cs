using System;

namespace CarpMuffin.Extensions
{
    /// <summary>
    /// Random Extensions
    /// </summary>
    public static class RandomExtensions
    {
        public static double Next(this Random random, double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }

        public static float Next(this Random random, float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }
    }
}