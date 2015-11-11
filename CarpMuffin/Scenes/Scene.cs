using System;
using System.Collections.Generic;
using CarpMuffin.Messages;
using CarpMuffin.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CarpMuffin.Scenes
{
    /// <summary>
    /// Base for all scenes
    /// </summary>
    public abstract class Scene
        : IScene
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsVisible { get; set; }
        public ContentManager Content { get; set; }

        public ScreenManager Screens { get; set; }

        public virtual void Update(GameTime gameTime)
        {
            Screens.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            Screens.Draw(gameTime);
        }

        public virtual void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, content.RootDirectory);
            Screens = new ScreenManager(Content);
        }

        public virtual IScene AddScreen(string alias, IScreen screen)
        {
            screen.ParentScene = this;
            Screens.Add(alias, screen);
            return this;
        }

        public virtual IScene AddScreen<T>(string alias)
            where T : IScreen, new()
        {
            var screen = new T();
            return AddScreen(alias, screen);
        }

        public virtual IScene AddScreens(params KeyValuePair<string, IScreen>[] screens)
        {
            foreach (var kvp in screens)
            {
                AddScreen(kvp.Key, kvp.Value);
            }
            return this;
        }

        public virtual IScene PushToScene(string alias)
        {
            Screens.ActivateScreen(alias);
            return this;
        }

        public virtual void PopFromScene()
        {
            Screens.DeactivateScreen();
        }

        public virtual void SendMessage(ScreenMessage message)
        {
            Screens.SendMessage(message);
        }

        public virtual void RegisterMessageListener(string id, Action<ScreenMessage> action)
        {
            Screens.RegisterListener(id, action);
        }

        public virtual void OnSceneChange()
        {

        }
    }
}