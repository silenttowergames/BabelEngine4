using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Misc
{
    public struct Line
    {
        public float Left, Right;

        public float Length => Right - Left;

        public static bool Intersects(Line line, float point, bool Inclusive = true)
        {
            if (!Inclusive)
            {
                return point > line.Left && point < line.Right;
            }

            return point >= line.Left && point <= line.Right;
        }

        public static bool Intersects(Line l1, Line l2, bool Inclusive = true)
        {
            return Intersects(l1, l2.Left, Inclusive) || Intersects(l1, l2.Right, Inclusive) || Inside(l1, l2);
        }

        public static bool Inside(Line l1, Line l2)
        {
            return l1.Left > l2.Left && l1.Right < l2.Right;
        }

        public Line(float _Left, float _Right)
        {
            Left = _Left;
            Right = _Right;
        }

        public bool Intersects(float point)
        {
            return Intersects(this, point);
        }

        public bool Intersects(Line line, bool Inclusive = true)
        {
            return Intersects(this, line, Inclusive);
        }

        public bool Inside(Line line)
        {
            return Inside(this, line);
        }
    }
}
