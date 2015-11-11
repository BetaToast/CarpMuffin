using CarpMuffin.Extensions;
using CarpMuffin.UserInterfaces.Controls.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.UserInterfaces.Controls
{
    /// <summary>
    /// Displays a glass button
    /// </summary>
    public class GlassButton
        : Button
    {
        public Texture2D WhitePixel { get; set; }
        public Color BacklightColor { get; set; }

        public GlassButton()
        {
            TextColor = Color.White;
            Size = new Vector2(100f, 48f);
            BacklightColor = Color.MidnightBlue.WithOpacity(0.33f);
        }

        public override void LoadParts()
        {
            WhitePixel = new Texture2D(Engine.Instance.GraphicsDevice, 1, 1);
            WhitePixel.SetData(new[] { Color.White });

            PartLeftSide = Skin[UserInterfacePartNames.GlassPanelProjection];
            PartRightSide = Skin[UserInterfacePartNames.GlassPanelProjection];
            PartMid = Skin[UserInterfacePartNames.GlassPanelProjection];

            var x = PartLeftSide.X;
            var width = PartLeftSide.Width;
            var sideWidth = 5;

            PartLeftSide.Width = sideWidth;

            PartRightSide.X += (width - sideWidth);
            PartRightSide.Width = sideWidth;

            PartMid.X = x + sideWidth;
            PartMid.Width = width - (sideWidth * 2);
        }

        public override void Draw(GameTime gameTime)
        {
            var leftWidth = PartLeftSide.Width;
            var rightWidth = PartRightSide.Width;
            var midWidth = PartMid.Width;
            var height = Size.Y;
            var leftPos = Position;
            var rightPos = Position + new Vector2(Size.X - rightWidth, 0f);
            var midPos = Position + new Vector2(leftWidth, 0f);
            var shadowOffset = new Vector2(4f, 4f);
            var offset = State == ButtonState.Click ? shadowOffset : Vector2.Zero;

            // Back Light
            if (State != ButtonState.Normal)
            {
                var backLightRect = new Rectangle((int)(Position.X + leftWidth + offset.X), (int)(Position.Y + shadowOffset.Y + offset.Y), midWidth, (int)height - 8);
                SpriteBatch.Draw(WhitePixel, null, backLightRect, PartMid, Vector2.Zero, 0f, Vector2.One, BacklightColor);
            }

            // Button
            var destRect = new Rectangle((int)(leftPos.X + offset.X), (int)(leftPos.Y + offset.Y), leftWidth, (int)height);
            SpriteBatch.Draw(Texture, null, destRect, PartLeftSide, Vector2.Zero, 0f, Vector2.One, Tint);

            for (var xOffset = 0; xOffset < Size.X - leftWidth - rightWidth; xOffset += midWidth)
            {
                var pos = midPos + new Vector2(xOffset, 0) + offset;
                destRect = new Rectangle((int)pos.X, (int)pos.Y, midWidth, (int)height);
                if (destRect.Right > rightPos.X)
                {
                    var width = rightPos.X - pos.X + offset.X;
                    destRect = new Rectangle((int)pos.X, (int)pos.Y, (int)width, (int)height);
                }
                SpriteBatch.Draw(Texture, null, destRect, PartMid, Vector2.Zero, 0f, Vector2.One, Tint);
            }

            destRect = new Rectangle((int)(rightPos.X + offset.X), (int)(rightPos.Y + offset.Y), rightWidth, (int)height);
            SpriteBatch.Draw(Texture, null, destRect, PartRightSide, Vector2.Zero, 0f, Vector2.One, Tint);

            // Text
            var textSize = Font.MeasureString(Text);
            var textPos = Position + new Vector2((Size.X / 2) - (textSize.X / 2), (Size.Y / 2) - (textSize.Y / 2)) + offset;
            SpriteBatch.DrawString(Font, Text, textPos, TextColor);
        }
    }
}