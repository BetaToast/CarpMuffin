using Microsoft.Xna.Framework;

namespace CarpMuffin.Graphics
{
    /// <summary>
    /// Interface to specify something as drawable to the screen
    /// </summary>
    public interface IDrawable
    {
        bool IsVisible { get; set; }

        void Draw(GameTime gameTime);
    }
}