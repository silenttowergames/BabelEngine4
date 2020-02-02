using BabelEngine4.Assets;
using BabelEngine4.Assets.Aseprite;
using BabelEngine4.Assets.Json;
using BabelEngine4.Assets.Sprites;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.AI;
using BabelEngine4.ECS.Systems;
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

        Entity e;
        
        string
            Title,
            Version
        ;

        IBabelSystem[] systems;
        DrawSystem drawSystem = new DrawSystem();

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

            // ECS
            world = new World();
            e = world.CreateEntity();
            e.Set(new Sprite(assets.sprite("8x8"), "Protag") { Effect = SpriteEffects.None, LayerDepth = 1 });
            e.Set(new Director());
            e.Set(new Body());

            Entity f = world.CreateEntity();
            f.Set(new Sprite(assets.sprite("8x8"), "Coin Light") { Effect = SpriteEffects.None, LayerDepth = 0.5f });
            f.Set(new Body() { Position = new Vector2(4) });

            systems = new IBabelSystem[]
            {
                new ControlSystem(),
                new DirectorSystem(),
                new MoveSystem(),
                new AnimationSystem(),
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

            assets.Load();

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
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
