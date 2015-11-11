using CarpMuffin.Graphics;
using Microsoft.Xna.Framework;

namespace CarpMuffin.Input
{
    /// <summary>
    /// Manages all input types
    /// Keyboard, Mouse and Gamepad (No Touch)
    /// </summary>
    public class InputManager
        : IUpdatable
    {
        public KeyboardManager Keyboard { get; set; }
        public GamePadManager GamePad { get; set; }
        public MouseManager Mouse { get; set; }

        public bool IsEnabled { get; set; }

        public InputManager(bool hasKeyboard = true, bool hasGamePad = true, bool hasMouse = true)
        {
            IsEnabled = true;
            Keyboard = new KeyboardManager();
            GamePad = new GamePadManager();
            Mouse = new MouseManager();

            if (!hasKeyboard) Keyboard.IsEnabled = false;
            if (!hasGamePad) GamePad.IsEnabled = false;
            if (!hasMouse) Mouse.IsEnabled = false;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.IsEnabled) Keyboard.Update(gameTime);
            if (GamePad.IsEnabled) GamePad.Update(gameTime);
            if (Mouse.IsEnabled) Mouse.Update(gameTime);
        }
    }
}