using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets.Aseprite
{
    public class AsepriteFrame
    {
        public AsepriteRect frame, spriteSourceSize;

        public AsepriteVec2 sourceSize;

        public bool rotated, trimmed;

        public float duration;
    }
}
