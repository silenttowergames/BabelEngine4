using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    [XmlRoot(ElementName = "tileoffset")]
    public class TiledTileOffset
    {
        [XmlAttribute(AttributeName = "x")]
        public int X;

        [XmlAttribute(AttributeName = "y")]
        public int Y;
    }
}
