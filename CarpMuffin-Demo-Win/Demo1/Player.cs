using System;
using CarpMuffin;
using CarpMuffin.Input;
using CarpMuffin.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Buttons = CarpMuffin.Input.Buttons;

namespace CarpMuffin_Demo_Win.Demo1
{
    public class Player
        : Sprite
    {
        #region Fields

        private double _bulletShotElapsed;

        #endregion

        #region Properties

        public float Speed { get; set; }
        public double FiringRate { get; set; }

        public Action<Player> FireWeapons { get; set; }

        #endregion

        #region Initialization

        public Player() { }

        public Player(Texture2D texture)
            : base(texture)
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();

            Speed = 4f;
            FiringRate = 250;
        }

        #endregion

        public void UpdateInput(InputManager input)
        {
            if (input.Keyboard.IsKeyHeld(Keys.A) 
                || input.Keyboard.IsKeyHeld(Keys.Left)
                || input.GamePad.IsButtonHeld(PlayerIndex.One, Buttons.LeftThumbstickLeft)
                || input.GamePad.IsButtonHeld(PlayerIndex.One, Buttons.DPadLeft))
            {
                Position += new Vector2(-Speed, 0f);
            }

            if (input.Keyboard.IsKeyHeld(Keys.D)
                || input.Keyboard.IsKeyHeld(Keys.Right)
                || input.GamePad.IsButtonHeld(PlayerIndex.One, Buttons.LeftThumbstickRight)
                || input.GamePad.IsButtonHeld(PlayerIndex.One, Buttons.DPadRight))
            {
                Position += new Vector2(Speed, 0f);
            }

            if (input.Keyboard.IsKeyHeld(Keys.Space)
                || input.GamePad.IsButtonHeld(PlayerIndex.One, Buttons.A)
                || input.GamePad.IsButtonHeld(PlayerIndex.One, Buttons.RightTrigger))
            {
                if (_bulletShotElapsed >= FiringRate)
                {
                    FireWeapons?.Invoke(this);
                    _bulletShotElapsed = 0;
                }
            }

            var viewport = Engine.Instance.Graphics.GraphicsDevice.Viewport;
            if (Position.X < 0)
            {
                Position = new Vector2(0f, Position.Y);
            }
            if (Position.X + Size.X > viewport.Width)
            {
                Position = new Vector2(viewport.Width - Size.X, Position.Y);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _bulletShotElapsed += gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
}