using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CarpMuffin.Data
{
    /// <summary>
    /// Data Structure for loading User Interface Skins from a .skin file
    /// </summary>
    [XmlRoot("Skin")]
    public class Skin
    {
        [XmlArray("SkinCells")]
        public List<SkinCell> SkinCells { get; set; }

        [XmlAttribute("imagePath")]
        public string ImagePath { get; set; }

        public Skin()
        {
            SkinCells = new List<SkinCell>();
        }

        public static Skin Load(string assetName)
        {
            Skin skin;
            var serializer = new XmlSerializer(typeof(Skin));
            using (var reader = new StreamReader(assetName))
            {
                skin = (Skin)serializer.Deserialize(reader);
            }

            return skin;
        }
    }
}