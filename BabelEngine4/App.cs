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
    // TODO: Spawner dictionary
    // TODO: Menus
    // TODO: SFX & Music
    // TODO: Layers
    // TODO: Tile maps
    // TODO: Tiled building
    // TODO: A* pathfinding with tilemap
    // TODO: Collisions
    // TODO: Saving; save states; config
    // TODO: Stress test

    public class App : Game
    {
        // A few foolish globals
        public static AssetManager assets;

        public static InputManager input;

        public static Renderer renderer;

        public static WindowManager windowManager;

        public static World world;

        public static RenderTarget[] renderTargets;

        public static int LayerIDCounter = 0;

        public static bool DoExit = false;

        // Local vars

        string
            Title,
            Version
        ;

        public IBabelSystem[] systems;
        DrawSystem drawSystem = new DrawSystem();

        public App(string _Title, string _Version, Point Resolution, Point Size)
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
                window = Window,
                WindowSize = Size
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
            if (DoExit)
            {
                Exit();
            }

            if(input.keyboard.Down(Microsoft.Xna.Framework.Input.Keys.LeftControl) && input.keyboard.Pressed(Microsoft.Xna.Framework.Input.Keys.F))
            {
                windowManager.Fullscreen = !windowManager.Fullscreen;
            }

            input.Update();
            
            for (int i = 0; i < systems.Length; i++)
            {
                systems[i].Update();
            }

            windowManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            drawSystem.Update();

            base.Draw(gameTime);
        }
    }
}
