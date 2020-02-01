using BabelEngine4.Assets;
using BabelEngine4.Assets.Aseprite;
using BabelEngine4.Assets.Json;
using BabelEngine4.Assets.Sprites;
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
        // A few foolish globals
        public static AssetManager assets;

        public static Renderer renderer;

        public static WindowManager windowManager;

        public static World world;

        public static int LayerIDCounter = 0;

        // Local vars

        string
            Title,
            Version
        ;

        public App(string _Title, string _Version, Point Resolution)
        {
            Title = _Title;
            Version = _Version;

            renderer = new Renderer()
            {
                graphics = new GraphicsDeviceManager(this),
                resolution = Resolution
            };

            windowManager = new WindowManager()
            {
                window = Window
            };

            assets = new AssetManager(Content);
            assets.addSprites(
                new SpriteSheet("8x8")
            );
        }

        protected override void Initialize()
        {
            Window.Title = $"{Title} {Version}";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            renderer.spriteBatch = new SpriteBatch(GraphicsDevice);

            assets.Load();

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            renderer.spriteBatch.Begin();

            renderer.spriteBatch.Draw(
                assets.sprite("8x8").Raw,
                new Rectangle(0, 0, 32, 32),
                Color.White
            );

            renderer.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
