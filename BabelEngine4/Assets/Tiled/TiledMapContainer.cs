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

                //XmlSerializer TS = new XmlSerializer(typeof(TiledTileset));
            }
        }
    }
}
