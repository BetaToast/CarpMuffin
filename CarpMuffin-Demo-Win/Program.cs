using System;
using CarpMuffin.Graphics;
using Microsoft.Xna.Framework;

namespace CarpMuffin_Demo_Win
{
#if WINDOWS || LINUX
    public static class Program
    {
        public static Camera2D Camera { get; set; }
        public static Vector2 TileSize => new Vector2(32f, 32f);

        [STAThread]
        static void Main()
        {
            using (var game = new WindowsDemoGame())
                game.Run();
        }
    }
#endif
}
