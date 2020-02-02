using BabelEngine4.Assets;
using BabelEngine4.Assets.Aseprite;
using BabelEngine4.Assets.Json;
using BabelEngine4.Assets.Sprites;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.AI;
using BabelEngine4.ECS.Systems;
using BabelEngine4.Input;
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

        public static InputManager input;

        public static Renderer renderer;

        public static WindowManager windowManager;

        public static World world;

        public static int LayerIDCounter = 0;

        // Local vars

        Entity e;
        
        string
            Title,
            Version
        ;

        public IBabelSystem[] systems;
        DrawSystem drawSystem = new DrawSystem();

        public App(string _Title, string _Version, Point Resolution)
        {
            Title = _Title;
            Version = _Version;

            input = new InputManager();

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
            input.Update();

            for(int i = 0; i < systems.Length; i++)
            {
                systems[i].Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            drawSystem.Update();

            base.Draw(gameTime);
        }
    }
}
