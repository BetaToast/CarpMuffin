using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.UserInterfaces.Controls
{
    public class ScreenOverlay
        : Control
    {
        private Texture2D _whitePixel;

        public override void LoadParts()
        {
            _whitePixel = Engine.Instance.CreateWhitePixel();
        }

        public override void Draw(GameTime gameTime)
        {
            var viewport = Engine.Instance.Graphics.GraphicsDevice.Viewport;

            SpriteBatch.Draw(_whitePixel, new Rectangle(0, 0, viewport.Width, viewport.Height), Tint);
        }
    }
}