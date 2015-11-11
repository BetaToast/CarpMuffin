using CarpMuffin.Input;
using Microsoft.Xna.Framework;

namespace CarpMuffin.UserInterfaces.Controls
{
    /// <summary>
    /// Displays any part specified from the skin
    /// </summary>
    public class Part
        : Control
    {
        private string _basePartName = UserInterfacePartNames.SquareWhite;

        private Rectangle _partBase;

        public override void LoadParts()
        {
            _partBase = Skin[_basePartName];
            Size = new Vector2(_partBase.Width, _partBase.Height);
        }

        public virtual void ReloadParts(string userInterfaceSkinPartName)
        {
            _basePartName = userInterfaceSkinPartName;
            LoadParts();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void UpdateInput(InputManager input)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            var destRect = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            SpriteBatch.Draw(Texture, null, destRect, _partBase, Vector2.Zero, 0f, Vector2.One, Tint);
        }
    }
}