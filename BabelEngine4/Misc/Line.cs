using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Misc
{
    public struct Line
    {
        public float Beginning, End;

        public float Length => End - Beginning;

        public static bool Intersects(Line line, float point, bool Inclusive = true)
        {
            if (!Inclusive)
            {
                return point > line.Beginning && point < line.End;
            }

            return point >= line.Beginning && point <= line.End;
        }

        public static bool Intersects(Line l1, Line l2, bool Inclusive = true)
        {
            return Intersects(l1, l2.Beginning, Inclusive) || Intersects(l1, l2.End, Inclusive) || Inside(l1, l2);
        }

        public static bool Inside(Line l1, Line l2)
        {
            if (l1.Beginning == l2.Beginning || l1.End == l2.End)
            {
                return true;
            }

            return l1.Beginning > l2.Beginning && l1.End < l2.End;
        }

        public Line(float _Left, float _Right)
        {
            Beginning = _Left;
            End = _Right;
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
