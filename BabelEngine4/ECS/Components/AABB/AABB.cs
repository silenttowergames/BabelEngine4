using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Components.AABB
{
    public struct AABB
    {
        public List<long> Cells;

        public Hitbox[] Hitboxes;

        public bool HitRight()
        {
            for (int i = 0; i < Hitboxes.Length; i++)
            {
                if (Hitboxes[i].HitRight)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HitLeft()
        {
            for (int i = 0; i < Hitboxes.Length; i++)
            {
                if (Hitboxes[i].HitLeft)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HitBottom()
        {
            for (int i = 0; i < Hitboxes.Length; i++)
            {
                if (Hitboxes[i].HitBottom)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HitTop()
        {
            for (int i = 0; i < Hitboxes.Length; i++)
            {
                if (Hitboxes[i].HitTop)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
