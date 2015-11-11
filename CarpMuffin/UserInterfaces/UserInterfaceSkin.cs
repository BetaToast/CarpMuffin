using System.Collections.Generic;
using System.IO;
using CarpMuffin.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CarpMuffin.UserInterfaces
{
    /// <summary>
    /// The skin for a user interface
    /// </summary>
    public class UserInterfaceSkin
    {
        public Texture2D Texture { get; set; }
        public List<Rectangle> Cells { get; set; }
        public List<string> CellNames { get; set; }

        public Rectangle this[int index] => Cells[index];
        public Rectangle this[string alias] => Cells[CellNames.IndexOf(alias)];

        public UserInterfaceSkin()
        {
            Cells = new List<Rectangle>();
            CellNames = new List<string>();
        }

        public static UserInterfaceSkin Load(ContentManager content, string assetName)
        {
            var fullAssetName = $".\\{content.RootDirectory}\\{assetName}";
            var skinData = Skin.Load(fullAssetName);
            var ret = new UserInterfaceSkin();

            var uiPath = Path.GetDirectoryName(fullAssetName);
            var spriteSheetPath = $"{uiPath}\\{skinData.ImagePath}".Replace($".\\{content.RootDirectory}\\", "");
            ret.Texture = content.Load<Texture2D>(spriteSheetPath);

            foreach (var cell in skinData.SkinCells)
            {
                var name = cell.Name;
                var bounds = new Rectangle(cell.X, cell.Y, cell.Width, cell.Height);
                ret.Cells.Add(bounds);
                ret.CellNames.Add(name);
            }

            return ret;
        }
    }
}