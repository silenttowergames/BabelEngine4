using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Misc
{
    public struct Compass
    {
        float rotation;

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

        public float RotationReal
        {
            get
            {
                return rotation * (float)(Math.PI / 180);
            }
        }
    }
}
