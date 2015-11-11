using Microsoft.Xna.Framework;

namespace CarpMuffin.Graphics
{
    /// <summary>
    /// Specifies whether an object can be updated
    /// </summary>
    public interface IUpdatable
    {
        bool IsEnabled { get; set; }

        void Update(GameTime gameTime);
    }
}