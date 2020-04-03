using BabelEngine4.Misc;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Components.AABB
{
    public struct Hitbox
    {
        public static Hitbox Square(int S)
        {
            return new Hitbox()
            {
                Bounds = new RectangleF(0, 0, S, S)
            };
        }

        public bool
            PassThrough,
            PassThroughTop,
            PassThroughBottom,
            PassThroughRight,
            PassThroughLeft,

            HitBottom,
            HitRight,
            HitLeft,
            HitTop
        ;

        public bool Solid => !PassThrough && (SolidTop || SolidBottom || SolidRight || SolidLeft);

        public bool SolidTop => !PassThroughTop;

        public bool SolidBottom => !PassThroughBottom;

        public bool SolidRight => !PassThroughRight;

        public bool SolidLeft => !PassThroughLeft;

        public RectangleF Bounds;

        public RectangleF GetRealBounds(Body body, bool IncludeVelocity = false)
        {
            RectangleF bounds = GetRealBounds(body.Position);

            if (IncludeVelocity)
            {
                if (body.Velocity.X < 0)
                {
                    bounds.Left += body.Velocity.X;
                }
                else
                {
                    bounds.Right += body.Velocity.X;
                }

                if (body.Velocity.Y < 0)
                {
                    bounds.Top += body.Velocity.Y;
                }
                else
                {
                    bounds.Bottom += body.Velocity.Y;
                }
            }

            return bounds;
        }

        public RectangleF GetRealBounds(Vector2 Position)
        {
            RectangleF bounds = new RectangleF(
                Position + Bounds.Position - (Bounds.Size / 2),
                Bounds.Size
            );

            return bounds;
        }
    }
}
