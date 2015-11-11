using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Collections
{
    /// <summary>
    /// Handles a collection of 2D Texture resources
    /// </summary>
    public class TextureCollection
        : ResourceCollection<Texture2D>
    {
        public TextureCollection()
        {

        }

        public TextureCollection(ContentManager content)
            : base(content)
        {

        }
    }
}