using System;
using CarpMuffin.Input;
using CarpMuffin.UserInterfaces.Controls.States;
using Microsoft.Xna.Framework;

namespace CarpMuffin.UserInterfaces.Controls
{
    /// <summary>
    ///  Displays a button
    /// </summary>
    public class Button
        : Control
    {
        protected Rectangle PartLeftSide;
        protected Rectangle PartRightSide;
        protected Rectangle PartMid;

        protected Rectangle PartLeftSideShadow;
        protected Rectangle PartRightSideShadow;
        protected Rectangle PartMidShadow;

        public string Text { get; set; }
        public Color TextColor { get; set; }
        public ButtonState State { get; set; }
        public Action<Button> OnClick { get; set; }

        public Button()
        {
            Size = new Vector2(60f, 26f);
            TextColor = Color.Black;
            State = ButtonState.Normal;
            Text = string.Empty;
        }

        public override void LoadParts()
        {
            PartLeftSide = Skin[UserInterfacePartNames.BarHorizontalWhiteLeft];
            PartRightSide = Skin[UserInterfacePartNames.BarHorizontalWhiteRight];
            PartMid = Skin[UserInterfacePartNames.BarHorizontalWhiteMid];

            PartLeftSideShadow = Skin[UserInterfacePartNames.BarHorizontalShadowLeft];
            PartRightSideShadow = Skin[UserInterfacePartNames.BarHorizontalShadowRight];
            PartMidShadow = Skin[UserInterfacePartNames.BarHorizontalShadowMid];
        }

        public override void Update(GameTime gameTime)
        {
            // Nothing to see here
        }

        public override void UpdateInput(InputManager input)
        {
            State = ButtonState.Normal;
            if (input.Mouse.Bounds.Intersects(Bounds))
            {
                State = ButtonState.Hover;
                if (input.Mouse.IsButtonPressed(MouseButtons.Left) || input.Mouse.IsButtonHeld(MouseButtons.Left))
                {
                    State = ButtonState.Click;
                }
                if (input.Mouse.IsButtonPressed(MouseButtons.Left)) OnClick?.Invoke(this);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var leftWidth = PartLeftSide.Width;
            var rightWidth = PartRightSide.Width;
            var midWidth = PartMid.Width;
            var midHeight = PartMid.Height;
            var leftPos = Position;
            var rightPos = Position + new Vector2(Size.X - rightWidth, 0f);
            var midPos = Position + new Vector2(leftWidth, 0f);
            var shadowOffset = new Vector2(4f, 4f);

            // Shadow
            if (State != ButtonState.Normal)
            {
                SpriteBatch.Draw(Texture, leftPos + shadowOffset, PartLeftSideShadow, Color.White);

                for (var xOffset = 0; xOffset < Size.X - leftWidth - rightWidth; xOffset += midWidth)
                {
                    var pos = midPos + new Vector2(xOffset, 0) + shadowOffset;
                    var destRect = new Rectangle((int)pos.X, (int)pos.Y, midWidth, midHeight);
                    if (destRect.Right > rightPos.X)
                    {
                        var width = rightPos.X - pos.X + shadowOffset.X;
                        destRect = new Rectangle((int)pos.X, (int)pos.Y, (int)width, midHeight);
                    }
                    SpriteBatch.Draw(Texture, null, destRect, PartMidShadow, Vector2.Zero, 0f, Vector2.One, Color.White);
                }

                SpriteBatch.Draw(Texture, rightPos + shadowOffset, PartRightSideShadow, Color.White);
            }

            // Button
            var offset = State == ButtonState.Click ? shadowOffset : Vector2.Zero;
            SpriteBatch.Draw(Texture, leftPos + offset, PartLeftSide, Tint);

            for (var xOffset = 0; xOffset < Size.X - leftWidth - rightWidth; xOffset += midWidth)
            {
                var pos = midPos + new Vector2(xOffset, 0) + offset;
                var destRect = new Rectangle((int)pos.X, (int)pos.Y, midWidth, midHeight);
                if (destRect.Right > rightPos.X)
                {
                    var width = rightPos.X - pos.X + offset.X;
                    destRect = new Rectangle((int)pos.X, (int)pos.Y, (int)width, midHeight);
                }
                SpriteBatch.Draw(Texture, null, destRect, PartMid, Vector2.Zero, 0f, Vector2.One, Tint);
            }

            SpriteBatch.Draw(Texture, rightPos + offset, PartRightSide, Tint);

            // Text
            var textSize = Font.MeasureString(Text);
            var textPos = Position + new Vector2((Size.X / 2) - (textSize.X / 2), (Size.Y / 2) - (textSize.Y / 2)) + offset;
            SpriteBatch.DrawString(Font, Text, textPos, TextColor);
        }
    }
}