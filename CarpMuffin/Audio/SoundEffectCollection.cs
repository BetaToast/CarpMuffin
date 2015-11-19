using CarpMuffin.Collections;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace CarpMuffin.Audio
{
    /// <summary>
    /// Handles a collection of audio resources (sound effects)
    /// </summary>
    public class SoundEffectCollection
        : ResourceCollection<SoundEffect>
    {
        public SoundEffectCollection()
        {

        }

        public SoundEffectCollection(ContentManager content)
            : base(content)
        {

        }
    }
}