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
    public class AABBSystem : SystemSkeleton
    {

        EntitySet EntitiesSet = null;

        Dictionary<long, List<Entity>> Spaces;

        const int SpatialHashSize = 64;

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

        void InsertEntities()
        {
            RectangleF bounds;
            Rectangle iBounds;

            foreach (ref readonly Entity e in EntitiesSet.GetEntities())
            {
                ref AABB pe = ref e.Get<AABB>();
                ref Body be = ref e.Get<Body>();

                if (pe.Spaces != null)
                {
                    ResetList(pe.Spaces);
                }
                else
                {
                    pe.Spaces = CreatePooledList<long>();
                }

                foreach (Hitbox h in pe.Hitboxes)
                {
                    bounds = h.GetRealBounds(be, true);

                    iBounds = new Rectangle(
                        (int)Math.Floor(bounds.Position.X / SpatialHashSize),
                        (int)Math.Floor(bounds.Position.Y / SpatialHashSize),
                        (int)Math.Ceiling(bounds.Size.X / SpatialHashSize),
                        (int)Math.Ceiling(bounds.Size.Y / SpatialHashSize)
                    );

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

                            pe.Spaces.Add(InsertEntity(e, iBounds.X + X, iBounds.Y + Y));
                        }
                    }
                }
            }
        }

        public override void Update()
        {
            if (true)
            {
                ResetEntitiesInDictionary();
                InsertEntities();

                DoCollisions(EntitiesSet.GetEntities());
            }
            else
            {
                //DoCollisions(EntitiesSet.GetEntities().ToArray());
            }

            /*
            Span<Text> Ts = App.world.Get<Text>();
            foreach (ref Text t in Ts)
            {
                t.Message = "";

                foreach (long Key in Spaces.Keys)
                {
                    t.Message += (Key + ": " + Spaces[Key].Count) + "\n";
                }
            }
            */
        }

        void DoCollisions(ReadOnlySpan<Entity> Entities)
        {
            RectangleF
                re,
                rf
            ;
            float MaxDistance;

            foreach (Entity e in Entities)
            {
                if (e == default)
                {
                    continue;
                }

                ref AABB pe = ref e.Get<AABB>();
                ref Body be = ref e.Get<Body>();

                foreach (Hitbox he in pe.Hitboxes)
                {
                    re = he.GetRealBounds(be);

                    for (int D = 0; D < 2; D++)
                    {
                        //for (int s = 0; s < pe.Spaces.Count; s++)
                        //{
                            //foreach (Entity f in Spaces[pe.Spaces[s]])
                            foreach (Entity f in Entities)
                            {
                                if (f == default)
                                {
                                    break;
                                }

                                if (f == e)
                                {
                                    continue;
                                }

                                ref AABB pf = ref f.Get<AABB>();
                                ref Body bf = ref f.Get<Body>();

                                foreach (Hitbox hf in pf.Hitboxes)
                                {
                                    rf = hf.GetRealBounds(bf);

                                    if (D == 0)
                                    {
                                        if (re.LineY.Intersects(rf.LineY, false))
                                        {
                                            if (hf.SolidLeft && be.Velocity.X > 0 && rf.Left >= re.Right)
                                            {
                                                MaxDistance = re.Right + be.Velocity.X;
                                                MaxDistance = Math.Min(MaxDistance, rf.Left);
                                                be.Velocity.X = MaxDistance - re.Right;
                                            } // moving right
                                            else if (hf.SolidRight && be.Velocity.X < 0 && rf.Right <= re.Left)
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
                                            if (hf.SolidTop && be.Velocity.Y > 0 && rf.Top >= re.Bottom)
                                            {
                                                MaxDistance = re.Bottom + be.Velocity.Y;
                                                MaxDistance = Math.Min(MaxDistance, rf.Top);
                                                be.Velocity.Y = MaxDistance - re.Bottom;
                                            } // moving down
                                            else if (hf.SolidBottom && be.Velocity.Y < 0 && rf.Bottom <= re.Top)
                                            {
                                                MaxDistance = re.Top + be.Velocity.Y;
                                                MaxDistance = Math.Max(MaxDistance, rf.Bottom);
                                                be.Velocity.Y = MaxDistance - re.Top;
                                            } // moving up
                                        } // if re.LineX
                                    }
                                } // foreach hf
                            } // foreach f
                        //} // for s

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
