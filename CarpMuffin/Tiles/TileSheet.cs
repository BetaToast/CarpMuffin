using CarpMuffin.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Tiles
{
    /// <summary>
    /// A collection of all the tiles on a texture
    /// </summary>
    public class TileSheet
        : SpriteSheet
    {
        public int MaxIndex => Count;

        public TileSheet(Texture2D texture, Vector2 tileSize)
            : base(texture, tileSize)
        {

        }
    }
}