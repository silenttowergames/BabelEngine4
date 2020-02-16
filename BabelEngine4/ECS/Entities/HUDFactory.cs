using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.Rendering;
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
    public class HUDFactory : IEntityFactory
    {
        public void Create()
        {
            Entity f = App.world.CreateEntity();
            f.Set(new Sprite(App.assets.sprite("8x8"), "Coin Light") { Effect = SpriteEffects.None, LayerDepth = 0.5f, RenderTargetID = 1 });
            f.Set(new Text("Coin") { font = App.assets.font("PressStart2P"), LayerDepth = 0.5f, Origin = new Vector2(-12, 0), color = Color.White, RenderTargetID = 1 });
            f.Set(new Body() { Position = new Vector2(4) });
        }
    }
}
