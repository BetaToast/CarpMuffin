using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CarpMuffin.Graphics
{
    /// <summary>
    /// Specifies that an object is updatable and drawable to the screen
    /// </summary>
    public interface IEntity
        : IUpdatable, IDrawable
    {
        string Name { get; set; }
        Vector2 Position { get; set; }
        ContentManager Content { get; set; }

        void LoadContent(ContentManager content);
    }
}