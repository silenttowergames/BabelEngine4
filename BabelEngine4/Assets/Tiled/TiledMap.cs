using BabelEngine4.Assets.Tiled.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    [XmlRoot(ElementName = "map")]
    public class TiledMap
    {
        [XmlAttribute(AttributeName = "firstgid")]
        public int firstGID;

        [XmlAttribute(AttributeName = "height")]
        public int height;

        [XmlAttribute(AttributeName = "width")]
        public int width;

        [XmlAttribute(AttributeName = "tileheight")]
        public int tileWidth;

        [XmlAttribute(AttributeName = "tilewidth")]
        public int tileHeight;

        [XmlAttribute(AttributeName = "nextobjectid")]
        public int nextObjectID;

        [XmlAttribute(AttributeName = "orientation")]
        public TiledOrientation orientation;

        [XmlAttribute(AttributeName = "renderorder")]
        public TiledRenderOrder renderOrder;

        [XmlElement(ElementName = "objectgroup")]
        public List<TiledObjectGroup> objectGroups;

        [XmlElement(ElementName = "layer", Type = typeof(TiledLayerTile))]
        [XmlElement(ElementName = "imagelayer", Type = typeof(TiledLayerImage))]
        public List<TiledLayer> layers;

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public List<TiledProperty> properties;

        [XmlElement(ElementName = "tileset")]
        public TiledTileset tileset;

        [XmlAttribute(AttributeName = "backgroundcolor")]
        public string backgroundColor;

        [XmlAttribute(AttributeName = "version")]
        public string version;
    }
}
