using CarpMuffin.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.UserInterfaces.Controls
{
    /// <summary>
    /// Base for all controls
    /// </summary>
    public abstract class Control
        : IControl
    {
        public bool IsEnabled { get; set; }
        public bool IsVisible { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public Texture2D Texture { get; set; }
        public UserInterfaceSkin Skin { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Color Tint { get; set; }
        public SpriteFont Font { get; set; }

        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        protected Control()
        {
            Position = Vector2.Zero;
            Size = Vector2.One;
            IsEnabled = true;
            IsVisible = true;
            Tint = Color.White;
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void UpdateInput(InputManager input) { }

        public virtual void Draw(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public abstract void LoadParts();
    }
}