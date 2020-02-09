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
        public Color BGColor;

        public List<int> sprites;

        RenderTarget2D renderTarget;

        public Shader shader = null;

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
            ID = _ID;

            Position = _Position;

            Resolution = _Resolution;

            if(_Size == Point.Zero)
            {
                _Size = Resolution;
            }

            Size = _Size;
        }

        public void Reset()
        {
            renderTarget = new RenderTarget2D(App.renderer.graphics.GraphicsDevice, Resolution.X, Resolution.Y);
        }

        public void Setup(SpriteBatch spriteBatch)
        {
            renderTarget.GraphicsDevice.SetRenderTarget(renderTarget);

            renderTarget.GraphicsDevice.Clear(BGColor);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle? _rect = null)
        {
            Rectangle rect;

            if(_rect == null)
            {
                rect = new Rectangle(
                    Position.X,
                    Position.Y,
                    Size.X,
                    Size.Y
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
