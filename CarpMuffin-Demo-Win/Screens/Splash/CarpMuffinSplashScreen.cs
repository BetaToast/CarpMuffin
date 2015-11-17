using CarpMuffin.Animations;
using CarpMuffin.Extensions;
using CarpMuffin.Screens;
using CarpMuffin_Demo_Win.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin_Demo_Win.Screens.Splash
{
    public class CarpMuffinSplashScreen
        : Screen
    {
        private Texture2D _logoTexture;
        private Texture2D _whitePixel;
        private TransitionState _state;
        private double _transitionValue;
        private Tween _transitionTween;
        
        public override void LoadContent(ContentManager content)
        {
            Engine.ClearColor = Color.Black;
            _state = TransitionState.Beginning;
            
            _transitionTween = new Tween(0, 100, 2000)
            {
                OnComplete = t =>
                {
                    if (_state == TransitionState.Beginning)
                    {
                        _state = TransitionState.Running;
                    }
                    else if (_state == TransitionState.Running)
                    {
                        _state = TransitionState.End;
                    }
                    else if (_state == TransitionState.End)
                    {
                        _state = TransitionState.Stopped;
                    }

                    if (_state != TransitionState.Stopped)
                    {
                        t.Reset();
                        t.Start();
                    }
                    else
                    {
                        Engine.Scenes.ChangeScene(SceneNames.Title);
                    }
                }
            };

            _logoTexture = Textures.Load(TextureNames.CarpMuffinLogo);
            _whitePixel = Engine.CreateWhitePixel();
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.Keyboard.IsAnyKeyPressed() 
                || Input.GamePad.IsAnyButtonPressed(PlayerIndex.One) 
                || Input.Mouse.IsAnyButtonPressed())
            {
                Engine.Scenes.ChangeScene(SceneNames.Title);
            }

            _transitionTween.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            var viewport = Engine.Graphics.GraphicsDevice.Viewport;

            SpriteBatch.Begin();
                SpriteBatch.Draw(_logoTexture, new Vector2((viewport.Width / 2) - (_logoTexture.Width / 2), (viewport.Height / 2) - (_logoTexture.Height / 2)), Color.White);

                if (_state == TransitionState.Beginning)
                {
                    _transitionValue = _transitionTween.Maximum - _transitionTween.Value;
                }
                else if (_state == TransitionState.Running)
                {
                    _transitionValue = 0;
                }
                else if (_state == TransitionState.End)
                {
                    _transitionValue = _transitionTween.Value;
                }

                var color = Color.Black.WithOpacity((float)(_transitionValue / 100));
                SpriteBatch.Draw(_whitePixel, new Rectangle(0, 0, viewport.Width, viewport.Height), color);

            SpriteBatch.End();
        }
    }
}