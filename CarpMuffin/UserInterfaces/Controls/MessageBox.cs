using System;
using CarpMuffin.Extensions;
using CarpMuffin.Input;
using Microsoft.Xna.Framework;

namespace CarpMuffin.UserInterfaces.Controls
{
    /// <summary>
    /// Displays a Message Box
    /// </summary>
    public class MessageBox
        : Control
    {
        private Rectangle _partPanel;
        private GlassButton _acceptButton;
        private GlassButton _cancelButton;

        public string Text { get; set; }
        public bool HasCancel { get; set; }
        public string AcceptText { get; set; }
        public string CancelText { get; set; }
        public Color TextColor { get; set; }
        public Action<MessageBox> OnAccept { get; set; }
        public Action<MessageBox> OnCancel { get; set; }

        public MessageBox()
        {
            AcceptText = "OK";
            CancelText = "Cancel";
            Size = new Vector2(288f, 128f);
            TextColor = Color.Black;
            Tint = Color.White.WithOpacity(0.5f);
            Text = string.Empty;

            var viewport = Engine.Instance.GraphicsDevice.Viewport;
            var centerScreen = new Vector2(viewport.Width / 2, viewport.Height / 2);
            Position = new Vector2((centerScreen.X) - (Size.X / 2), (centerScreen.Y) - (Size.Y / 2));
        }

        public override void LoadParts()
        {
            _partPanel = Skin[UserInterfacePartNames.GlassPanelCorners];

            _acceptButton = new GlassButton
            {
                Skin = Skin,
                SpriteBatch = SpriteBatch,
                Texture = Skin.Texture,
                Font = Font,
                Text = AcceptText,
                OnClick = b =>
                {
                    OnAccept?.Invoke(this);
                }
            };
            _acceptButton.LoadParts();

            _cancelButton = new GlassButton
            {
                Skin = Skin,
                SpriteBatch = SpriteBatch,
                Texture = Skin.Texture,
                Font = Font,
                Text = CancelText,
                OnClick = b =>
                {
                    OnCancel?.Invoke(this);
                }
            };
            _cancelButton.LoadParts();
        }

        public override void Update(GameTime gameTime)
        {
            _cancelButton.IsVisible = HasCancel;
            _cancelButton.IsEnabled = HasCancel;

            if (_acceptButton.IsEnabled)
            {
                _acceptButton.Position = new Vector2(Position.X + (Size.X / 2) - (_acceptButton.Size.X / 2), Position.Y + ((Size.Y * 3) / 4) - (_acceptButton.Size.Y / 2));
                if (HasCancel) _acceptButton.Position = new Vector2(Position.X + (Size.X / 4) - (_acceptButton.Size.X / 3), Position.Y + ((Size.Y * 3) / 4) - (_acceptButton.Size.Y / 2));
                _acceptButton.Text = AcceptText;
                _acceptButton.Update(gameTime);
            }

            if (_cancelButton.IsEnabled)
            {
                _cancelButton.Position = new Vector2(Position.X + ((Size.X * 3) / 4) - ((_acceptButton.Size.X * 2) / 3), Position.Y + ((Size.Y * 3) / 4) - (_acceptButton.Size.Y / 2));
                _cancelButton.Text = CancelText;
                _cancelButton.Update(gameTime);
            }
        }

        public override void UpdateInput(InputManager input)
        {
            if (_acceptButton.IsEnabled) _acceptButton.UpdateInput(input);
            if (_cancelButton.IsEnabled) _cancelButton.UpdateInput(input);
        }

        public override void Draw(GameTime gameTime)
        {
            var destRect = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

            SpriteBatch.Draw(Texture, null, destRect, _partPanel, Vector2.Zero, 0f, Vector2.One, Tint);

            if (_acceptButton.IsVisible) _acceptButton.Draw(gameTime);
            if (_cancelButton.IsVisible) _cancelButton.Draw(gameTime);

            var textSize = Font.MeasureString(Text);
            var textPos = Position + new Vector2((Size.X / 2) - (textSize.X / 2), (Size.Y / 4) - (textSize.Y / 2));
            SpriteBatch.DrawString(Font, Text, textPos, TextColor);
        }

        public void Show()
        {
            IsEnabled = true;
            IsVisible = true;
        }

        public void Hide()
        {
            IsEnabled = false;
            IsVisible = false;
        }
    }
}