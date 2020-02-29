using BabelEngine4.Assets;
using BabelEngine4.Assets.Aseprite;
using BabelEngine4.Assets.Json;
using BabelEngine4.Assets.Sprites;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.AI;
using BabelEngine4.ECS.Entities;
using BabelEngine4.ECS.Systems;
using BabelEngine4.Input;
using BabelEngine4.Rendering;
using BabelEngine4.Saving;
using BabelEngine4.Scenes;
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
    // TODO: Collisions
    // TODO: SFX & Music
    // TODO: Saving; save states; config
    // TODO: Stress test
    // TODO: A* pathfinding with tilemap
    // TODO: Menus finalized (only usable when active; only one active at a time)
    
    public class App : Game
    {
        // A few foolish globals
        public static AssetManager assets;

        public static Dictionary<string, IEntityFactory> Factories = new Dictionary<string, IEntityFactory>();

        public static Dictionary<string, IScene> Scenes = new Dictionary<string, IScene>();

        public static InputManager input;

        public static Renderer renderer;

        public static WindowManager windowManager;

        public static World world;

        public static RenderTarget[] renderTargets;

        public static int LayerIDCounter = 0;

        public static bool DoExit = false;

        public static IScene Scene = null;

        public static Config config = null;

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

            config = Storage.Load<Config>("config.dat");
        }

        protected override void Initialize()
        {
            Window.Title = $"{Title} {Version}";

            config.OnLoad();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            renderer.spriteBatch = new SpriteBatch(GraphicsDevice);

            assets.Load();

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            config.Save();
        }

        protected override void Update(GameTime gameTime)
        {
            if (DoExit)
            {
                Exit();
            }

            if (input.keyboard.Down(Microsoft.Xna.Framework.Input.Keys.LeftControl) && input.keyboard.Pressed(Microsoft.Xna.Framework.Input.Keys.F))
            {
                windowManager.Fullscreen = !windowManager.Fullscreen;
            }

            if (input.keyboard.Down(Microsoft.Xna.Framework.Input.Keys.A))
            {
                App.windowManager.WindowSize = new Point(640, 480);
            }

            if (input.keyboard.Down(Microsoft.Xna.Framework.Input.Keys.S))
            {
                config.Save();
            }

            if (Scene != null)
            {
                world?.Dispose();

                world = new World();

                drawSystem.Reset();

                for (int i = 0; i < systems.Length; i++)
                {
                    systems[i].Reset();
                }

                Scene.Load();

                Scene = null;

                GC.Collect();
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
