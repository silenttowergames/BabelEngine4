using BabelEngine4.ECS.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Rendering
{
    public class WindowManager
    {
        bool
            Changed = true,
            fullscreen = false
        ;

        public bool Letterbox = true;

        public float Zoom
        {
            get;
            protected set;
        }

        public GameWindow window;

        public string Title
        {
            get
            {
                return window.Title;
            }

            set
            {
                window.Title = value;
            }
        }

        Point size;

        public Point WindowSize
        {
            get
            {
                return size;
            }

            set
            {
                if (size == value)
                {
                    return;
                }

                Changed = true;

                size = value;
            }
        }

        public Point Size;

        public bool Fullscreen
        {
            get
            {
                return fullscreen;
            }

            set
            {
                if (fullscreen == value)
                {
                    return;
                }

                Changed = true;

                fullscreen = value;
            }
        }

        public void Update()
        {
            if (Changed)
            {
                Changed = false;

                if (Fullscreen)
                {
                    Size = new Point(
                        App.renderer.graphics.GraphicsDevice.DisplayMode.Width,
                        App.renderer.graphics.GraphicsDevice.DisplayMode.Height
                    );
                }
                else
                {
                    Size = size;
                }

                App.renderer.graphics.PreferredBackBufferWidth = Size.X;
                App.renderer.graphics.PreferredBackBufferHeight = Size.Y;

                if (Fullscreen != App.renderer.graphics.IsFullScreen)
                {
                    App.renderer.graphics.ToggleFullScreen();
                }

                Vector2 _Zoom = new Vector2(Size.X / (float)App.renderer.resolution.X, Size.Y / (float)App.renderer.resolution.Y);
                Zoom = Math.Min(_Zoom.X, _Zoom.Y);

                if (Letterbox)
                {
                    Zoom = (float)Math.Floor(Zoom);
                }

                App.renderer.graphics.ApplyChanges();

                foreach (RenderTarget renderTarget in App.renderTargets)
                {
                    renderTarget.Reset();
                }

                if (DrawSystem.primaryRenderTarget == null)
                {
                    DrawSystem.primaryRenderTarget = new RenderTarget(-1, App.renderer.resolution);
                    DrawSystem.primaryRenderTarget.shaders = DrawSystem.shaders;
                }

                DrawSystem.primaryRenderTarget.Reset();
            }
        }
    }
}
