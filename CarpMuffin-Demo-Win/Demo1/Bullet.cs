using CarpMuffin.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin_Demo_Win.Demo1
{
    public class Bullet
        : Sprite
    {
        public float Speed { get; set; }

        public bool IsAlive { get; set; }

        public Bullet() { }

        public Bullet(Texture2D texture)
            : base(texture)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            Speed = 4f;
            IsAlive = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!IsAlive) return;

            Position += new Vector2(0f, -Speed);

            if (Position.Y + Size.Y <= 0)
            {
                IsAlive = false;
            }
        }
    }
}