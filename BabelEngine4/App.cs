using BabelEngine4.Assets.Aseprite;
using BabelEngine4.Assets.Json;
using BabelEngine4.Rendering;
using DefaultEcs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4
{
    public class App : Game
    {
        public static int LayerIDCounter = 0;

        string
            Title,
            Version
        ;

        JsonContainer j;

        Renderer renderer;

        WindowManager windowManager;

        World world;

        public App(string _Title, string _Version, Point Resolution)
        {
            Title = _Title;
            Version = _Version;

            Content.RootDirectory = "Content";

            renderer = new Renderer()
            {
                graphics = new GraphicsDeviceManager(this),
                resolution = Resolution
            };

            windowManager = new WindowManager()
            {
                window = Window
            };
        }

        protected override void Initialize()
        {
            Window.Title = $"{Title} {Version}";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            renderer.spriteBatch = new SpriteBatch(GraphicsDevice);

            j = Content.Load<JsonContainer>("8x8.meta");

            AsepriteData d = JsonConvert.DeserializeObject<AsepriteData>(j.Data);
            Console.WriteLine(d.frames["8x8 1.aseprite"].frame.x);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
