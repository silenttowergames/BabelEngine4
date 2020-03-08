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

                foreach (Hitbox he in pe.Hitboxes)
                {
                    re = new RectangleF(be.Position + he.Bounds.Position - (he.Bounds.Size / 2), he.Bounds.Size);

                    for (int D = 0; D < 2; D++)
                    {
                        foreach (ref readonly Entity f in Entities)
                        {
                            if (e == f)
                            {
                                continue;
                            }

                            ref AABB pf = ref f.Get<AABB>();
                            ref Body bf = ref f.Get<Body>();

                            foreach (Hitbox hf in pf.Hitboxes)
                            {
                                rf = new RectangleF(bf.Position + hf.Bounds.Position - (hf.Bounds.Size / 2), hf.Bounds.Size);

                                if (D == 0)
                                {
                                    if (re.LineY.Intersects(rf.LineY, false))
                                    {
                                        if (be.Velocity.X > 0 && rf.Left >= re.Right)
                                        {
                                            MaxDistance = re.Right + be.Velocity.X;
                                            MaxDistance = Math.Min(MaxDistance, rf.Left);
                                            be.Velocity.X = MaxDistance - re.Right;
                                        } // moving right
                                        else if (be.Velocity.X < 0 && rf.Right <= re.Left)
                                        {
                                            MaxDistance = re.Left + be.Velocity.X;
                                            MaxDistance = Math.Max(MaxDistance, rf.Right);
                                            be.Velocity.X = MaxDistance - re.Left;
                                        } // moving left
                                    } // if re.LineY
                                }

                                if (D == 1)
                                {
                                    if (re.LineX.Intersects(rf.LineX, false))
                                    {
                                        if (be.Velocity.Y > 0 && rf.Top >= re.Bottom)
                                        {
                                            MaxDistance = re.Bottom + be.Velocity.Y;
                                            MaxDistance = Math.Min(MaxDistance, rf.Top);
                                            be.Velocity.Y = MaxDistance - re.Bottom;
                                        } // moving down
                                        else if (be.Velocity.Y < 0 && rf.Bottom <= re.Top)
                                        {
                                            MaxDistance = re.Top + be.Velocity.Y;
                                            MaxDistance = Math.Max(MaxDistance, rf.Bottom);
                                            be.Velocity.Y = MaxDistance - re.Top;
                                        } // moving up
                                    } // if re.LineX
                                }
                            } // foreach hf
                        } // foreach f

                        if (D == 0)
                        {
                            be.Position.X += be.Velocity.X;
                            re.X += be.Velocity.X;
                            be.Velocity.X = 0;
                        }

                        if (D == 1)
                        {
                            be.Position.Y += be.Velocity.Y;
                            be.Velocity.Y = 0;
                        }
                    } // Dimension; X or Y
                } // foreach he
            } // foreach e
        }
    }
}
