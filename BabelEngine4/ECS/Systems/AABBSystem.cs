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

        Rectangle BoundsToCells(RectangleF bounds, int CellSizeX = -1, int CellSizeY = -1)
        {
            if (CellSizeX == -1)
            {
                CellSizeX = SpatialHashSize;
            }

            if (CellSizeY == -1)
            {
                CellSizeY = SpatialHashSize;
            }

            return new Rectangle(
                (int)Math.Floor(bounds.Position.X / CellSizeX),
                (int)Math.Floor(bounds.Position.Y / CellSizeY),
                (int)Math.Ceiling((bounds.Position.X + bounds.Size.X) / CellSizeX),
                (int)Math.Ceiling((bounds.Position.Y + bounds.Size.Y) / CellSizeY) + 1
            );
        }

        public override void Update()
        {
            BroadPhase();

            // Get the collidable map layer
            // Currently only supports one
            Span<TileMap> layers = App.world.Get<TileMap>();
            ref TileMap layer = ref TileMap.emptyMap;
            //for (int i = 0; i < layers.Length; i++)
            foreach(ref TileMap _layer in layers)
            {
                if (_layer.Solid)
                {
                    layer = ref _layer;

                    break;
                }
            }

            for (int i = 0; i < AllEntities.Count; i++)
            {
                if (!AllEntities[i].IsAlive)
                {
                    AllEntities[i] = default;

                    continue;
                }

                if (AllEntities[i] == default)
                {
                    continue;
                }

                NarrowPhase(AllEntities[i], ref layer);
            }
        }

        void BroadPhase()
        {
            ReadOnlySpan<Entity> EntitySpan = EntitySet.GetEntities();
            RectangleF bounds;
            Rectangle iBounds;
            long Hash;

            // TODO: AABBSystem: AllEntities reset: one cycle instead?
            // Have an index var that counts up with the entity span
            // If that index exceeds the list's bounds, do Add. Otherwise, replace
            // After the foreach, cycle through any remaining indexes & reset them
            ResetEntityList(AllEntities);

            foreach (Entity entity in EntitySpan)
            {
                ref AABB eAABB = ref entity.Get<AABB>();
                ref Body eBody = ref entity.Get<Body>();

                eBody.InitialVelocity = eBody.Velocity;
                eBody.EffectiveVelocity = eBody.Velocity;

                if (eBody.Velocity != default)
                {
                    AddEntityToPooledList(AllEntities, entity);
                }
                else
                {
                    for (int h = 0; h < eAABB.Hitboxes.Length; h++)
                    {
                        eAABB.Hitboxes[h].HitRight = eAABB.Hitboxes[h].HitLeft = eAABB.Hitboxes[h].HitBottom = eAABB.Hitboxes[h].HitTop = false;
                    }
                }

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

                    for (int X = iBounds.X; X < iBounds.Width; X++)
                    {
                        for (int Y = iBounds.Y; Y < iBounds.Height; Y++)
                        {
                            Hash = GetHashedKey(X, Y);

                            Cells.Add(Hash, entity);

                            AddLongToPooledList(eAABB.Cells, Hash);
                        }
                    }
                }
            }
        }

        void NarrowPhase(Entity entity, ref TileMap map)
        {
            if (entity == default)
            {
                return;
            }

            // The entity you're colliding with
            Entity subentity;

            // Allocation for storing main entity's bounds
            // Initialize this on the stack here & use it a bunch later
            RectangleF eBounds;

            // Tileset hitbox & bounds
            // A bool that stores whether or not there's a valid map
            bool HasMap = map.LayerName != TileMap.emptyMap.LayerName;
            // Store the map's tilesize as a Vector2 here for repeated use
            Vector2 TileSize = new Vector2(map.SizeEst.X, map.SizeEst.Y);
            // Allocate the bounds for a tile hitbox
            RectangleF tileBounds;
            // Allocate a tile hitbox
            Hitbox tileHitbox;
            // Allocate an int rectangle for your hitbox's bounds
            Rectangle eMapBounds;

            // Main entity's AABB component
            // Contains hitboxes
            ref AABB eAABB = ref entity.Get<AABB>();
            // Main entity's Body component
            // Holds its position
            ref Body eBody = ref entity.Get<Body>();

            // Cycle through the two dimensions
            for (int Dimension = 0; Dimension < 2; Dimension++)
            {
                // Cycle through the hitboxes
                // Not creating lots of new stack allocations, or garbage
                for (int h = 0; h < eAABB.Hitboxes.Length; h++)
                {
                    if (eBody.Velocity == default)
                    {
                        break;
                    }

                    // Skip this hitbox if it's not solid
                    if (!eAABB.Hitboxes[h].Solid)
                    {
                        continue;
                    }

                    if (Dimension == 0)
                    {
                        eAABB.Hitboxes[h].HitRight = eAABB.Hitboxes[h].HitLeft = eAABB.Hitboxes[h].HitBottom = eAABB.Hitboxes[h].HitTop = false;
                    }

                    // Fill eBounds with the current hitbox's bounds
                    // GetRealBounds adds the Body's position to the Hitbox's size & position
                    // 0,0 for the hitbox is when the center of the hitbox is on the entity's Position, not its top-left
                    // GetRealBounds also takes care of placing it properly like that
                    eBounds = eAABB.Hitboxes[h].GetRealBounds(eBody);

                    // Cycle through all of the cells the AABB component is in
                    for (int CellID = 0; CellID < eAABB.Cells.Count; CellID++)
                    {
                        if (eBody.Velocity == default)
                        {
                            break;
                        }

                        // If it isn't a valid cell, skip
                        if (eAABB.Cells[CellID] == long.MaxValue)
                        {
                            break;
                        }

                        // Collide with solid map layer, if there is one
                        //*
                        if (HasMap)
                        {
                            eMapBounds = BoundsToCells(eBounds, map.SizeEst.X, map.SizeEst.Y);

                            for(int X = eMapBounds.X - 1; X < eMapBounds.Width + 1; X++)
                            {
                                if (eBody.Velocity == default)
                                {
                                    break;
                                }

                                for (int Y = eMapBounds.Y - 1; Y < eMapBounds.Height + 1; Y++)
                                {
                                    if (eBody.Velocity == default)
                                    {
                                        break;
                                    }

                                    if (map[X, Y] == -1)
                                    {
                                        continue;
                                    }

                                    tileHitbox = new Hitbox()
                                    {
                                        Bounds = new RectangleF(TileSize / 2, TileSize)
                                    };

                                    tileBounds = tileHitbox.GetRealBounds(new Vector2(X, Y) * TileSize);

                                    EntityCollideWithEntity(
                                        ref eAABB.Hitboxes[h],
                                        ref eBody,
                                        ref eBounds,
                                        ref tileHitbox,
                                        ref tileBounds,
                                        Dimension
                                    );
                                }
                            }
                        }
                        //*/

                        // Cycle through the entities in that cell
                        for (int e = 0; e < Cells[eAABB.Cells[CellID]].Count; e++)
                        {
                            if (eBody.Velocity == default)
                            {
                                break;
                            }

                            subentity = Cells[eAABB.Cells[CellID]][e];

                            if (!subentity.IsAlive)
                            {
                                subentity = default;

                                continue;
                            }

                            if (subentity == default)
                            {
                                continue;
                            }

                            // If you've reached a default entity, then you've reached the end of the pool's valid entities
                            if (subentity == default)
                            {
                                break;
                            }

                            // Don't make an entity collide with itself!
                            // Waste of processing power & undesired behavior
                            if (entity == subentity)
                            {
                                continue;
                            }

                            ref AABB sAABB = ref subentity.Get<AABB>();
                            ref Body sBody = ref subentity.Get<Body>();

                            for (int sh = 0; sh < sAABB.Hitboxes.Length; sh++)
                            {
                                if (eBody.Velocity == default)
                                {
                                    break;
                                }

                                if (sAABB.Hitboxes[sh].CanPass)
                                {
                                    continue;
                                }

                                RectangleF subentityBounds = sAABB.Hitboxes[sh].GetRealBounds(sBody);

                                EntityCollideWithEntity(
                                    ref eAABB.Hitboxes[h],
                                    ref eBody,
                                    ref eBounds,
                                    ref sAABB.Hitboxes[sh],
                                    ref subentityBounds,
                                    Dimension
                                );
                            }
                        }
                    }
                }

                if (Dimension == 0)
                {
                    eBody.EffectiveVelocity.X = eBody.Velocity.X;
                    eBody.Position.X += eBody.Velocity.X;
                    eBody.Velocity.X = 0;
                }
                else if (Dimension == 1)
                {
                    eBody.EffectiveVelocity.Y = eBody.Velocity.Y;
                    eBody.Position.Y += eBody.Velocity.Y;
                    eBody.Velocity.Y = 0;
                }
            }
        }

        void EntityCollideWithEntity(ref Hitbox entityHitbox, ref Body entityBody, ref RectangleF entityBounds, ref Hitbox subentityHitbox, ref RectangleF subentityBounds, int D)
        {
            float MaxDistance;
            Vector2 ogVelocity = entityBody.Velocity;

            if (D == 1 && entityBounds.LineX.Intersects(subentityBounds.LineX, false))
            {
                if (entityHitbox.SolidRight && subentityHitbox.SolidTop && entityBody.Velocity.Y > 0 && subentityBounds.Top >= entityBounds.Bottom)
                {
                    MaxDistance = entityBounds.Bottom + entityBody.Velocity.Y;
                    MaxDistance = Math.Min(MaxDistance, subentityBounds.Top);
                    entityBody.Velocity.Y = MaxDistance - entityBounds.Bottom;

                    if (Math.Abs(MaxDistance - subentityBounds.Top) < 0.000001f)
                    {
                        entityHitbox.HitBottom = true;
                    }
                } // moving down
                else if (entityHitbox.SolidTop && subentityHitbox.SolidBottom && entityBody.Velocity.Y < 0 && subentityBounds.Bottom <= entityBounds.Top)
                {
                    MaxDistance = entityBounds.Top + entityBody.Velocity.Y;
                    MaxDistance = Math.Max(MaxDistance, subentityBounds.Bottom);
                    entityBody.Velocity.Y = MaxDistance - entityBounds.Top;

                    if (Math.Abs(MaxDistance - subentityBounds.Bottom) < 0.000001f)
                    {
                        entityHitbox.HitTop = true;
                    }
                } // moving up
            } // if entity.LineX

            if (D == 0 && entityBounds.LineY.Intersects(subentityBounds.LineY, false))
            {
                if (entityHitbox.SolidRight && subentityHitbox.SolidLeft && entityBody.Velocity.X > 0 && subentityBounds.Left >= entityBounds.Right)
                {
                    MaxDistance = entityBounds.Right + entityBody.Velocity.X;
                    MaxDistance = Math.Min(MaxDistance, subentityBounds.Left);
                    entityBody.Velocity.X = MaxDistance - entityBounds.Right;

                    if (Math.Abs(MaxDistance - subentityBounds.Left) < 0.000001f)
                    {
                        entityHitbox.HitRight = true;
                    }
                } // moving right
                else if (entityHitbox.SolidLeft && subentityHitbox.SolidRight && entityBody.Velocity.X < 0 && subentityBounds.Right <= entityBounds.Left)
                {
                    MaxDistance = entityBounds.Left + entityBody.Velocity.X;
                    MaxDistance = Math.Max(MaxDistance, subentityBounds.Right);
                    entityBody.Velocity.X = MaxDistance - entityBounds.Left;

                    if (Math.Abs(MaxDistance - subentityBounds.Right) < 0.000001f)
                    {
                        entityHitbox.HitLeft = true;
                    }
                } // moving left
            } // if entity.LineY
        }
    }
}
