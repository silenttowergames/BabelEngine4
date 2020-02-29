using BabelEngine4.Assets.Tiled;
using BabelEngine4.ECS.Components;
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
        public void Create(float LayerDepth, int LayerID, float Parallax, List<TiledProperty> properties = null)
        {
            Entity e = App.world.CreateEntity();
            e.Set(new Sprite("8x8", "Protag") { Effect = SpriteEffects.None, LayerDepth = LayerDepth, LayerID = LayerID, Parallax = Parallax });
            e.Set(new CameraFollow() { RenderTargetID = 0 });
            //e.Set(new Text("upscaled\ntext") { color = new Color(40, 30, 255), font = App.assets.font("PressStart2P"), LayerDepth = LayerDepth, LayerID = LayerID, Origin = new Vector2(8), Scale = new Vector2(2f) });
            e.Set(new Director());
            e.Set(new Body());
        }
    }
}
