using System.Collections.Generic;
using CarpMuffin.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Layers
{
    /// <summary>
    /// Base for all layers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Layer<T>
        : ILayer
        where T : IRenderable
    {
        public List<T> Items { get; private set; }
        public bool IsEnabled { get; set; }
        public bool IsVisible { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        protected Layer()
        {
            IsEnabled = true;
            IsVisible = true;
            Items = new List<T>();
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!IsEnabled) return;

            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                item.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (!IsVisible) return;

            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                item.Draw(gameTime);
            }
        }

        public virtual void Add(T item)
        {
            Items.Add(item);
        }
    }
}