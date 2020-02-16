using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.AI;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class DirectorSystem : IBabelSystem
    {
        EntitySet EntitiesSet = null;

        float Speed = 0.5f;

        public DirectorSystem()
        {
            EntitiesSet = App.world.GetEntities().With<Director>().With<Body>().AsSet();
        }

        public void Update()
        {
            ReadOnlySpan<Entity> Entities = EntitiesSet.GetEntities();

            foreach (ref readonly Entity entity in Entities)
            {
                ref Director director = ref entity.Get<Director>();
                ref Body body = ref entity.Get<Body>();

                if (director.MoveRight)
                {
                    body.Velocity.X += Speed;
                }

                if (director.MoveLeft)
                {
                    body.Velocity.X -= Speed;
                }

                if (director.MoveDown)
                {
                    body.Velocity.Y += Speed;
                }

                if (director.MoveUp)
                {
                    body.Velocity.Y -= Speed;
                }
            }
        }
    }
}
