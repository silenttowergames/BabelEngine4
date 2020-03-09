using BabelEngine4.Assets.Tiled;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BabelEngine4.ECS.Components.Rendering;
using BabelEngine4.ECS.Components;

namespace BabelEngine4.ECS.Entities
{
    public class AABBDebugEntityFactory : IEntityFactory
    {
        public Entity Create(float LayerDepth, int LayerID, float Parallax, Vector2 Position = default, List<TiledProperty> properties = null)
        {
            Entity e = App.world.CreateEntity();

            e.Set(new Text() { color = new Color(40, 30, 255), spriteFont = "PressStart2P", LayerDepth = LayerDepth, LayerID = LayerID, Origin = new Vector2(8), Scale = new Vector2(1f) });
            e.Set(new Body());

            return e;
        }
    }
}
