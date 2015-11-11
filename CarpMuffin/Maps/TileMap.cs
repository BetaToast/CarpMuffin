using System.Collections.Generic;
using CarpMuffin.Layers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Maps
{
    /// <summary>
    /// Displays a map of tiles on the screen
    /// </summary>
    public class TileMap
    {
        public SpriteBatch SpriteBatch { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsVisible { get; set; }
        public List<ILayer> Layers { get; }
        public List<string> LayerNames { get; }

        public ILayer this[int index] => Layers[index];
        public ILayer this[string alias] => Layers[LayerNames.IndexOf(alias)];


        public TileMap(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
            IsEnabled = true;
            IsVisible = true;
            Layers = new List<ILayer>();
            LayerNames = new List<string>();
        }

        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                layer.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            for (var i = 0; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                layer.Draw(gameTime);
            }
        }

        public void AddLayer(string alias, ILayer layer)
        {
            layer.SpriteBatch = SpriteBatch;

            if (LayerNames.Contains(alias))
            {
                var index = LayerNames.IndexOf(alias);
                Layers[index] = layer;
            }
            else
            {
                LayerNames.Add(alias);
                Layers.Add(layer);
            }
        }

        public T CreateLayer<T>(string alias)
            where T : ILayer, new()
        {
            var layer = new T();
            AddLayer(alias, layer);
            return layer;
        }

        public void RemoveLayer(string alias)
        {
            var index = LayerNames.IndexOf(alias);
            Layers.RemoveAt(index);
            LayerNames.Remove(alias);
        }
    }
}