namespace CarpMuffin.Graphics
{
    /// <summary>
    /// Base interface for all things that update and render to the screen
    /// </summary>
    public interface IRenderable
        : IUpdatable, IDrawable
    {
    }
}