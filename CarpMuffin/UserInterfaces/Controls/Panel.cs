using System;
using System.Collections.Generic;
using System.Linq;
using CarpMuffin.Extensions;
using CarpMuffin.Input;
using Microsoft.Xna.Framework;

namespace CarpMuffin.UserInterfaces.Controls
{
    /// <summary>
    ///  Displays a Panel
    /// </summary>
    public class Panel
        : Control
    {
        private string _basePartName = UserInterfacePartNames.GlassPanel;
        private float _partWidth;
        private float _partHeight;
        private bool _isDragging;
        private Vector2 _dragOffset;

        private Rectangle _partBase;
        private Rectangle _partTopLeft;
        private Rectangle _partTopCenter;
        private Rectangle _partTopRight;
        private Rectangle _partCenterLeft;
        private Rectangle _partCenter;
        private Rectangle _partCenterRight;
        private Rectangle _partBottomLeft;
        private Rectangle _partBottomCenter;
        private Rectangle _partBottomRight;

        public List<IControl> Children { get; set; }
        public bool IsDraggable { get; set; }
        public int DragRegionHeight { get; set; }

        public Panel()
        {
            Size = new Vector2(100f, 100f);
            Children = new List<IControl>();
            IsDraggable = true;
            DragRegionHeight = 8;
            _isDragging = false;
        }

        public override void LoadParts()
        {
            _partBase = Skin[_basePartName];

            _partWidth = _partBase.Width / 3f;
            _partHeight = _partBase.Height / 3f;

            var x = _partBase.X;
            var y = _partBase.Y;

            DragRegionHeight = (int)(_partHeight / 2);

            _partTopLeft = new Rectangle(x, y, (int)_partWidth, (int)_partHeight);
            _partTopCenter = new Rectangle((int)(x + _partWidth), y, (int)_partWidth, (int)_partHeight);
            _partTopRight = new Rectangle((int)(x + (_partWidth * 2)), y, (int)_partWidth, (int)_partHeight);
            _partCenterLeft = new Rectangle(x, (int)(y + _partHeight), (int)_partWidth, (int)_partHeight);
            _partCenter = new Rectangle((int)(x + _partWidth), (int)(y + _partHeight), (int)_partWidth, (int)_partHeight);
            _partCenterRight = new Rectangle((int)(x + (_partWidth * 2)), (int)(y + _partHeight), (int)_partWidth, (int)_partHeight);
            _partBottomLeft = new Rectangle(x, (int)(y + (_partHeight * 2)), (int)_partWidth, (int)_partHeight);
            _partBottomCenter = new Rectangle((int)(x + _partWidth), (int)(y + (_partHeight * 2)), (int)_partWidth, (int)_partHeight);
            _partBottomRight = new Rectangle((int)(x + (_partWidth * 2)), (int)(y + (_partHeight * 2)), (int)_partWidth, (int)_partHeight);
        }

        public void ReloadPartsWith(string userInterfacePartName, bool loadParts = true)
        {
            _basePartName = userInterfacePartName;
            if (loadParts) LoadParts();
        }

        public override void Update(GameTime gameTime)
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
            if (IsDraggable)
            {
                var dragRegion = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, DragRegionHeight);
                if (input.Mouse.Bounds.Intersects(dragRegion) && input.Mouse.IsButtonHeld(MouseButtons.Left) && !_isDragging)
                {
                    _isDragging = true;
                    _dragOffset = input.Mouse.CurrentPosition - Position;
                }
                if (_isDragging && input.Mouse.IsButtonReleased(MouseButtons.Left))
                {
                    _isDragging = false;
                }
                if (_isDragging)
                {
                    Position = input.Mouse.CurrentPosition - _dragOffset;
                }
            }

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
            var x = (int)Position.X;
            var y = (int)Position.Y;
            var w = (int)Size.X;
            var h = (int)Size.Y;
            var pw = (int)_partWidth;
            var ph = (int)_partHeight;
            var iw = (w - pw - pw);
            var ih = (h - ph - ph);

