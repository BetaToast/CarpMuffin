using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Layers
{
    /// <summary>
    /// Base interface for all layers
    /// </summary>
    public interface ILayer
    {
        bool IsEnabled { get; set; }
        bool IsVisible { get; set; }

        SpriteBatch SpriteBatch { get; set; }

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}