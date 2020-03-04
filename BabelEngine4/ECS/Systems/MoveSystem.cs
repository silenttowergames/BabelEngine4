using BabelEngine4.ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class MoveSystem : IBabelSystem
    {
        public void Reset()
        {
            // nothing
        }

        public void OnLoad()
        {
            // nothing
        }

        public void Update()
        {
            Span<Body> bodies = App.world.Get<Body>();

            for(int i = 0; i < bodies.Length; i++)
            {
                bodies[i].Position += bodies[i].Velocity;

                bodies[i].Velocity = Vector2.Zero;
            }
        }
    }
}
