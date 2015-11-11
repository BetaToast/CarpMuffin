using CarpMuffin;
using CarpMuffin.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin_Demo_Win.Screens.Splash
{
    public class CarpMuffinSplashScreen
        : Screen
    {
        private Texture2D _logoTexture;

        public override void LoadContent(ContentManager content)
        {
            _logoTexture = Textures.Load("gfx/carpmuffin-logo");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            Engine.Graphics.GraphicsDevice.Clear(Color.Black);
            var viewport = Engine.Graphics.GraphicsDevice.Viewport;
            SpriteBatch.Begin();
                SpriteBatch.Draw(_logoTexture, new Vector2((viewport.Width / 2) - (_logoTexture.Width / 2), (viewport.Height / 2) - (_logoTexture.Height / 2)), Color.White);
            SpriteBatch.End();
        }
    }
}