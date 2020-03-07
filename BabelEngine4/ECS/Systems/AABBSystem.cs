using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.AABB;
using BabelEngine4.Misc;
using DefaultEcs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class AABBSystem : SystemSkeleton
    {

        EntitySet EntitiesSet = null;

        public override void Reset()
        {
            EntitiesSet = App.world.GetEntities().With<AABB>().With<Body>().AsSet();
        }

        public override void Update()
        {
            ReadOnlySpan<Entity> Entities = EntitiesSet.GetEntities();
            RectangleF
                re,
                rf
            ;
            float MaxDistance = 0;

            foreach (ref readonly Entity e in Entities)
            {
                ref AABB pe = ref e.Get<AABB>();
                ref Body be = ref e.Get<Body>();

                re = new RectangleF(be.Position - new Vector2(4), new Vector2(8));

                foreach (ref readonly Entity f in Entities)
                {
                    if(e == f)
                    {
                        continue;
                    }

                    ref AABB pf = ref f.Get<AABB>();
                    ref Body bf = ref f.Get<Body>();

                    rf = new RectangleF(bf.Position - new Vector2(4), new Vector2(8));

                    if (re.LineY.Intersects(rf.LineY, false))
                    {
                        if (be.Velocity.X > 0 && rf.Left >= re.Right)
                        {
                            MaxDistance = re.Right + be.Velocity.X;
                            MaxDistance = Math.Min(MaxDistance, rf.Left);
                            be.Velocity.X = MaxDistance - re.Right;
                        }
                        else if(be.Velocity.X < 0 && rf.Right <= re.Left)
                        {
                            MaxDistance = re.Left + be.Velocity.X;
                            MaxDistance = Math.Max(MaxDistance, rf.Right);
                            be.Velocity.X = MaxDistance - re.Left;
                        }
                    }
                }
            }
        }
    }
}
