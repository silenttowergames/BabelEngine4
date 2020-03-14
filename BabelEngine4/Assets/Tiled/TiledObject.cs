using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledObject
    {
        [XmlAttribute(AttributeName = "id")]
        public int id;

        [XmlAttribute(AttributeName = "name")]
        public string name;

        [XmlAttribute(AttributeName = "type")]
        public string type;

        [XmlAttribute(AttributeName = "x")]
        public float x;

        [XmlAttribute(AttributeName = "y")]
        public float y;

        [XmlAttribute(AttributeName = "width")]
        public float width;

        [XmlAttribute(AttributeName = "height")]
        public float height;

        [XmlAttribute(AttributeName = "rotation")]
        public int rotation;

        [XmlAttribute(AttributeName = "gid")]
        public int GID;

        [XmlAttribute(AttributeName = "visible")]
        public bool Visible = true;

        [XmlElement(ElementName = "image")]
        public TiledImage image;

        [XmlElement(ElementName = "ellipse")]
        public TiledObjectEllipse ellipse;

        [XmlElement(ElementName = "polygon")]
        public TiledObjectPolygon polygon;

        [XmlElement(ElementName = "polyline")]
        public TiledObjectPolyline polyline;

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public List<TiledProperty> properties;

        Dictionary<string, string> Properties = new Dictionary<string, string>();

        public void Load()
        {
            if (properties != null)
            {
                for (int i = 0; i < properties.Count; i++)
                {
                    Properties.Add(properties[i].name, properties[i].value);
                }
            }
        }

        public string Property(string Key)
        {
            if (!Properties.ContainsKey(Key))
            {
                return null;
            }

            return Properties[Key];
        }
    }
}
