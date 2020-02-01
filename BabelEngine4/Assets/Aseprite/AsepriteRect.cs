using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets.Aseprite
{
    public struct AsepriteRect
    {
        public int x, y, w, h;

        public Rectangle ToRect()
        {
            return new Rectangle(x, y, w, h);
        }
    }
}
