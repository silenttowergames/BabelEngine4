using BabelEngine4.Assets.Tiled;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.Rendering;

namespace BabelEngine4.ECS.Entities.LogoScene
{
    public class LogoFactory : IEntityFactory
    {
        public Entity Create(float LayerDepth, int LayerID, float Parallax, Vector2 Position = default, TiledObject obj = null)
        {
            Entity e = App.world.CreateEntity();

            e.Set(new Body() { Position = Position });
            e.Set(new Sprite("logo", "logo") { LayerDepth = LayerDepth, LayerID = LayerID, Scale = new Vector2(16), RenderTargetID = 2, });

            return e;
        }
    }
}
