using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace CarpMuffin.Input
{
    /// <summary>
    /// Determines the state of a gamepads buttons
    /// </summary>
    public class GamePadButtonState
    {
        public ButtonState A { get; private set; }
        public ButtonState B { get; private set; }
        public ButtonState Back { get; private set; }
        public ButtonState BigButton { get; private set; }
        public ButtonState LeftShoulder { get; private set; }
        public ButtonState RightShoulder { get; private set; }
        public ButtonState LeftStick { get; private set; }
        public ButtonState RightStick { get; private set; }
        public ButtonState Start { get; private set; }
        public ButtonState Menu { get; private set; }
        public ButtonState View { get; private set; }
        public ButtonState X { get; private set; }
        public ButtonState Y { get; private set; }
        public ButtonState DPadLeft { get; private set; }
        public ButtonState DPadRight { get; private set; }
        public ButtonState DPadUp { get; private set; }
        public ButtonState DPadDown { get; private set; }
        public ButtonState LeftStickUp { get; private set; }
        public ButtonState LeftStickDown { get; private set; }
        public ButtonState LeftStickLeft { get; private set; }
        public ButtonState LeftStickRight { get; private set; }
        public ButtonState RightStickLeft { get; private set; }
        public ButtonState RightStickRight { get; private set; }
        public ButtonState RightStickUp { get; private set; }
        public ButtonState RightStickDown { get; private set; }
        public ButtonState LeftTrigger { get; private set; }
        public ButtonState RightTrigger { get; private set; }

        public List<Buttons> PressedButtons { get; private set; }

        public GamePadButtonState()
        {
            PressedButtons = new List<Buttons>();
        }

        public void Reset()
        {
            A = ButtonState.Released;
            B = ButtonState.Released;
            Back = ButtonState.Released;
            BigButton = ButtonState.Released;
            LeftShoulder = ButtonState.Released;
            RightShoulder = ButtonState.Released;
            LeftStick = ButtonState.Released;
            RightStick = ButtonState.Released;
            Start = ButtonState.Released;
            Menu = ButtonState.Released;
            View = ButtonState.Released;
            X = ButtonState.Released;
            Y = ButtonState.Released;

            DPadLeft = ButtonState.Released;
            DPadRight = ButtonState.Released;
            DPadUp = ButtonState.Released;
            DPadDown = ButtonState.Released;

            LeftStickLeft = ButtonState.Released;
            LeftStickRight = ButtonState.Released;
            LeftStickUp = ButtonState.Released;
            LeftStickDown = ButtonState.Released;

            RightStickLeft = ButtonState.Released;
            RightStickRight = ButtonState.Released;
            RightStickDown = ButtonState.Released;
            RightStickUp = ButtonState.Released;

            RightTrigger = ButtonState.Released;
            LeftTrigger = ButtonState.Released;
        }

        public void Update(GamePadState state)
        {
            PressedButtons.Clear();

            A = state.Buttons.A;
            B = state.Buttons.B;
            Back = state.Buttons.Back;
            BigButton = state.Buttons.BigButton;
            LeftShoulder = state.Buttons.LeftShoulder;
            RightShoulder = state.Buttons.RightShoulder;
            LeftStick = state.Buttons.LeftStick;
            RightStick = state.Buttons.RightStick;
            Start = state.Buttons.Start;
            Menu = state.Buttons.Start;
            View = state.Buttons.Back;
            X = state.Buttons.X;
            Y = state.Buttons.Y;

            DPadLeft = state.DPad.Left;
            DPadRight = state.DPad.Right;
            DPadUp = state.DPad.Up;
            DPadDown = state.DPad.Down;

            LeftStickLeft = state.ThumbSticks.Left.X < 0f ? ButtonState.Pressed : ButtonState.Released;
            LeftStickRight = state.ThumbSticks.Left.X > 0f ? ButtonState.Pressed : ButtonState.Released;
            LeftStickUp = state.ThumbSticks.Left.Y < 0f ? ButtonState.Pressed : ButtonState.Released;
            LeftStickDown = state.ThumbSticks.Left.Y > 0f ? ButtonState.Pressed : ButtonState.Released;

            RightStickLeft = state.ThumbSticks.Left.X < 0f ? ButtonState.Pressed : ButtonState.Released;
            RightStickRight = state.ThumbSticks.Left.X > 0f ? ButtonState.Pressed : ButtonState.Released;
            RightStickUp = state.ThumbSticks.Left.Y < 0f ? ButtonState.Pressed : ButtonState.Released;
            RightStickDown = state.ThumbSticks.Left.Y > 0f ? ButtonState.Pressed : ButtonState.Released;

            var buttons = Enum.GetValues(typeof(Buttons)).Cast<Buttons>();
            foreach (var button in buttons.Where(IsButtonPressed))
            {
                PressedButtons.Add(button);
            }
        }

        public bool IsButtonPressed(Buttons button)
        {
            switch (button)
            {
                case Buttons.DPadUp: return DPadUp == ButtonState.Pressed;
                case Buttons.DPadDown: return DPadDown == ButtonState.Pressed;
                case Buttons.DPadLeft: return DPadLeft == ButtonState.Pressed;
                case Buttons.DPadRight: return DPadRight == ButtonState.Pressed;
                case Buttons.Start: return Start == ButtonState.Pressed;
                case Buttons.Back: return Back == ButtonState.Pressed;
                case Buttons.LeftStick: return LeftStick == ButtonState.Pressed;
                case Buttons.RightStick: return RightStick == ButtonState.Pressed;
                case Buttons.LeftShoulder: return LeftShoulder == ButtonState.Pressed;
                case Buttons.RightShoulder: return RightShoulder == ButtonState.Pressed;
                case Buttons.BigButton: return BigButton == ButtonState.Pressed;
                case Buttons.A: return A == ButtonState.Pressed;
                case Buttons.B: return B == ButtonState.Pressed;
                case Buttons.X: return X == ButtonState.Pressed;
                case Buttons.Y: return Y == ButtonState.Pressed;
                case Buttons.LeftThumbstickLeft: return LeftStickLeft == ButtonState.Pressed;
                case Buttons.RightTrigger: return RightTrigger == ButtonState.Pressed;
                case Buttons.LeftTrigger: return LeftTrigger == ButtonState.Pressed;
                case Buttons.RightThumbstickUp: return RightStickUp == ButtonState.Pressed;
                case Buttons.RightThumbstickDown: return RightStickDown == ButtonState.Pressed;
                case Buttons.RightThumbstickRight: return RightStickRight == ButtonState.Pressed;
                case Buttons.RightThumbstickLeft: return RightStickLeft == ButtonState.Pressed;
                case Buttons.LeftThumbstickUp: return LeftStickUp == ButtonState.Pressed;
                case Buttons.LeftThumbstickDown: return LeftStickDown == ButtonState.Pressed;
                case Buttons.LeftThumbstickRight: return LeftStickRight == ButtonState.Pressed;
                case Buttons.View: return View == ButtonState.Pressed;
                case Buttons.Menu: return Menu == ButtonState.Pressed;
            }
            return false;
        }

        public bool IsButtonReleased(Buttons button)
        {
            switch (button)
            {
                case Buttons.DPadUp: return DPadUp == ButtonState.Released;
                case Buttons.DPadDown: return DPadDown == ButtonState.Released;
                case Buttons.DPadLeft: return DPadLeft == ButtonState.Released;
                case Buttons.DPadRight: return DPadRight == ButtonState.Released;
                case Buttons.Start: return Start == ButtonState.Released;
                case Buttons.Back: return Back == ButtonState.Released;
                case Buttons.LeftStick: return LeftStick == ButtonState.Released;
                case Buttons.RightStick: return RightStick == ButtonState.Released;
                case Buttons.LeftShoulder: return LeftShoulder == ButtonState.Released;
                case Buttons.RightShoulder: return RightShoulder == ButtonState.Released;
                case Buttons.BigButton: return BigButton == ButtonState.Released;
                case Buttons.A: return A == ButtonState.Released;
                case Buttons.B: return B == ButtonState.Released;
                case Buttons.X: return X == ButtonState.Released;
                case Buttons.Y: return Y == ButtonState.Released;
                case Buttons.LeftThumbstickLeft: return LeftStickLeft == ButtonState.Released;
                case Buttons.RightTrigger: return RightTrigger == ButtonState.Released;
                case Buttons.LeftTrigger: return LeftTrigger == ButtonState.Released;
                case Buttons.RightThumbstickUp: return RightStickUp == ButtonState.Released;
                case Buttons.RightThumbstickDown: return RightStickDown == ButtonState.Released;
                case Buttons.RightThumbstickRight: return RightStickRight == ButtonState.Released;
                case Buttons.RightThumbstickLeft: return RightStickLeft == ButtonState.Released;
                case Buttons.LeftThumbstickUp: return LeftStickUp == ButtonState.Released;
                case Buttons.LeftThumbstickDown: return LeftStickDown == ButtonState.Released;
                case Buttons.LeftThumbstickRight: return LeftStickRight == ButtonState.Released;
                case Buttons.View: return View == ButtonState.Released;
                case Buttons.Menu: return Menu == ButtonState.Released;
            }
            return false;
        }

        public bool IsAnyButtonPressed()
        {
            return PressedButtons.Count > 0;
        }
    }
}