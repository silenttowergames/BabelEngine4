using BabelEngine4.ECS.Components;
using BabelEngine4.Rendering;
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
        RenderTarget primaryRenderTarget = null;

        public void Update()
        {
            ReadOnlySpan<Entity> Entities = App.world.GetEntities().With<Sprite>().With<Body>().AsSet().GetEntities();

            if (primaryRenderTarget == null)
            {
                primaryRenderTarget = new RenderTarget(-1, App.renderer.resolution);
                primaryRenderTarget.Reset();
            }

            // Render the targets individually
            for (int i = 0; i < App.renderTargets.Length; i++)
            {
                UseRenderTarget(App.renderTargets[i], ref Entities);
            }

            // Draw the subtargets onto our primary target
            primaryRenderTarget.Setup(App.renderer.spriteBatch);

            App.renderer.spriteBatch.Begin(
                SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                SamplerState.PointClamp
            );

            for (int i = 0; i < App.renderTargets.Length; i++)
            {
                App.renderTargets[i].Draw(App.renderer.spriteBatch);
            }

            App.renderer.spriteBatch.End();

            // Render the primary target
            App.renderer.graphics.GraphicsDevice.SetRenderTarget(null);

            App.renderer.graphics.GraphicsDevice.Clear(Color.Black);

            App.renderer.spriteBatch.Begin(
                SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                SamplerState.PointClamp
            );

            Rectangle primaryTargetRect = new Rectangle(
                0,
                0,
                (int)(primaryRenderTarget.Size.X * App.windowManager.Zoom),
                (int)(primaryRenderTarget.Size.Y * App.windowManager.Zoom)
            );
            primaryTargetRect.X = (App.windowManager.Size.X - primaryTargetRect.Width) / 2;
            primaryTargetRect.Y = (App.windowManager.Size.Y - primaryTargetRect.Height) / 2;

            primaryRenderTarget.Draw(
                App.renderer.spriteBatch,
                primaryTargetRect
            );

            App.renderer.spriteBatch.End();
        }

        void UseRenderTarget(RenderTarget renderTarget, ref ReadOnlySpan<Entity> Entities)
        {
            Sprite sprite;
            Body body;

            renderTarget.Setup(App.renderer.spriteBatch);

            App.renderer.spriteBatch.Begin(
                SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                SamplerState.PointClamp
            );

            foreach (ref readonly Entity entity in Entities)
            {
                sprite = entity.Get<Sprite>();

                // Could definitely be more efficient :/
                if(sprite.RenderTargetID != renderTarget.ID)
                {
                    continue;
                }

                body = entity.Get<Body>();

                sprite.sheet.Draw(
                    App.renderer.spriteBatch,
                    body.Position,
                    sprite.AnimationID,
                    sprite.Frame,
                    sprite.color,
                    sprite.Rotation,
                    sprite.Origin,
                    sprite.Scale,
                    sprite.Effect,
                    sprite.LayerDepth
                );
            }

            App.renderer.spriteBatch.End();
        }
    }
}
