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
using BabelEngine4.ECS.Components.LogoScene;

namespace BabelEngine4.ECS.Entities.LogoScene
{
    public class LogoSceneCompanyNameFactory : IEntityFactory
    {
        public Entity Create(float LayerDepth, int LayerID, float Parallax, Vector2 Position = default, TiledObject obj = null)
        {
            Entity e = App.world.CreateEntity();

            e.Set(new Body() { Position = Position });
            e.Set(new Text(null) { color = Color.White, spriteFont = "PressStart2P", RenderTargetID = 2, LayerDepth = 0.5f, LayerID = LayerID, Scale = new Vector2(2), });
            e.Set(new Sprite("logo", "blank") { LayerDepth = 0f, LayerID = LayerID, RenderTargetID = 2, color = Color.Black, });
            e.Set(new LogoSceneCompanyName());

            return e;
        }
    }
}
