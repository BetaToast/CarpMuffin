using CarpMuffin.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.Sprites
{
    /// <summary>
    /// Base interface for all sprites
    /// </summary>
    public interface ISprite
        : IFocusable
    {
        string Name { get; set; }
        bool IsVisible { get; set; }
        bool IsEnabled { get; set; }
        Texture2D Texture { get; }
        SpriteSheet SpriteSheet { get; set; }
        Rectangle Bounds { get; }
        Rectangle SourceRectangle { get; }
        int Index { get; set; }
        Color Tint { get; set; }
        float Rotation { get; set; }
        Vector2 Origin { get; set; }
        Vector2 Scale { get; set; }
        SpriteEffects SpriteEffects { get; set; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}