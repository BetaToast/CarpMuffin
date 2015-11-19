using System;
using CarpMuffin.Audio;
using CarpMuffin.Graphics;
using CarpMuffin.Input;
using CarpMuffin.Messages;
using CarpMuffin.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TextureCollection = CarpMuffin.Collections.TextureCollection;

namespace CarpMuffin.Screens
{
    /// <summary>
    /// Base for all screens
    /// </summary>
    public abstract class Screen
        : IScreen
    {
        public bool IsEnabled { get; set; }
        public bool IsVisible { get; set; }
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public ContentManager Content { get; set; }
        public TextureCollection Textures { get; set; }
        public InputManager Input { get; set; }
        public Camera2D Camera { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public IScene ParentScene { get; set; }
        public SongCollection Songs { get; set; }
        public SoundEffectCollection SoundEffects { get; set; }

        public Engine Engine => Engine.Instance;

        protected Screen()
        {
            IsEnabled = true;
            IsVisible = true;
            Textures = new TextureCollection();
            Camera = new Camera2D();
            Songs = new SongCollection();
            SoundEffects = new SoundEffectCollection();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        public abstract void LoadContent(ContentManager content);

        public virtual void SendMessage(string to, object what)
        {
            var message = new ScreenMessage
            {
                Id = to,
                Payload = what,
                Sender = this
            };
            ParentScene.SendMessage(message);
        }

        public virtual void ListenForMessage(string id, Action<ScreenMessage> action)
        {
            ParentScene.RegisterMessageListener(id, action);
        }
    }
}