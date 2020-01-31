using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledProperty
    {
        public static string prop(List<TiledProperty> properties, string Key)
        {
            foreach (TiledProperty property in properties)
            {
                if (property.name == Key)
                {
                    return property.value;
                }
            }

            return null;
        }

        [XmlAttribute(AttributeName = "name")]
        public string name;

        [XmlAttribute(AttributeName = "value")]
        public string value;
    }
}
