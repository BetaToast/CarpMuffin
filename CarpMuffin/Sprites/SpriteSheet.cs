using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Sprites
{
    public class SpriteSheet
    {
        private readonly List<Rectangle> _bounds = new List<Rectangle>();
        private readonly Dictionary<string, int> _aliases = new Dictionary<string, int>();

        public Texture2D Texture { get; private set; }
        public Vector2 SpriteSize { get; private set; }

        public int Count => _bounds.Count;

        public Rectangle this[int index] => _bounds[index];

        public Rectangle this[string alias]
        {
            get
            {
                if (!_aliases.ContainsKey(alias)) return Rectangle.Empty;
                var index = _aliases[alias];
                return _bounds[index];
            }
        }

        public SpriteSheet(Texture2D texture, Vector2 spriteSize)
        {
            Texture = texture;

            SpriteSize = spriteSize;

            var spriteWidth = (int)spriteSize.X;
            var spriteHeight = (int)spriteSize.Y;
            var w = texture.Width / spriteWidth;
            var h = texture.Height / spriteHeight;

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    var rect = new Rectangle(x * spriteWidth, y * spriteHeight, (int)spriteSize.X, (int)spriteSize.Y);
                    _bounds.Add(rect);
                }
            }
        }

        public void AddAlias(string alias, int index)
        {
            _aliases.Add(alias, index);
        }
    }
}