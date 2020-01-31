using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledLayerImage : TiledLayer
    {
        [XmlElement(ElementName = "image")]
        public TiledImage image;
    }
}
