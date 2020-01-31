using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledTilesetTile
    {
        [XmlAttribute(AttributeName = "id")]
        public int ID;

        [XmlElement(ElementName = "terrain")]
        public TiledTerrain terrain;

        [XmlAttribute(AttributeName = "probability")]
        public float probability = 1;

        [XmlElement(ElementName = "image")]
        public TiledImage image;

        [XmlElement(ElementName = "objectgroup")]
        public List<TiledObjectGroup> objectGroups;

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public List<TiledProperty> properties;

        [XmlArray("animation")]
        [XmlArrayItem("frame")]
        public List<TiledTilesetTileAnimationFrame> animationFrames;
    }
}
