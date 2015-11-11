using CarpMuffin.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Tiles
{
    /// <summary>
    /// Base interface for all tiles
    /// </summary>
    public interface ITile
        : IRenderable
    {
        Vector2 Position { get; set; }
        Vector2 Size { get; set; }
        int Index { get; set; }
        Color Tint { get; set; }
        Rectangle Bounds { get; }
        Rectangle SourceRectangle { get; }
        TileSheet TileSheet { get; set; }
        Texture2D Texture { get; }
        SpriteBatch SpriteBatch { get; set; }
    }
}