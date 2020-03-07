using BabelEngine4.Assets.Tiled;
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
        public Entity Create(float LayerDepth, int LayerID, float Parallax, List<TiledProperty> properties = null)
        {
            Entity f = App.world.CreateEntity();
            f.Set(new Sprite("8x8", "Coin Light") { Effect = SpriteEffects.None, LayerID = LayerID, LayerDepth = LayerDepth, RenderTargetID = 1, Parallax = Parallax });
            f.Set(new Text("Coin") { spriteFont = "PressStart2P", LayerDepth = LayerDepth, LayerID = LayerID, Origin = new Vector2(-8, 4), color = Color.White, RenderTargetID = 1, Parallax = Parallax });
            f.Set(new Body() { Position = new Vector2(8) });

            return f;
        }
    }
}
