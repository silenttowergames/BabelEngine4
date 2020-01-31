using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledObjectGroup
    {
        public TiledObjectGroup()
        {
            LayerDepth = ++App.LayerIDCounter;
        }

        public int LayerDepth;

        [XmlAttribute(AttributeName = "id")]
        public int ID;

        [XmlAttribute(AttributeName = "offsetx")]
        public float OffsetX;

        [XmlAttribute(AttributeName = "offsety")]
        public float OffsetY;

        [XmlAttribute(AttributeName = "name")]
        public string name;

        [XmlAttribute(AttributeName = "color")]
        public string color;

        [XmlAttribute(AttributeName = "opacity")]
        public float opacity = 1;

        [XmlAttribute(AttributeName = "visible")]
        public bool Visible = true;

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public List<TiledProperty> properties;

        [XmlElement(ElementName = "object")]
        public List<TiledObject> objects = new List<TiledObject>();
    }
}
