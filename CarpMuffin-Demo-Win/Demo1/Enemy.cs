using System;
using CarpMuffin;
using CarpMuffin.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin_Demo_Win.Demo1
{
    public class Enemy
        : Sprite
    {
        public float Speed { get; set; }
        public int MovementDirection { get; set; }
        public string Group { get; set; }
        public Action<Enemy> LeftViewportCollision { get; set; }
        public Action<Enemy> RightViewportCollision { get; set; }

        public Enemy() { }

        public Enemy(Texture2D texture)
            : base(texture)
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();

            Speed = 1f;

            MovementDirection = 1;
        }

        public override void Update(GameTime gameTime)
        {
            var viewport = Engine.Instance.Graphics.GraphicsDevice.Viewport;
            base.Update(gameTime);

            Position += new Vector2(Speed * MovementDirection, 0f);

            if (MovementDirection == -1 && Position.X <= 0)
            {
                MovementDirection = 1;
                Position = new Vector2(0f, Position.Y + Speed);
                LeftViewportCollision?.Invoke(this);
            }
            else if (MovementDirection == 1 && Position.X + Size.X >= viewport.Width)
            {
                MovementDirection = -1;
                Position = new Vector2(viewport.Width - Size.X, Position.Y + Speed);
                RightViewportCollision?.Invoke(this);
            }
        }
    }
}