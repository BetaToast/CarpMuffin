using System;
using System.Collections.Generic;
using System.Linq;
using CarpMuffin.Input;
using CarpMuffin.Managers;
using CarpMuffin.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Screens
{
    /// <summary>
    /// Manages all screens
    /// </summary>
    public class ScreenManager
        : EntityManager<IScreen>
    {
        private readonly ContentManager _content;
        private readonly Queue<IScreen> _screens = new Queue<IScreen>();

        private readonly Dictionary<string, Action<ScreenMessage>> _registeredMessageListeners = new Dictionary<string, Action<ScreenMessage>>();

        /// <summary>
        /// Only updates top most screen
        /// </summary>
        public bool TopMostMode { get; set; }

        public ScreenManager(ContentManager content)
        {
            _content = content;
            TopMostMode = false;
        }

        public override IScreen Add(string alias, IScreen item)
        {
            item.SpriteBatch = new SpriteBatch(Engine.Instance.GraphicsDevice);
            item.SpriteBatch.Name = $"SpriteBatch_{item.GetType().FullName}";
            item.Textures = new Collections.TextureCollection(_content);
            item.Input = new InputManager();
            item.Content = _content;
            item.LoadContent(_content);
            return base.Add(alias, item);
        }

        public override IScreen Add(IScreen item)
        {
            item.LoadContent(_content);
            return base.Add(item);
        }

        public void Update(GameTime gameTime)
        {
            if (TopMostMode)
            {
                var topMostScreen = _screens.Peek();
                if (topMostScreen.IsEnabled)
                {
                    topMostScreen.Input.Update(gameTime);
                    topMostScreen.Update(gameTime);
                }
            }
            else
            {
                foreach (var screen in _screens.Where(screen => screen.IsEnabled))
                {
                    screen.Input.Update(gameTime);
                    screen.Update(gameTime);
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var screen in _screens.Where(screen => screen.IsVisible))
            {
                screen.Draw(gameTime);
            }
        }

        /// <summary>
        /// Adds the specified screen to the display stack
        /// </summary>
        public void ActivateScreen(string alias)
        {
            _screens.Enqueue(Get(alias));
        }

        /// <summary>
        /// Removes specified amount of screens from the display stack
        /// </summary>
        public void DeactivateScreen(int count = 1)
        {
            if (count > _screens.Count) count = _screens.Count;
            for (var i = 0; i < count; i++)
            {
                _screens.Dequeue();
            }
        }

        public void SendMessage(ScreenMessage message)
        {
            if (_registeredMessageListeners.ContainsKey(message.Id))
            {
                var action = _registeredMessageListeners[message.Id];
                action.Invoke(message);
            }
        }

        public void RegisterListener(string id, Action<ScreenMessage> action)
        {
            _registeredMessageListeners.Add(id, action);
        }
    }
}