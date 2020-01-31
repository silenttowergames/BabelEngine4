using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets.Aseprite
{
    public class AsepriteMeta
    {
        public AsepriteAnimation[] frameTags;

        public AsepriteVec2 size;

        public string app, format, image, version, scale;

        // wow this sucks
        public dynamic[] slices;
    }
}
