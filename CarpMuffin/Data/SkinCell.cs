using System.Xml.Serialization;

namespace CarpMuffin.Data
{
    /// <summary>
    /// Data structure for loading sub components of a .skin file
    /// </summary>
    public class SkinCell
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("x")]
        public int X { get; set; }

        [XmlAttribute("y")]
        public int Y { get; set; }

        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }
    }
}