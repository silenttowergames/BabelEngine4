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
using DefaultEcs.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4
{
    // TODO: SFX & Music
    // TODO: Volume
    // TODO: Multiple shaders per RenderTarget that redraw to the RT
    // TODO: Save states that don't save to file
    // TODO: Collisions
    // TODO: A* pathfinding with tilemap
    // TODO: Stress test
    
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

        static ISerializer serializer = new TextSerializer();

        static string saveState = null;

        public static IBabelSystem[] systems;

        // Local vars

        string
            Title,
            Version
        ;

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

            if (input.keyboard.Held(Microsoft.Xna.Framework.Input.Keys.P))
            {
                assets.sfx("GB_Loop_04").Play(Assets.Audio.SFX.SFXCondition.New);
            }

            if (input.keyboard.Pressed(Microsoft.Xna.Framework.Input.Keys.S))
            {
                StateSave();
            }

            if (input.keyboard.Pressed(Microsoft.Xna.Framework.Input.Keys.L))
            {
                world.Dispose();

                using (Stream stream = File.OpenRead("savestate"))
                {
                    world = serializer.Deserialize(stream);
                }

                GC.Collect();
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

                for (int i = 0; i < systems.Length; i++)
                {
                    systems[i].OnLoad();
                }

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

        public static void StateSave()
        {
            //*
            using (Stream stream = File.Create("savestate"))
            {
                serializer.Serialize(stream, world);
            }
            //*/

            /*
            using (Stream stream = new MemoryStream())
            {
                serializer.Serialize(stream, world);

                StreamReader reader = new StreamReader(stream);
            }
            //*/

            GC.Collect();
        }

        public static void StateLoad()
        {
            //
        }

        public static T GetSystem<T>() where T : class, IBabelSystem
        {
            foreach (IBabelSystem system in systems)
            {
                if (system is T)
                {
                    return (T)system;
                }
            }

            return null;
        }
    }
}
