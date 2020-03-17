using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledObjectGroup : TiledGenericLayer
    {
        [XmlAttribute(AttributeName = "color")]
        public string color;

        [XmlElement(ElementName = "object")]
        public List<TiledObject> objects = new List<TiledObject>();
    }
}
