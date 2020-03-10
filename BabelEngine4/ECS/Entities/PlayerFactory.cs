using BabelEngine4.Assets.Tiled;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.AABB;
using BabelEngine4.ECS.Components.AI;
using BabelEngine4.ECS.Components.Rendering;
using BabelEngine4.Misc;
using DefaultEcs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Entities
{
    public class PlayerFactory : IEntityFactory
    {
        public bool Alive = true;

        public Entity Create(float LayerDepth, int LayerID, float Parallax, Vector2 Position = default, List<TiledProperty> properties = null)
        {
            Entity e = App.world.CreateEntity();
            e.Set(new Sprite("8x8", "Protag") { Effect = SpriteEffects.None, LayerDepth = LayerDepth, LayerID = LayerID, Parallax = Parallax });
            e.Set(new AABB()
            {
                Hitboxes = new Hitbox[]
                {
                    new Hitbox() { Bounds = new RectangleF() { Width = 8, Height = 8 } },
                    //new Hitbox() { Bounds = new RectangleF() { Width = 8, Height = 8, X = -8, Y = -8, } },
                }
            });
            e.Set(new Director());
            if (Alive)
            {
                e.Set(new CameraFollow() { RenderTargetID = 0 });
                e.Set(new AIPlayer());
                //e.Set(new Text("upscaled\ntext") { color = new Color(40, 30, 255), spriteFont = "PressStart2P", LayerDepth = LayerDepth, LayerID = LayerID, Origin = new Vector2(8), Scale = new Vector2(1f) });
            }
            else
            {
                e.Set(new AIRandom());
            }
            e.Set(new Body() { Position = Position });

            return e;
        }
    }
}
