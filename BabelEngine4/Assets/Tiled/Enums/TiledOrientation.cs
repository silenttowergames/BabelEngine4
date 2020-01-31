using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled.Enums
{
    public enum TiledOrientation
    {
        [XmlEnum(Name = "orthogonal")]
        Orthogonal,
        [XmlEnum(Name = "isometric")]
        Isometric,
        [XmlEnum(Name = "staggered")]
        Staggered,
        [XmlEnum(Name = "hexagonal")]
        Hexagonal,
    }
}
