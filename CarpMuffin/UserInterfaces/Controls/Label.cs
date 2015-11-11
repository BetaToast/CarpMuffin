using CarpMuffin.Extensions;
using CarpMuffin.Input;
using Microsoft.Xna.Framework;

namespace CarpMuffin.UserInterfaces.Controls
{
    /// <summary>
    /// Displays a Label
    /// </summary>
    public class Label
        : Control
    {
        public bool HasShadow { get; set; }
        public Color ShadowTint { get; set; }
        public Vector2 ShadowOffset { get; set; }
        public string Text { get; set; }

        public Label()
        {
            ShadowTint = Color.Black.WithOpacity(0.5f);
            ShadowOffset = new Vector2(2f, 2f);
            Tint = Color.Black;
            HasShadow = true;
        }

        public override void LoadParts()
        {
            // No parts   
        }

        public override void Update(GameTime gameTime)
        {
            // Nothing to update 
        }

        public override void UpdateInput(InputManager input)
        {
            //Nothing to input
        }

        public override void Draw(GameTime gameTime)
        {
            if (HasShadow) SpriteBatch.DrawString(Font, Text, Position + ShadowOffset, ShadowTint);
            SpriteBatch.DrawString(Font, Text, Position, Tint);
        }
    }
}