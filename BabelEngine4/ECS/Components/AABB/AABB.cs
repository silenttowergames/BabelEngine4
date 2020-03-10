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
    }
}
