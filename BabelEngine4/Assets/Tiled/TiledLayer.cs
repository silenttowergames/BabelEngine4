using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    [XmlInclude(typeof(TiledLayerTile))]
    [XmlInclude(typeof(TiledLayerImage))]
    public class TiledLayer
    {
        [XmlAttribute(AttributeName = "id")]
        public int ID;

        [XmlAttribute(AttributeName = "offsetx")]
        public float offsetx;

        [XmlAttribute(AttributeName = "offsety")]
        public float offsety;

        [XmlAttribute(AttributeName = "name")]
        public string name;

        [XmlAttribute(AttributeName = "opacity")]
        public float opacity = 1;

        [XmlAttribute(AttributeName = "visible")]
        public bool Visible = true;

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
