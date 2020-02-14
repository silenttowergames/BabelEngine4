using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.Rendering;
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
        EntitySet
            EntitySetWithSprites = null,
            EntitySetWithText = null
        ;

        public static RenderTarget primaryRenderTarget = null;

        public void Update()
        {
            //*
            if (EntitySetWithSprites == null)
            {
                EntitySetWithSprites = App.world.GetEntities().With<Sprite>().With<Body>().AsSet();
            }

            if (EntitySetWithText == null)
            {
                EntitySetWithText = App.world.GetEntities().With<Text>().With<Body>().AsSet();
            }

            ReadOnlySpan<Entity> EntitiesWithSprites = EntitySetWithSprites.GetEntities();
            ReadOnlySpan<Entity> EntitiesWithText = EntitySetWithText.GetEntities();

            // Render the targets individually
            for (int i = 0; i < App.renderTargets.Length; i++)
            {
                UseRenderTarget(App.renderTargets[i], ref EntitiesWithSprites, ref EntitiesWithText);
            }

            // Draw the subtargets onto our primary target
            primaryRenderTarget.Setup(App.renderer.spriteBatch);

            App.renderer.spriteBatch.Begin(
                SpriteSortMode.FrontToBack,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp
            );

            for (int i = 0; i < App.renderTargets.Length; i++)
            {
                App.renderTargets[i].Draw(App.renderer.spriteBatch);
            }

            App.renderer.spriteBatch.End();

            // Render the primary target
            App.renderer.graphics.GraphicsDevice.SetRenderTarget(null);
            //*/

            //*
            App.renderer.graphics.GraphicsDevice.Clear(Color.Black);

            App.renderer.spriteBatch.Begin(
                SpriteSortMode.FrontToBack,
                BlendState.NonPremultiplied,
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
            //*/
        }

        void UseRenderTarget(RenderTarget renderTarget, ref ReadOnlySpan<Entity> EntitiesWithSprites, ref ReadOnlySpan<Entity> EntitiesWithText)
        {
            renderTarget.Setup(App.renderer.spriteBatch);

            App.renderer.spriteBatch.Begin(
                sortMode: SpriteSortMode.FrontToBack,
                blendState: BlendState.NonPremultiplied,
                samplerState: SamplerState.PointClamp,
                effect: renderTarget.shader?.Raw,
                transformMatrix: renderTarget.camera.matrix
            );

            renderTarget.shader?.Update?.Invoke(renderTarget.shader.Raw);

            //*
            // Draw maps
            Span<TileMap> maps = App.world.Get<TileMap>();
            foreach (ref readonly TileMap map in maps)
            {
                if (map.RenderTargetID != renderTarget.ID)
                {
                    continue;
                }

                Vector2 Position = new Vector2();

                for (int Y = 0; Y < map.Dimensions.Y; Y++)
                {
                    for (int X = 0; X < map.Dimensions.X; X++)
                    {
                        int
                            FrameID = X + (Y * map.Dimensions.X),
                            Frame = map.Tiles[FrameID]
                        ;

                        Rectangle sourceRect = map.sheet.Meta.frames.Values.ElementAt(Frame).frame.ToRect();

                        Position.X = X * sourceRect.Width;
                        Position.Y = Y * sourceRect.Height;

                        map.sheet.Draw(
                            App.renderer.spriteBatch,
                            Position,
                            //map.Tiles[]
                            sourceRect,
                            Color.White,
                            0,
                            new Vector2(),
                            new Vector2(1),
                            SpriteEffects.None,
                            map.LayerDepth
                        );
                    }
                }
            }

            // Draw sprites
            foreach (ref readonly Entity entity in EntitiesWithSprites)
            {
                ref Sprite sprite = ref entity.Get<Sprite>();

                // Could definitely be more efficient :/
                if (sprite.RenderTargetID != renderTarget.ID)
                {
                    continue;
                }

                ref Body body = ref entity.Get<Body>();

                //*
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
                //*/
            }

            // Draw text
            foreach (ref readonly Entity entity in EntitiesWithText)
            {
                ref Text text = ref entity.Get<Text>();

                // Could definitely be more efficient :/
                if (text.RenderTargetID != renderTarget.ID)
                {
                    continue;
                }

                ref Body body = ref entity.Get<Body>();

                //*
                text.font.Draw(
                    App.renderer.spriteBatch,
                    text.Message,
                    body.Position,
                    text.color,
                    text.Rotation,
                    text.Origin,
                    text.Scale,
                    text.effect,
                    text.LayerDepth
                );
                //*/
            }
            //*/

            App.renderer.spriteBatch.End();
        }
    }
}
