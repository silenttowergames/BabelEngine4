using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledData
    {
        [XmlAttribute(AttributeName = "encoding")]
        public string encoding;

        [XmlAttribute(AttributeName = "compression")]
        public string compression;

        [XmlElement(ElementName = "tile")]
        public List<TiledDataTile> tiles = new List<TiledDataTile>();

        [XmlText]
        public string value;
    }
}
