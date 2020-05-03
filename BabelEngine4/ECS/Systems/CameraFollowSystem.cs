using BabelEngine4.ECS.Components;
using DefaultEcs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class CameraFollowSystem : SystemSkeleton
    {
        EntitySet Set = null;

        public override void Update()
        {
            Vector2 FollowPos;

            foreach (ref readonly Entity E in Set.GetEntities())
            {
                ref Body body = ref E.Get<Body>();
                ref CameraFollow camFollow = ref E.Get<CameraFollow>();

                FollowPos = body.Position - camFollow.Offset;

                if (camFollow.RoundToX != 0)
                {
                    FollowPos.X -= FollowPos.X % camFollow.RoundToX;
                    FollowPos.X += camFollow.RoundToX / 2;
                }

                if (camFollow.RoundToY != 0)
                {
                    FollowPos.Y -= FollowPos.Y % camFollow.RoundToY;
                    FollowPos.Y += camFollow.RoundToY / 2;
                }

                if (!camFollow.FollowX)
                {
                    FollowPos.X = App.renderTargets[camFollow.RenderTargetID].camera.Position.X + (App.renderTargets[camFollow.RenderTargetID].Resolution.X / 2);
                }

                if (!camFollow.FollowY)
                {
                    FollowPos.Y = App.renderTargets[camFollow.RenderTargetID].camera.Position.Y + (App.renderTargets[camFollow.RenderTargetID].Resolution.Y / 2);
                }

                App.renderTargets[camFollow.RenderTargetID].Center(FollowPos);
            }
        }

        public override void Reset()
        {
            Set = App.world.GetEntities().With<Body>().With<CameraFollow>().AsSet();
        }
    }
}
