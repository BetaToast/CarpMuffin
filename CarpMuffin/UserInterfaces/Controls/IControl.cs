using CarpMuffin.Graphics;
using CarpMuffin.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.UserInterfaces.Controls
{
    /// <summary>
    /// Base interface for controls
    /// </summary>
    public interface IControl
        : IRenderable
    {
        SpriteBatch SpriteBatch { get; set; }
        Texture2D Texture { get; set; }
        UserInterfaceSkin Skin { get; set; }
        Vector2 Position { get; set; }
        Vector2 Size { get; set; }
        Color Tint { get; set; }
        SpriteFont Font { get; set; }
        Rectangle Bounds { get; }

        void LoadParts();
        void UpdateInput(InputManager input);
    }
}