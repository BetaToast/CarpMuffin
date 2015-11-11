using System;
using System.Collections.Generic;
using CarpMuffin.Input;
using Microsoft.Xna.Framework;

namespace CarpMuffin.UserInterfaces.Controls
{
    /// <summary>
    /// Displays a Radio Button control
    /// </summary>
    public class RadioButton
        : Control
    {
        public static Dictionary<string, RadioButton> Groups = new Dictionary<string, RadioButton>();

        private string _groupName;
        private bool _isSelected;
        private Rectangle _partDot;


        public Color SelectedTint { get; set; }
        public string Text { get; set; }
        public Color TextColor { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                if (_isSelected) Groups[GroupName] = this;
            }
        }

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                if (!Groups.ContainsKey(value))
                {
                    Groups.Add(value, this);
                }
            }
        }

        public RadioButton()
        {
            SelectedTint = Color.Lime;
            Text = String.Empty;
            TextColor = Color.Black;
            IsSelected = false;
            Size = new Vector2(24f, 24f);
        }

        public override void LoadParts()
        {
            _partDot = Skin[UserInterfacePartNames.DotWhite];
        }

        public override void Update(GameTime gameTime)
        {
            if (Groups[GroupName] != this) IsSelected = false;
        }

        public override void UpdateInput(InputManager input)
        {
            if (input.Mouse.Bounds.Intersects(Bounds))
            {
                if (input.Mouse.IsButtonPressed(MouseButtons.Left))
                {
                    IsSelected = true;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var color = IsSelected ? SelectedTint : Tint;
            SpriteBatch.Draw(Texture, Position, _partDot, color);

            var textSize = Font.MeasureString(Text);
            var textPos = Position + new Vector2(_partDot.Width + (_partDot.Width / 2), (Size.Y / 2) - (textSize.Y / 2));
            SpriteBatch.DrawString(Font, Text, textPos, TextColor);
        }
    }
}