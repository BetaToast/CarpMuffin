using System;
using CarpMuffin.Audio;
using CarpMuffin.Graphics;
using CarpMuffin.Input;
using CarpMuffin.Messages;
using CarpMuffin.Scenes;
using Microsoft.Xna.Framework.Graphics;
using TextureCollection = CarpMuffin.Collections.TextureCollection;

namespace CarpMuffin.Screens
{
    /// <summary>
    /// Defines a screen
    /// </summary>
    public interface IScreen
        : IEntity
    {
        SpriteBatch SpriteBatch { get; set; }
        TextureCollection Textures { get; set; }
        InputManager Input { get; set; }
        Camera2D Camera { get; set; }
        IScene ParentScene { get; set; }
        Engine Engine { get; }
        SongCollection Songs { get; set; }
        SoundEffectCollection SoundEffects { get; set; }

        void SendMessage(string to, object what);
        void ListenForMessage(string id, Action<ScreenMessage> action);
    }
}