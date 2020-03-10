﻿using BabelEngine4.ECS.Components;
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
    public class AABBSystem : SystemSkeleton
    {
        class HashTable
        {
            Dictionary<long, List<Entity>> Cells;

            public void Reset()
            {
                Cells = new Dictionary<long, List<Entity>>();
            }

            public void Add(long Hash, Entity entity)
            {
                AddHash(Hash);

                if (Cells[Hash].Contains(entity))
                {
                    return;
                }

                for (int i = 0; i < Cells[Hash].Count; i++)
                {
                    if (Cells[Hash][i] == default)
                    {
                        Cells[Hash][i] = entity;

                        return;
                    }
                }

                Cells[Hash].Add(entity);
            }

            void AddHash(long Hash)
            {
                if (Cells.ContainsKey(Hash))
                {
                    return;
                }

                Cells.Add(Hash, NewPooledEntityList());
            }

            public List<Entity> this[long Hash]
            {
                get
                {
                    return Cells[Hash];
                }
            }

            List<Entity> NewPooledEntityList()
            {
                return new List<Entity>()
                {
                    default,
                    default,
                    default,
                    default,
                };
            }
        }

        EntitySet EntitySet;

        List<Entity> AllEntities;

        HashTable Cells = new HashTable();

        const int SpatialHashSize = 64;

        long GetHashedKey(int x, int y) => unchecked((long)x << 32 | (uint)y);

        public override void Reset()
        {
            EntitySet = App.world.GetEntities().With<Body>().With<AABB>().AsSet();
            AllEntities = new List<Entity>();
            Cells.Reset();
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

        void AddLongToPooledList(List<long> List, long Hash)
        {
            // Look for an empty entity if one exists & replace it
            // This is pooling
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i] == long.MaxValue)
                {
                    List[i] = Hash;

                    return;
                }
            }

            // If there wasn't an empty entity, replace it
            List.Add(Hash);
        }

        List<long> NewPooledCellList()
        {
            return new List<long>()
            {
                long.MaxValue,
                long.MaxValue,
                long.MaxValue,
                long.MaxValue,
            };
        }

        void ResetEntityList(List<Entity> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = default;
            }
        }

        void ResetCellList(List<long> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = long.MaxValue;
            }
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

        public override void Update()
        {
            BroadPhase();
            NarrowPhase();
        }

        void BroadPhase()
        {
            ReadOnlySpan<Entity> EntitySpan = EntitySet.GetEntities();
            RectangleF bounds;
            Rectangle iBounds;
            long Hash;

            ResetEntityList(AllEntities);

            foreach (Entity entity in EntitySpan)
            {
                AddEntityToPooledList(AllEntities, entity);

                ref AABB eAABB = ref entity.Get<AABB>();
                ref Body eBody = ref entity.Get<Body>();

                if (eAABB.Cells == null)
                {
                    eAABB.Cells = NewPooledCellList();
                }
                else
                {
                    ResetCellList(eAABB.Cells);
                }

                for (int h = 0; h < eAABB.Hitboxes.Length; h++)
                {
                    bounds = eAABB.Hitboxes[h].GetRealBounds(eBody, true);

                    iBounds = BoundsToCells(bounds);

                    for (int X = iBounds.X; X < iBounds.X + iBounds.Width; X++)
                    {
                        for (int Y = iBounds.Y; Y < iBounds.Y + iBounds.Height; Y++)
                        {
                            Hash = GetHashedKey(X, Y);

                            Cells.Add(Hash, entity);

                            AddLongToPooledList(eAABB.Cells, Hash);
                        }
                    }
                }
            }
        }

        void NarrowPhase()
        {
            for (int i = 0; i < AllEntities.Count; i++)
            {
                Entity entity = AllEntities[i];

                if (entity == default)
                {
                    break;
                }

                ref AABB eAABB = ref entity.Get<AABB>();
                ref Body eBody = ref entity.Get<Body>();

                for (int h = 0; h < eAABB.Hitboxes.Length; h++)
                {
                    for (int s = 0; s < eAABB.Cells.Count; s++)
                    {
                        if (eAABB.Cells[s] == long.MaxValue)
                        {
                            break;
                        }

                        for (int e = 0; e < Cells[eAABB.Cells[s]].Count; e++)
                        {
                            Entity subentity = Cells[eAABB.Cells[s]][e];

                            if (subentity == default)
                            {
                                break;
                            }

                            if (entity == subentity)
                            {
                                continue;
                            }

                            ref AABB sAABB = ref subentity.Get<AABB>();
                            ref Body sBody = ref subentity.Get<Body>();

                            for (int sh = 0; sh < sAABB.Hitboxes.Length; sh++)
                            {
                                CollideWith(
                                    ref eAABB.Hitboxes[h],
                                    ref eBody,
                                    ref sAABB.Hitboxes[sh],
                                    ref sBody
                                );
                            }
                        }
                    }
                }

                eBody.Position += eBody.Velocity;
                eBody.Velocity = default;
            }
        }

        void CollideWith(ref Hitbox entityHitbox, ref Body entityBody, ref Hitbox subentityHitbox, ref Body subentityBody)
        {
            RectangleF
                entityBounds = entityHitbox.GetRealBounds(entityBody),
                subentityBounds = subentityHitbox.GetRealBounds(subentityBody)
            ;

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
