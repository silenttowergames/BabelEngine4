using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.AABB;
using BabelEngine4.ECS.Components.Rendering;
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
    public class AABBSystemOld : SystemSkeleton
    {

        EntitySet EntitiesSet = null;

        Dictionary<long, List<Entity>> Spaces;

        const int SpatialHashSize = 1024;

        public override void Reset()
        {
            EntitiesSet = App.world.GetEntities().With<AABB>().With<Body>().AsSet();
            // Reset this each time you switch scenes
            Spaces = new Dictionary<long, List<Entity>>();
        }

        // Thanks, prime31!
        long GetHashedKey(int x, int y) => unchecked((long)x << 32 | (uint)y);

        long InsertEntity(Entity e, int x, int y)
        {
            // Turn X,Y into a single hash value
            long Hash = GetHashedKey(x, y);

            // If this space hasn't been used yet, we need to initialize it
            // Pool some entities
            if (!Spaces.ContainsKey(Hash))
            {
                Spaces.Add(Hash, CreatePooledList<Entity>());
            }

            AddEntityToPooledList(Spaces[Hash], e);

            return Hash;
        }

        void AddEntityToPooledList(List<Entity> List, Entity e)
        {
            // Look for an empty entity if one exists & replace it
            // This is pooling
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i] == default)
                {
                    List[i] = e;

                    return;
                }
            }

            // If there wasn't an empty entity, replace it
            List.Add(e);
        }

        void ResetEntitiesInDictionary()
        {
            foreach (List<Entity> List in Spaces.Values)
            {
                ResetList(List);
            }
        }

        void ResetList<T>(List<T> List) where T : struct
        {
            for (int i = 0; i < List.Count; i++)
            {
                List[i] = default;
            }
        }

        List<T> CreatePooledList<T>() where T : struct
        {
            return new List<T>()
            {
                /*
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                default,
                */
            };
        }

        Rectangle BoundsToCells(RectangleF bounds)
        {
            return new Rectangle(
                (int)Math.Floor(bounds.Position.X / SpatialHashSize),
                (int)Math.Floor(bounds.Position.Y / SpatialHashSize),
                (int)Math.Ceiling(bounds.Size.X / SpatialHashSize),
                (int)Math.Ceiling(bounds.Size.Y / SpatialHashSize)
            );
        }

        List<Entity> GetCollidingEntities(RectangleF bounds, Entity collider)
        {
            List<Entity> returnSet = new List<Entity>();

            Rectangle iBounds = BoundsToCells(bounds);

            for (int X = 0; X < iBounds.Width + 1; X++)
            {
                if (iBounds.X + X < 0)
                {
                    X -= (iBounds.X + X) + 1;

                    continue;
                }

                for (int Y = 0; Y < iBounds.Height + 1; Y++)
                {
                    if (iBounds.Y + Y < 0)
                    {
                        Y -= (iBounds.Y + Y) + 1;

                        continue;
                    }

                    long hash = GetHashedKey(iBounds.X + X, iBounds.Y + Y);

                    if (!Spaces.ContainsKey(hash))
                    {
                        continue;
                    }

                    for (int i = 0; i < Spaces[hash].Count; i++)
                    {
                        if (Spaces[hash][i] == default)
                        {
                            continue;
                        }

                        ref AABB aabb = ref Spaces[hash][i].Get<AABB>();
                        ref Body b = ref Spaces[hash][i].Get<Body>();

                        for(int h = 0; h < aabb.Hitboxes.Length; h++)
                        {
                            if (true)//bounds.Intersects(aabb.Hitboxes[h].GetRealBounds(b, true)) && !returnSet.Contains(Spaces[hash][i]))
                            {
                                returnSet.Add(Spaces[hash][i]);
                            }
                        }
                    }
                }
            }

            return returnSet;
        }

        void InsertEntities()
        {
            Rectangle iBounds;

            foreach (ref readonly Entity e in EntitiesSet.GetEntities())
            {
                ref AABB pe = ref e.Get<AABB>();
                ref Body be = ref e.Get<Body>();

                if (pe.Cells != null)
                {
                    ResetList(pe.Cells);
                }
                else
                {
                    pe.Cells = CreatePooledList<long>();
                }

                foreach (Hitbox h in pe.Hitboxes)
                {
                    iBounds = BoundsToCells(h.GetRealBounds(be, true));

                    for (int X = 0; X < iBounds.Width + 1; X++)
                    {
                        if (false && iBounds.X + X < 0)
                        {
                            X -= (iBounds.X + X) + 1;

                            continue;
                        }

                        for (int Y = 0; Y < iBounds.Height + 1; Y++)
                        {
                            if (false && iBounds.Y + Y < 0)
                            {
                                Y -= (iBounds.Y + Y) + 1;

                                continue;
                            }

                            pe.Cells.Add(InsertEntity(e, iBounds.X + X, iBounds.Y + Y));
                        }
                    }
                }
            }
        }

        public override void Update()
        {
            ResetEntitiesInDictionary();
            InsertEntities();

            DoCollisions(EntitiesSet.GetEntities());
        }

        void DoCollisions(ReadOnlySpan<Entity> Entities)
        {
            RectangleF
                entityBounds,
                entityBoundsWithVelocity,
                subentityBounds
            ;

            foreach (Entity entity in Entities)
            {
                ref AABB entityAABB = ref entity.Get<AABB>();
                ref Body entityBody = ref entity.Get<Body>();

                for (int hid = 0; hid < entityAABB.Hitboxes.Length; hid++)
                {
                    entityBounds = entityAABB.Hitboxes[hid].GetRealBounds(entityBody);
                    entityBoundsWithVelocity = entityAABB.Hitboxes[hid].GetRealBounds(entityBody, true);

                    foreach (Entity subentity in GetCollidingEntities(entityBoundsWithVelocity, entity))
                    {
                        ref AABB subentityAABB = ref subentity.Get<AABB>();
                        ref Body subentityBody = ref subentity.Get<Body>();

                        for (int shid = 0; shid < subentityAABB.Hitboxes.Length; shid++)
                        {
                            subentityBounds = subentityAABB.Hitboxes[shid].GetRealBounds(subentityBody);

                            ResolveCollisions(entityBounds, entityAABB.Hitboxes[hid], ref entityBody, subentityBounds, subentityAABB.Hitboxes[shid]);
                        }
                    }
                }

                entityBody.Position += entityBody.Velocity;
                entityBody.Velocity = default;
            }
        }

        void ResolveCollisions(RectangleF entityBounds, Hitbox entityHitbox, ref Body entityBody, RectangleF subentityBounds, Hitbox subentityHitbox)
        {
            float MaxDistance;

            if (entityBounds.LineX.Intersects(subentityBounds.LineX, false))
            {
                if (entityHitbox.SolidRight && subentityHitbox.SolidTop && entityBody.Velocity.Y > 0 && subentityBounds.Top >= entityBounds.Bottom)
                {
                    MaxDistance = entityBounds.Bottom + entityBody.Velocity.Y;
                    MaxDistance = Math.Min(MaxDistance, subentityBounds.Top);
                    entityBody.Velocity.Y = MaxDistance - entityBounds.Bottom;
                } // moving down
                else if (entityHitbox.SolidTop && subentityHitbox.SolidBottom && entityBody.Velocity.Y < 0 && subentityBounds.Bottom <= entityBounds.Top)
                {
                    MaxDistance = entityBounds.Top + entityBody.Velocity.Y;
                    MaxDistance = Math.Max(MaxDistance, subentityBounds.Bottom);
                    entityBody.Velocity.Y = MaxDistance - entityBounds.Top;
                } // moving up
            } // if entity.LineX

            if (entityBounds.LineY.Intersects(subentityBounds.LineY, false))
            {
                if (entityHitbox.SolidRight && subentityHitbox.SolidLeft && entityBody.Velocity.X > 0 && subentityBounds.Left >= entityBounds.Right)
                {
                    MaxDistance = entityBounds.Right + entityBody.Velocity.X;
                    MaxDistance = Math.Min(MaxDistance, subentityBounds.Left);
                    entityBody.Velocity.X = MaxDistance - entityBounds.Right;
                } // moving right
                else if (entityHitbox.SolidLeft && subentityHitbox.SolidRight && entityBody.Velocity.X < 0 && subentityBounds.Right <= entityBounds.Left)
                {
                    MaxDistance = entityBounds.Left + entityBody.Velocity.X;
                    MaxDistance = Math.Max(MaxDistance, subentityBounds.Right);
                    entityBody.Velocity.X = MaxDistance - entityBounds.Left;
                } // moving left
            } // if entity.LineY
        }
    }
}
