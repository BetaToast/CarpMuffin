using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Tiles
{
    /// <summary>
    /// Displays a tile on the screen
    /// </summary>
    public class Tile
        : ITile
    {
        public bool IsEnabled { get; set; }
        public bool IsVisible { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public int Index { get; set; }
        public Color Tint { get; set; }
        public TileSheet TileSheet { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        public Texture2D Texture => TileSheet?.Texture;

        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        public Rectangle SourceRectangle => TileSheet?[Index] ?? new Rectangle(0, 0, (int)Size.X, (int)Size.Y);

        public Tile(TileSheet tilesheet)
        {
            TileSheet = tilesheet;
            IsEnabled = true;
            IsVisible = true;
            Tint = Color.White;
            Index = -1;
            Size = tilesheet.SpriteSize;
        }

        public virtual void Update(GameTime gameTime)
        {
            //if (!IsEnabled) return;
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (!IsVisible) return;
            SpriteBatch.Draw(Texture, null, Bounds, SourceRectangle, Vector2.Zero, 0f, Vector2.One, Tint);
        }
    }
}