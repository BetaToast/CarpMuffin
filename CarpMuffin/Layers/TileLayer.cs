using System.Linq;
using CarpMuffin.Tiles;

namespace CarpMuffin.Layers
{
    /// <summary>
    /// Displays a layer of tiles on a tile map
    /// </summary>
    public class TileLayer
        : Layer<ITile>
    {
        public override void Add(ITile item)
        {
            item.SpriteBatch = SpriteBatch;
            base.Add(item);
        }

        public bool IsTileAt(int x, int y)
        {
            return (from tile in Items
                    let tx = tile.Position.X
                    let ty = tile.Position.Y
                    where tx == x && ty == y
                    select tx).Any();
        }
    }
}