            // Shadow
            var destRect = new Rectangle(x + 4, y + 4, (int)Size.X, (int)Size.Y);
            if (IsDraggable) SpriteBatch.Draw(Texture, null, destRect, _partBase, Vector2.Zero, 0f, Vector2.One, Color.Black.WithOpacity(0.5f));

            // Top Left
            destRect = new Rectangle(x, y, pw, ph);
            SpriteBatch.Draw(Texture, null, destRect, _partTopLeft, Vector2.One, 0f, Vector2.One, Tint);

            // Top Center
            for (var xo = 0; xo < iw; xo++)
            {
                var srcRect = new Rectangle(_partTopCenter.X, _partTopCenter.Y, 1, _partTopCenter.Height);
                destRect = new Rectangle(x + pw + xo, y, 1, ph);
                SpriteBatch.Draw(Texture, null, destRect, srcRect, Vector2.One, 0f, Vector2.One, Tint);
            }

            // Top Right
            destRect = new Rectangle(x + (w - pw), y, pw, ph);
            SpriteBatch.Draw(Texture, null, destRect, _partTopRight, Vector2.One, 0f, Vector2.One, Tint);


            // Center Left
            for (var yo = 0; yo < ih; yo++)
            {
                var srcRect = new Rectangle(_partCenterLeft.X, _partCenterLeft.Y, _partCenterLeft.Width, 1);
                destRect = new Rectangle(x, y + ph + yo, pw, 1);
                SpriteBatch.Draw(Texture, null, destRect, srcRect, Vector2.One, 0f, Vector2.One, Tint);
            }

            // Center
            for (var yo = 0; yo < ih - ph; yo += ph)
            {
                for (var xo = 0; xo < iw; xo++)
                {
                    var srcRect = new Rectangle(_partCenter.X, _partCenter.Y, 1, _partCenter.Height);
                    destRect = new Rectangle(x + pw + xo, y + ph + yo, 1, ph);
                    SpriteBatch.Draw(Texture, null, destRect, srcRect, Vector2.One, 0f, Vector2.One, Tint);
                }
            }

            // Remainder height
            var dh = ih / (float)ph;
            var inth = ih / ph;
            var remh = dh - inth;
            var remph = (int)Math.Round(ph * remh, 0);
            for (var yo = ih - remph; yo < ih; yo++)
            {
                for (var xo = 0; xo < iw; xo++)
                {
                    var srcRect = new Rectangle(_partCenter.X, _partCenter.Y, 1, 1);
                    destRect = new Rectangle(x + pw + xo, y + ph + yo, 1, 1);
                    SpriteBatch.Draw(Texture, null, destRect, srcRect, Vector2.One, 0f, Vector2.One, Tint);
                }
            }

            // Center Right
            for (var yo = 0; yo < ih; yo++)
            {
                var srcRect = new Rectangle(_partCenterRight.X, _partCenterRight.Y, _partCenterRight.Width, 1);
                destRect = new Rectangle(x + (w - pw), y + ph + yo, pw, 1);
                SpriteBatch.Draw(Texture, null, destRect, srcRect, Vector2.One, 0f, Vector2.One, Tint);
            }


            // Bottom Left
            destRect = new Rectangle(x, y + (h - ph), pw, ph);
            SpriteBatch.Draw(Texture, null, destRect, _partBottomLeft, Vector2.One, 0f, Vector2.One, Tint);

            // Bottom Center
            for (var xo = 0; xo < iw; xo++)
            {
                var srcRect = new Rectangle(_partBottomCenter.X, _partBottomCenter.Y, 1, _partBottomCenter.Height);
                destRect = new Rectangle(x + pw + xo, y + (h - ph), 1, ph);
                SpriteBatch.Draw(Texture, null, destRect, srcRect, Vector2.One, 0f, Vector2.One, Tint);
            }

            // Bottom Right
            destRect = new Rectangle(x + (w - pw), y + (h - ph), pw, ph);
            SpriteBatch.Draw(Texture, null, destRect, _partBottomRight, Vector2.One, 0f, Vector2.One, Tint);

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