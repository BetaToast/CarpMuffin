using System.Linq;
using CarpMuffin.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CarpMuffin.Input
{
    /// <summary>
    /// Manages Keyboard Input
    /// </summary>
    public class KeyboardManager
        : IUpdatable
    {
        public KeyboardState CurrentState { get; set; }
        public KeyboardState PreviousState { get; set; }
        public bool IsEnabled { get; set; }

        public KeyboardManager()
        {
            IsEnabled = true;
        }

        public void Update(GameTime gameTime)
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
        }

        #region Methods

        public bool IsKeyPressed(Keys key)
        {
            return CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);
        }

        public bool IsKeyHeld(Keys key)
        {
            return CurrentState.IsKeyDown(key) && PreviousState.IsKeyDown(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            return CurrentState.IsKeyUp(key) && PreviousState.IsKeyDown(key);
        }

        public bool IsAnyKeyPressed()
        {
            return CurrentState.GetPressedKeys().Count() > 0;
        }

        public bool AllKeysReleased()
        {
            return CurrentState.GetPressedKeys().Count() == 0;
        }

        #endregion
    }
}