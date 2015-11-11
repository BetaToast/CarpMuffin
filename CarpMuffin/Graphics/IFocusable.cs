using Microsoft.Xna.Framework;

namespace CarpMuffin.Graphics
{
    /// <summary>
    /// Base interface for things that can be focused on with the Camera
    /// </summary>
    public interface IFocusable
    {
        Vector2 Position { get; set; }
        Vector2 Size { get; set; }
    }
}