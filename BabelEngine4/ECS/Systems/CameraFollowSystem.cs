using BabelEngine4.ECS.Components;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class CameraFollowSystem : IBabelSystem
    {
        EntitySet Set;

        public CameraFollowSystem()
        {
            Set = App.world.GetEntities().With<Body>().With<CameraFollow>().AsSet();
        }

        public void Update()
        {
            foreach (ref readonly Entity E in Set.GetEntities())
            {
                ref Body body = ref E.Get<Body>();
                ref CameraFollow camFollow = ref E.Get<CameraFollow>();

                App.renderTargets[camFollow.RenderTargetID].Center(body.Position);
            }
        }
    }
}
