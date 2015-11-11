using CarpMuffin.Extensions;
using CarpMuffin.Input;
using Microsoft.Xna.Framework;

namespace CarpMuffin.UserInterfaces.Controls
{
    /// <summary>
    /// Displays a progress bar control
    /// </summary>
    public class ProgressBar
        : Control
    {
        private Rectangle _partLeftSide;
        private Rectangle _partRightSide;
        private Rectangle _partMid;

        private Rectangle _partLeftSideShadow;
        private Rectangle _partRightSideShadow;
        private Rectangle _partMidShadow;


        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public double Value { get; set; }
        public Color ShadowTint { get; set; }

        public ProgressBar()
        {
            Minimum = 0;
            Maximum = 100;
            Value = 0;

            Size = new Vector2(120f, 26f);
            ShadowTint = Color.White.WithOpacity(0.5f);
        }

        public override void LoadParts()
        {
            _partLeftSide = Skin[UserInterfacePartNames.BarHorizontalWhiteLeft];
            _partRightSide = Skin[UserInterfacePartNames.BarHorizontalWhiteRight];
            _partMid = Skin[UserInterfacePartNames.BarHorizontalWhiteMid];

            _partLeftSideShadow = Skin[UserInterfacePartNames.BarHorizontalShadowLeft];
            _partRightSideShadow = Skin[UserInterfacePartNames.BarHorizontalShadowRight];
            _partMidShadow = Skin[UserInterfacePartNames.BarHorizontalShadowMid];

            _partMid.Width = 1;
        }

        public override void Update(GameTime gameTime)
        {
            if (Value > Maximum) Value = Maximum;
            if (Value < Minimum) Value = Minimum;
        }

        public override void UpdateInput(InputManager input)
        {
            // No input required

            // Debug settings
            //if (input.Keyboard.IsKeyHeld(Keys.Right)) Value++;
            //if (input.Keyboard.IsKeyHeld(Keys.Left)) Value--;
        }


        public override void Draw(GameTime gameTime)
        {
            var leftWidth = _partLeftSide.Width;
            var rightWidth = _partRightSide.Width;
            var midWidth = _partMid.Width;
            var midHeight = _partMid.Height;
            var leftPos = Position;
            var rightPos = Position + new Vector2(Size.X - rightWidth, 0f);
            var midPos = Position + new Vector2(leftWidth, 0f);
            Rectangle destRect;

            // Shadow
            SpriteBatch.Draw(Texture, leftPos, _partLeftSideShadow, Color.White);

            for (var xOffset = 0; xOffset < Size.X - leftWidth - rightWidth; xOffset += midWidth)
            {
                var pos = midPos + new Vector2(xOffset, 0);
                destRect = new Rectangle((int)pos.X, (int)pos.Y, midWidth, midHeight);
                if (destRect.Right > rightPos.X)
                {
                    var width = rightPos.X - pos.X;
                    destRect = new Rectangle((int)pos.X, (int)pos.Y, (int)width, midHeight);
                }
                SpriteBatch.Draw(Texture, null, destRect, _partMidShadow, Vector2.Zero, 0f, Vector2.One, Color.White);
            }

            SpriteBatch.Draw(Texture, rightPos, _partRightSideShadow, Color.White);

            // Fill
            var sidePercent = _partLeftSide.Width / Size.X;
            var innerWidth = Size.X - leftWidth - rightWidth;
            var leftMaxValue = (Maximum * sidePercent);
            var rightMinValue = Maximum - (Maximum * sidePercent);
            var rightMaxValue = Maximum - rightMinValue;
            var midMaximum = Maximum - leftMaxValue - rightMaxValue;

            if (Value < leftMaxValue)
            {
                // Left Cap
                var leftPercentWidth = (Value / (Maximum * sidePercent)) * leftWidth;
                destRect = new Rectangle((int)Position.X, (int)Position.Y, (int)leftPercentWidth, midHeight);
                SpriteBatch.Draw(Texture, null, destRect, _partLeftSide, Vector2.Zero, 0f, Vector2.One, Tint);
            }
            else
            {
                var midValue = Value - leftMaxValue;
                var innerPercentWidth = (midValue / midMaximum) * innerWidth;

                // Left Cap
                destRect = new Rectangle((int)Position.X, (int)Position.Y, leftWidth, midHeight);
                SpriteBatch.Draw(Texture, null, destRect, _partLeftSide, Vector2.Zero, 0f, Vector2.One, Tint);

                // Mid Bar
                for (var xOffset = 0; xOffset < innerPercentWidth; xOffset++)
                {
                    var pos = midPos + new Vector2(xOffset, 0);
                    destRect = new Rectangle((int)pos.X, (int)pos.Y, midWidth, midHeight);
                    if (destRect.Right > rightPos.X)
                    {
                        var width = rightPos.X - pos.X;
                        destRect = new Rectangle((int)pos.X, (int)pos.Y, (int)width, midHeight);
                    }
                    SpriteBatch.Draw(Texture, null, destRect, _partMid, Vector2.Zero, 0f, Vector2.One, Tint);
                }

                // Right Cap
                if (Value > rightMinValue)
                {
                    var rightValue = Value - rightMinValue;
                    var rightPercentWidth = (rightValue / rightMaxValue) * leftWidth;
                    destRect = new Rectangle((int)rightPos.X, (int)rightPos.Y, (int)rightPercentWidth, midHeight);
                    SpriteBatch.Draw(Texture, null, destRect, _partRightSide, Vector2.Zero, 0f, Vector2.One, Tint);
                }
            }
        }
    }
}