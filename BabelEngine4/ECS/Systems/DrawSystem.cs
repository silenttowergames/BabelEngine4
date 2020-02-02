using BabelEngine4.ECS.Components;
using DefaultEcs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class DrawSystem : IBabelSystem
    {
        public void Update()
        {
            ReadOnlySpan<Entity> Entities = App.world.GetEntities().With<Sprite>().With<Body>().AsSet().GetEntities();
            Sprite sprite;
            Body body;

            App.renderer.graphics.GraphicsDevice.Clear(Color.DarkCyan);

            App.renderer.spriteBatch.Begin(
                SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                SamplerState.PointClamp
            );

            foreach(ref readonly Entity entity in Entities)
            {
                sprite = entity.Get<Sprite>();
                body = entity.Get<Body>();

                sprite.sheet.Draw(
                    App.renderer.spriteBatch,
                    body.Position,
                    sprite.AnimationID,
                    sprite.Frame,
                    Color.White,
                    0,
                    new Vector2(),
                    new Vector2(1),
                    sprite.Effect,
                    sprite.LayerDepth
                );
            }

            App.renderer.spriteBatch.End();
        }
    }
}
