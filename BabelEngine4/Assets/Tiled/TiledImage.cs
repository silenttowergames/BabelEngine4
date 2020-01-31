using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledImage
    {
        [XmlAttribute(AttributeName = "source")]
        public string source;

        [XmlAttribute(AttributeName = "width")]
        public int width;

        [XmlAttribute(AttributeName = "height")]
        public int height;

        [XmlAttribute(AttributeName = "format")]
        public string format;

        [XmlAttribute(AttributeName = "trans")]
        public string trans;

        [XmlElement(ElementName = "data")]
        public TiledData data;
    }
}
