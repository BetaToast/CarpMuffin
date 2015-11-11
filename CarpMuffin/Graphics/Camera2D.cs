using Microsoft.Xna.Framework;

namespace CarpMuffin.Graphics
{
    /// <summary>
    /// Camera for a 2D plane
    /// </summary>
    public class Camera2D
    {
        private float _viewportHeight;
        private float _viewportWidth;

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public Vector2 CenterScreen { get; set; }
        public Matrix Transform { get; set; }
        public IFocusable Focus { get; set; }
        public float MoveSpeed { get; set; }

        public Camera2D()
        {
            Initialize();
        }

        private void Initialize()
        {
            var engine = Engine.Instance;
            _viewportWidth = engine.GraphicsDevice.Viewport.Width;
            _viewportHeight = engine.GraphicsDevice.Viewport.Height;

            CenterScreen = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            Scale = 1f;
            MoveSpeed = 1.25f;
        }

        public void Update(GameTime gameTime)
        {
            Transform = Matrix.Identity
                        * Matrix.CreateTranslation(-Position.X, -Position.Y, 0)
                        * Matrix.CreateRotationZ(Rotation)
                        * Matrix.CreateTranslation(Origin.X, Origin.Y, 0)
                        * Matrix.CreateScale(new Vector3(Scale, Scale, Scale));

            //Origin = CenterScreen / Scale;

            if (Focus == null) return;

            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var x = Position.X + (Focus.Position.X - Position.X) * MoveSpeed * delta;
            var y = Position.Y + (Focus.Position.Y - Position.Y) * MoveSpeed * delta;
            Position = new Vector2(x, y);
        }

        public void FocusOn(IFocusable focus)
        {
            Focus = focus;
        }

        public bool IsInView(Rectangle rect)
        {
            if (rect.Right < (Position.X - Origin.X) || rect.X > (Position.X + Origin.X)) return false;
            if (rect.Bottom < (Position.Y - Origin.Y) || rect.Y > (Position.Y + Origin.Y)) return false;
            return true;
        }
    }
}