using CarpMuffin.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CarpMuffin.Input
{
    /// <summary>
    /// Manages Gamepads
    /// </summary>
    public class GamePadManager
        : IUpdatable
    {
        public bool IsEnabled { get; set; }
        public GamePadState[] CurrentState { get; set; }
        public GamePadState[] PreviousState { get; set; }

        public GamePadButtonState[] CurrentButtonState { get; set; }
        public GamePadButtonState[] PreviousButtonState { get; set; }

        public GamePadManager()
        {
            IsEnabled = true;
            CurrentState = new GamePadState[4];
            PreviousState = new GamePadState[4];
            CurrentButtonState = new GamePadButtonState[4];
            PreviousButtonState = new GamePadButtonState[4];
        }

        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < 4; i++)
            {
                PreviousState[i] = CurrentState[i];
                CurrentState[i] = GamePad.GetState((PlayerIndex)i);
                PreviousButtonState[i] = CurrentButtonState[i];
                if (CurrentButtonState[i] == null) CurrentButtonState[i] = new GamePadButtonState();
                CurrentButtonState[i].Update(CurrentState[i]);
            }
        }

        #region Methods

        public bool IsConnected(PlayerIndex playerIndex)
        {
            return GetCurrentState(playerIndex).IsConnected;
        }

        public GamePadState GetCurrentState(PlayerIndex playerIndex)
        {
            return CurrentState[(int)playerIndex];
        }

        public GamePadState GetPreviousState(PlayerIndex playerIndex)
        {
            return PreviousState[(int)playerIndex];
        }

        public GamePadButtonState GetCurrentButtonState(PlayerIndex playerIndex)
        {
            return CurrentButtonState[(int)playerIndex];
        }

        public GamePadButtonState GetPreviousButtonState(PlayerIndex playerIndex)
        {
            return PreviousButtonState[(int)playerIndex];
        }

        public bool IsButtonPressed(PlayerIndex playerIndex, Buttons button)
        {
            return GetCurrentButtonState(playerIndex).IsButtonPressed(button) && GetPreviousButtonState(playerIndex).IsButtonReleased(button);
        }

        public bool IsButtonHeld(PlayerIndex playerIndex, Buttons button)
        {
            return GetCurrentButtonState(playerIndex).IsButtonPressed(button) && GetPreviousButtonState(playerIndex).IsButtonPressed(button);
        }

        public bool IsButtonReleased(PlayerIndex playerIndex, Buttons button)
        {
            return GetCurrentButtonState(playerIndex).IsButtonReleased(button) && GetPreviousButtonState(playerIndex).IsButtonPressed(button);
        }

        public bool IsAnyButtonPressed(PlayerIndex playerIndex)
        {
            return GetCurrentButtonState(playerIndex).IsAnyButtonPressed();
        }

        #endregion
    }
}