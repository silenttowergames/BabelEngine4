using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Misc
{
    public struct RectangleF
    {
        public float
            X,
            Y,
            Width,
            Height
        ;

        public Vector2 Position => new Vector2(X, Y);

        public Vector2 Size => new Vector2(Width, Height);

        public Line LineX => new Line(Left, Right);

        public Line LineY => new Line(Top, Bottom);

        public float Left
        {
            get
            {
                return X;
            }

            set
            {
                Width += X - value;

                X = value;
            }
        }

        public float Right
        {
            get
            {
                return X + Width;
            }

            set
            {
                Width += value - Right;
            }
        }

        public float Top
        {
            get
            {
                return Y;
            }

            set
            {
                Height += Y - value;

                Y = value;
            }
        }

        public float Bottom
        {
            get
            {
                return Y + Height;
            }

            set
            {
                Width += value - Bottom;
            }
        }

        public static bool Intersects(RectangleF r, Vector2 point)
        {
            return r.LineX.Intersects(point.X) || r.LineY.Intersects(point.Y);
        }

        public static bool Intersects(RectangleF r1, RectangleF r2)
        {
            return r1.LineX.Intersects(r2.LineX) && r1.LineY.Intersects(r2.LineY);
        }

        public RectangleF(float _X, float _Y, float _Width, float _Height)
        {
            X = _X;
            Y = _Y;
            Width = _Width;
            Height = _Height;
        }

        public RectangleF(Vector2 _Position, Vector2 _Size)
        {
            X = _Position.X;
            Y = _Position.Y;
            Width = _Size.X;
            Height = _Size.Y;
        }

        public RectangleF(Rectangle r)
        {
            X = r.X;
            Y = r.Y;
            Width = r.Width;
            Height = r.Height;
        }

        public bool Intersects(Vector2 point)
        {
            return Intersects(this, point);
        }

        public bool Intersects(RectangleF r)
        {
            return Intersects(this, r);
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}:{2}x{3}", X, Y, Width, Height);
        }
    }
}
