using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled.Enums
{
    public enum TiledRenderOrder
    {
        [XmlEnum(Name = "right-down")]
        RightDown,
        [XmlEnum(Name = "right-up")]
        RightUp,
        [XmlEnum(Name = "left-down")]
        LeftDown,
        [XmlEnum(Name = "left-up")]
        LeftUp,
    }
}
