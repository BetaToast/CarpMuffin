using System;
using System.Collections.Generic;
using System.Linq;
using CarpMuffin.Input;
using Microsoft.Xna.Framework;

namespace CarpMuffin.UserInterfaces.Controls
{
    public class Container
        : Control
    {
        public List<IControl> Children { get; set; }

        public Container()
        {
            Size = new Vector2(100f, 100f);
            Children = new List<IControl>();
        }

        public override void LoadParts()
        {

        }

        public override void Update(GameTime gameTime)
        {
            UpdateChildren(gameTime);
        }

        public virtual void UpdateChildren(GameTime gameTime)
        {
            foreach (var child in Children.Where(child => child.IsEnabled))
            {
                var originalPosition = child.Position;
                child.Position = Position + originalPosition;
                child.Update(gameTime);
                child.Position = originalPosition;
            }
        }

        public override void UpdateInput(InputManager input)
        {
            UpdateChildInput(input);
        }

        public virtual void UpdateChildInput(InputManager input)
        {
            foreach (var child in Children.Where(child => child.IsEnabled))
            {
                var originalPosition = child.Position;
                child.Position = Position + originalPosition;
                child.UpdateInput(input);
                child.Position = originalPosition;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            DrawChildren(gameTime);
        }

        public void DrawChildren(GameTime gameTime)
        {
            // Children
            foreach (var child in Children.Where(child => child.IsVisible))
            {
                var originalPosition = child.Position;
                child.Position = Position + originalPosition;
                child.Draw(gameTime);
                child.Position = originalPosition;
            }
        }

        public T AddChild<T>()
            where T : IControl, new()
        {
            if (Skin == null) throw new Exception("No Skin Loaded.");
            if (Font == null) throw new Exception("No Font Loaded.");
            var control = new T
            {
                Skin = Skin,
                SpriteBatch = SpriteBatch,
                Texture = Skin.Texture,
                Font = Font
            };
            control.LoadParts();
            Children.Add(control);
            return control;
        }
    }
}