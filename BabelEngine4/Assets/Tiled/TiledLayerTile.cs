using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledLayerTile : TiledLayer
    {
        [XmlAttribute(AttributeName = "x")]
        public int x;

        [XmlAttribute(AttributeName = "y")]
        public int y;

        [XmlAttribute(AttributeName = "width")]
        public int width;

        [XmlAttribute(AttributeName = "height")]
        public int height;

        [XmlElement(ElementName = "data")]
        public TiledData data;
    }
}
