using BabelEngine4.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Components.AABB
{
    public struct Hitbox
    {
        public bool
            PassThrough,
            PassThroughTop,
            PassThroughBottom,
            PassThroughRight,
            PassThroughLeft
        ;

        public bool Solid => !PassThrough;

        public bool SolidTop => !PassThroughTop;

        public bool SolidBottom => !PassThroughBottom;

        public bool SolidRight => !PassThroughRight;

        public bool SolidLeft => !PassThroughLeft;

        public RectangleF Bounds;
    }
}
