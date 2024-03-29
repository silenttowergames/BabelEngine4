﻿using BabelEngine4.Assets;
using BabelEngine4.Assets.Aseprite;
using BabelEngine4.Assets.Audio;
using BabelEngine4.Assets.Json;
using BabelEngine4.Assets.Sprites;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Entities;
using BabelEngine4.ECS.Systems;
using BabelEngine4.Input;
using BabelEngine4.Rendering;
using BabelEngine4.Saving;
using BabelEngine4.Scenes;
using DefaultEcs;
using DefaultEcs.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BabelEngine4
{
    // TODO: A* pathfinding with tilemap
    // TODO: Stress test
    // TODO: Clean up test componentes/entities/systems/scenes
    // TODO: Save states that don't save to file
    
    public class App : Game
    {
        // A few foolish globals

        public static AssetManager assets;

        public static Dictionary<string, IEntityFactory> Factories = new Dictionary<string, IEntityFactory>();

        public static Dictionary<string, IScene> Scenes = new Dictionary<string, IScene>();

        static List<Entity> RemovedEntities = new List<Entity>();

        public static InputManager input;

        public static Renderer renderer;

        public static WindowManager windowManager;

        public static World world;

        public static RenderTarget[] renderTargets;

        public static bool
            DoExit = false,
            SaveConfig = true
        ;

        public static IScene Scene = null;

        public static Config config = null;

        static ISerializer serializer = new BinarySerializer();

        static byte[] saveState = null;

        public static IBabelSystem[] systems;

        public static DrawSystem drawSystem = new DrawSystem();

        public static string
            Title,
            Version
        ;

        public Action DefaultConfig = null;

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

            config = Storage.Load<Config>("config.dat", DefaultConfig);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            renderer.spriteBatch = new SpriteBatch(GraphicsDevice);

            assets.Load();

            config.OnLoad();

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            if (SaveConfig)
            {
                config.Save();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (DoExit)
            {
                Exit();
            }

            if (!IsActive)
            {
                assets.Update(true);

                return;
            }

            if (input.keyboard.Down(Microsoft.Xna.Framework.Input.Keys.LeftControl) && input.keyboard.Pressed(Microsoft.Xna.Framework.Input.Keys.F))
            {
                windowManager.Fullscreen = !windowManager.Fullscreen;
            }

            if (input.keyboard.Pressed(Microsoft.Xna.Framework.Input.Keys.S))
            {
                StateSave();
            }

            if (input.keyboard.Pressed(Microsoft.Xna.Framework.Input.Keys.L))
            {
                StateLoad();
            }

            if (Scene != null)
            {
                if (world != null)
                {
                    drawSystem.OnUnload();

                    for (int i = 0; i < systems.Length; i++)
                    {
                        systems[i].OnUnload();
                    }
                }

                world?.Dispose();

                world = new World();

                drawSystem.Reset();

                for (int i = 0; i < systems.Length; i++)
                {
                    systems[i].Reset();
                }

                Scene.Load();

                Scene = null;

                drawSystem.OnLoad();

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

            assets.Update();

            base.Update(gameTime);

            if (RemovedEntities.Count > 0)
            {
                for (int i = 0; i < RemovedEntities.Count; i++)
                {
                    RemovedEntities[i].Dispose();

                    RemovedEntities[i] = default;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            drawSystem.Update();

            base.Draw(gameTime);
        }

        public static void RemoveEntity(Entity e)
        {
            if (RemovedEntities.Contains(e))
            {
                return;
            }

            for (int i = 0; i < RemovedEntities.Count; i++)
            {
                if (RemovedEntities[i] == default)
                {
                    RemovedEntities[i] = e;

                    return;
                }
            }

            RemovedEntities.Add(e);
        }

        public static void StateSave()
        {
            using (Stream stream = File.Create("savestate"))
            {
                serializer.Serialize(stream, world);
            }

            saveState = File.ReadAllBytes("savestate");

            if (File.Exists("savestate"))
            {
                File.Delete("savestate");
            }

            GC.Collect();
        }

        public static void StateLoad()
        {
            if (saveState == null)
            {
                return;
            }

            drawSystem.OnUnload();

            for (int i = 0; i < systems.Length; i++)
            {
                systems[i].OnUnload();
            }

            world.Dispose();

            using (Stream stream = new MemoryStream(saveState))
            {
                world = serializer.Deserialize(stream);
            }

            GC.Collect();
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
