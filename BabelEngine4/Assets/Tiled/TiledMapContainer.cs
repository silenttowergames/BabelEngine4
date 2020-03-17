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

            for(int L = 0; L < map.allLayers.Count; L++)
            {
                map.allLayers[L].Load();

                if (map.allLayers[L] is TiledObjectGroup)
                {
                    TiledObjectGroup layer = (TiledObjectGroup)map.allLayers[L];

                    for (int O = 0; O < layer.objects.Count; O++)
                    {
                        layer.objects[O].Load();
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
