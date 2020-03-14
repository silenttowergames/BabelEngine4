using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledMapContainer
    {
        public string Data;

        public TiledMap map;

        Dictionary<string, string> Properties = new Dictionary<string, string>();

        public TiledMapContainer()
        {
            Data = "";
        }

        public TiledMapContainer(string _Data)
        {
            Data = _Data;
        }

        public void LoadMap()
        {
            StringReader S = new StringReader(Data);

            using (XmlReader reader = XmlReader.Create(S))
            {
                XmlSerializer XS = new XmlSerializer(typeof(TiledMap));

                map = (TiledMap)XS.Deserialize(reader);
            }

            if (map.properties != null)
            {
                for (int i = 0; i < map.properties.Count; i++)
                {
                    Properties.Add(map.properties[i].name, map.properties[i].value);
                }
            }

            for (int i = 0; i < Math.Max(map.layers.Count, map.objectGroups.Count); i++)
            {
                if (i < map.layers.Count)
                {
                    map.layers[i].Load();
                }

                if (i < map.objectGroups.Count)
                {
                    map.objectGroups[i].Load();

                    for (int j = 0; j < map.objectGroups[i].objects.Count; j++)
                    {
                        map.objectGroups[i].objects[j].Load();
                    }
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
