using CarpMuffin.Collections;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace CarpMuffin.Audio
{
    /// <summary>
    /// Handles a collection of audio resources (songs)
    /// </summary>
    public class SongCollection
        : ResourceCollection<Song>
    {
        public SongCollection()
        {

        }

        public SongCollection(ContentManager content)
            : base(content)
        {

        }
    }
}