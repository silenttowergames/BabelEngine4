using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.Rendering;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class FlipSpriteOnXSystem : SystemSkeleton
    {
        EntitySet entities;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<Sprite>().With<Body>().With<FlipSpriteOnX>().AsSet();
        }

        public override void Update()
        {
            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Body body = ref entity.Get<Body>();
                ref Sprite sprite = ref entity.Get<Sprite>();

                if (body.EffectiveVelocity.X < 0)
                {
                    sprite.FlippedX = true;

                    continue;
                }

                if (body.EffectiveVelocity.X > 0)
                {
                    sprite.FlippedX = false;
                }
            }
        }
    }
}
