using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Components
{
    public struct Body
    {
        public Vector2
            Position,
            Velocity,
            InitialVelocity,
            EffectiveVelocity
        ;
    }
}
