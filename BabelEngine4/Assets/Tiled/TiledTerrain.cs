using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledTerrain
    {
        [XmlAttribute(AttributeName = "name")]
        public string name;

        [XmlAttribute(AttributeName = "tile")]
        public int tileID;

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public List<TiledProperty> properties = new List<TiledProperty>();
    }
}
