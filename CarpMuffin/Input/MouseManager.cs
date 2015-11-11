using System;
using System.Linq;
using CarpMuffin.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CarpMuffin.Input
{
    /// <summary>
    /// Manages Mouse Input
    /// </summary>
    public class MouseManager
        : IUpdatable
    {
        public bool IsEnabled { get; set; }
        public MouseState CurrentState { get; set; }
        public MouseState PreviousState { get; set; }

        public Vector2 CurrentPosition => new Vector2(CurrentState.X, CurrentState.Y);
        public Vector2 PreviousPosition => new Vector2(PreviousState.X, PreviousState.Y);

        public Rectangle Bounds => new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, 1, 1);
        public Rectangle PreviousBounds => new Rectangle((int)PreviousPosition.X, (int)PreviousPosition.Y, 1, 1);


        public MouseManager()
        {
            IsEnabled = true;
        }

        public void Update(GameTime gameTime)
        {
            PreviousState = CurrentState;
            CurrentState = Mouse.GetState();
        }

        private bool IsButton(MouseButtons button, ButtonState currentState, ButtonState previousState)
        {
            switch (button)
            {
                case MouseButtons.Left: return CurrentState.LeftButton == currentState && PreviousState.LeftButton == previousState;
                case MouseButtons.Middle: return CurrentState.MiddleButton == currentState && PreviousState.MiddleButton == previousState;
                case MouseButtons.Right: return CurrentState.RightButton == currentState && PreviousState.RightButton == previousState;
                case MouseButtons.XButton1: return CurrentState.XButton1 == currentState && PreviousState.XButton1 == previousState;
                case MouseButtons.XButton2: return CurrentState.XButton2 == currentState && PreviousState.XButton2 == previousState;
            }
            return false;
        }

        public bool IsButtonPressed(MouseButtons button)
        {
            var currentState = ButtonState.Pressed;
            var previousState = ButtonState.Released;
            return IsButton(button, currentState, previousState);
        }

        public bool IsButtonReleased(MouseButtons button)
        {
            var currentState = ButtonState.Released;
            var previousState = ButtonState.Pressed;
            return IsButton(button, currentState, previousState);
        }

        public bool IsButtonHeld(MouseButtons button)
        {
            var currentState = ButtonState.Pressed;
            var previousState = ButtonState.Pressed;
            return IsButton(button, currentState, previousState);
        }

        public bool IsAnyButtonPressed()
        {
            var currentState = ButtonState.Pressed;
            var buttons = Enum.GetValues(typeof(MouseButtons)).Cast<MouseButtons>();
            foreach (var button in buttons)
            {
                switch (button)
                {
                    case MouseButtons.Left:
                        if (CurrentState.LeftButton == currentState) return true;
                        break;
                    case MouseButtons.Middle:
                        if (CurrentState.MiddleButton == currentState) return true;
                        break;
                    case MouseButtons.Right:
                        if (CurrentState.RightButton == currentState) return true;
                        break;
                    case MouseButtons.XButton1:
                        if (CurrentState.XButton1 == currentState) return true;
                        break;
                    case MouseButtons.XButton2:
                        if (CurrentState.XButton2 == currentState) return true;
                        break;
                }
            }
            return false;
        }
    }
}