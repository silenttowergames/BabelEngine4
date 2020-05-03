using BabelEngine4.Assets.Shaders;
using BabelEngine4.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Rendering
{
    public class RenderTarget
    {
        public Camera camera = new Camera() { Zoom = 1, };

        public Color BGColor;

        public List<int> sprites;

        public RenderTarget2D
            renderTarget,
            renderTargetBackup,
            renderTargetOriginal
        ;

        public Shader[] shaders = null;

        public Point Position
        {
            get;
            protected set;
        }

        public Point Resolution
        {
            get;
            protected set;
        }

        public Point Size
        {
            get;
            protected set;
        }

        public int ID
        {
            get;
            protected set;
        }

        public RenderTarget(int _ID, Point _Resolution, Point _Position = new Point(), Point _Size = new Point())
        {
            camera.renderTarget = this;

            ID = _ID;

            Position = _Position;

            Resolution = _Resolution;

            if(_Size == Point.Zero)
            {
                _Size = Resolution;
            }

            Size = _Size;
        }

        public void Center(Vector2 FollowPos)
        {
            camera.Position = FollowPos - (new Vector2(Resolution.X, Resolution.Y) / 2);
        }

        RenderTarget2D CreateRenderTarget()
        {
            return new RenderTarget2D(App.renderer.graphics.GraphicsDevice, (int)(Resolution.X * App.windowManager.Zoom), (int)(Resolution.Y * App.windowManager.Zoom));
        }

        public void Reset()
        {
            renderTarget = CreateRenderTarget();
            renderTargetBackup = CreateRenderTarget();
            renderTargetOriginal = CreateRenderTarget();
        }

        public void Setup(SpriteBatch spriteBatch, bool Clear = true)
        {
            renderTarget.GraphicsDevice.SetRenderTarget(renderTarget);

            if (Clear)
            {
                renderTarget.GraphicsDevice.Clear(BGColor);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle? _rect = null)
        {
            Rectangle rect;

            if(_rect == null)
            {
                rect = new Rectangle(
                    (int)(Position.X * App.windowManager.Zoom),
                    (int)(Position.Y * App.windowManager.Zoom),
                    (int)(Size.X * App.windowManager.Zoom),
                    (int)(Size.Y * App.windowManager.Zoom)
                );
            }
            else
            {
                rect = (Rectangle)_rect;
            }

            spriteBatch.Draw(
                renderTarget,
                rect,
                Color.White
            );
        }
    }
}
