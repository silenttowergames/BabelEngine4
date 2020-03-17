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
    public class TiledLayer : TiledGenericLayer
    {
    }
}
