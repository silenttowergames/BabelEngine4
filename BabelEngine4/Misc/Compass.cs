using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Misc
{
    public struct Compass
    {
        public static float Legend = (float)(Math.PI / 180);

        float rotation;

        /// <summary>
        /// Human-readable rotation value between 0 and 360 degrees
        /// </summary>
        public float Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value % 360;

                if (rotation < 0)
                {
                    rotation += 360;
                }
            }
        }

        /// <summary>
        /// Calculates & gets the actual rotation value that MonoGame understands
        /// </summary>
        public float RotationReal
        {
            get
            {
                return RotationToReal(Rotation);
            }
        }

        public void TurnTowards(Vector2 PosA, Vector2 PosB)
        {
            Rotation = Face(PosA, PosB);
        }

        public static float Face(Vector2 PosA, Vector2 PosB)
        {
            return RealToRotation((float)Math.Atan2(PosA.Y - PosB.Y, PosA.X - PosB.X));
        }

        public static float RotationToReal(float _Rotation)
        {
            return _Rotation * Legend;
        }

        public static float RealToRotation(float _Real)
        {
            return _Real / Legend;
        }
    }
}
