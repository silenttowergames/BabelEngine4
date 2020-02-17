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

            // Allocating position for all things drawn
            Vector2 Position = new Vector2();

            // Draw maps
            Span<TileMap> maps = App.world.Get<TileMap>();
            foreach (ref readonly TileMap map in maps)
            {
                // Could definitely be more efficient :/
                if (map.RenderTargetID != renderTarget.ID)
                {
                    continue;
                }

                float LayerDepth = getLayerDepth(map.LayerDepth, map.LayerID);

                int X = 0, _X = 0, Y = 0, _Y = 0;
                for (_Y = -1; _Y <= (renderTarget.Resolution.Y / map.SizeEst.Y) + 1; _Y++)
                {
                    Y = _Y + (int)Math.Floor(renderTarget.camera.Position.Y / map.SizeEst.Y);

                    if (Y < 0)
                    {
                        _Y -= Y + 1;

                        continue;
                    }

                    if(Y >= map.Dimensions.Y)
                    {
                        break;
                    }

                    for (_X = -1; _X <= renderTarget.Resolution.X / map.SizeEst.X; _X++)
                    {
                        X = _X + (int)Math.Floor(renderTarget.camera.Position.X / map.SizeEst.X);

                        if (X < 0)
                        {
                            _X -= X + 1;

                            continue;
                        }

                        if (X >= map.Dimensions.X)
                        {
                            break;
                        }

                        int
                            FrameID = X + (Y * map.Dimensions.X),
                            Frame = map.Tiles[FrameID]
                        ;

                        if (Frame == -1)
                        {
                            continue;
                        }

                        Rectangle sourceRect = map.sheet.Meta.frames.Values.ElementAt(Frame).frame.ToRect();

                        Position.X = X * sourceRect.Width;
                        Position.Y = Y * sourceRect.Height;

                        map.sheet.Draw(
                            App.renderer.spriteBatch,
                            Position,
                            sourceRect,
                            Color.White,
                            0,
                            new Vector2(),
                            new Vector2(1),
                            SpriteEffects.None,
                            LayerDepth
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

                Position = body.Position + (renderTarget.camera.Position * (1 - sprite.Parallax));

                sprite.sheet.Draw(
                    App.renderer.spriteBatch,
                    Position,
                    sprite.AnimationID,
                    sprite.Frame,
                    sprite.color,
                    sprite.Rotation,
                    sprite.Origin,
                    sprite.Scale,
                    sprite.Effect,
                    getLayerDepth(sprite.LayerDepth, sprite.LayerID)
                );
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

                Position = body.Position + (renderTarget.camera.Position * (1 - text.Parallax));

                text.font.Draw(
                    App.renderer.spriteBatch,
                    text.Message,
                    Position,
                    text.color,
                    text.Rotation,
                    text.Origin,
                    text.Scale,
                    text.effect,
                    getLayerDepth(text.LayerDepth, text.LayerID)
                );
            }

            App.renderer.spriteBatch.End();
        }

        public static float getLayerDepth(float LayerDepth, float LayerID)
        {
            LayerID /= 100;
            LayerDepth /= 1000;

            return LayerID + LayerDepth;
        }
    }
}
