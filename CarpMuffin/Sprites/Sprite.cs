using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Sprites
{
    /// <summary>
    /// Displays an image on the screen
    /// </summary>
    public class Sprite
        : ISprite
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public SpriteSheet SpriteSheet { get; set; }
        public int Index { get; set; }
        public Color Tint { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; }
        public SpriteEffects SpriteEffects { get; set; }

        public Texture2D Texture => SpriteSheet?.Texture;

        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        public Rectangle SourceRectangle => SpriteSheet?[Index] ?? new Rectangle(0, 0, (int)Size.X, (int)Size.Y);

        public Sprite()
        {
            Size = Vector2.One;
            Initialize();
        }

        public Sprite(Texture2D texture, Vector2 size)
        {
            Size = size;
            SpriteSheet = new SpriteSheet(texture, size);
            Initialize();
        }

        public Sprite(SpriteSheet spriteSheet)
        {
            Size = spriteSheet.SpriteSize;
            SpriteSheet = spriteSheet;
            Initialize();
        }

        public Sprite(Texture2D texture)
        {
            Size = new Vector2(texture.Width, texture.Height);
            SpriteSheet = new SpriteSheet(texture, Size);
            Initialize();
        }

        public virtual void Initialize()
        {
            IsEnabled = true;
            IsVisible = true;
            Position = Vector2.Zero;
            Index = 0;
            Tint = Color.White;
            Rotation = 0f;
            Origin = Vector2.Zero;
            Scale = Vector2.One;
            SpriteEffects = SpriteEffects.None;
            Name = Guid.NewGuid().ToString();
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!IsEnabled) return;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!IsVisible) return;
            if (Texture == null) return;
            spriteBatch.Draw(Texture, null, Bounds, SourceRectangle, Origin, Rotation, Scale, Tint, SpriteEffects);
        }
    }
}