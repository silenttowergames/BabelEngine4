using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    [XmlRoot(ElementName = "tileset")]
    public class TiledTileset
    {
        [XmlAttribute(AttributeName = "firstgid")]
        public int firstGID;

        [XmlAttribute(AttributeName = "name")]
        public string name;

        [XmlAttribute(AttributeName = "source")]
        public string source;

        [XmlAttribute(AttributeName = "tilewidth")]
        public int tileWidth;

        [XmlAttribute(AttributeName = "tileheight")]
        public int tileHeight;

        [XmlAttribute(AttributeName = "spacing")]
        public int spacing;

        [XmlAttribute(AttributeName = "margin")]
        public int margin;

        [XmlAttribute(AttributeName = "tilecount")]
        public int tileCount;

        [XmlAttribute(AttributeName = "columns")]
        public int columns;

        [XmlElement(ElementName = "tileoffset")]
        public TiledTileOffset tileOffset;

        [XmlElement(ElementName = "tile")]
        public List<TiledTilesetTile> tiles;

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public List<TiledProperty> properties;

        [XmlElement(ElementName = "image")]
        public TiledImage image;

        [XmlArray("terraintypes")]
        [XmlArrayItem("terrain")]
        public List<TiledTerrain> terrainTypes;
    }
}
