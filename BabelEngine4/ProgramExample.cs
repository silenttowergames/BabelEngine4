using BabelEngine4.ECS.Systems;
using BabelEngine4.Rendering;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4
{
    /*
    public static class Animations
    {
        public static string
            // Enemy animations
            EnemyIdle = "enemy-idle",
            EnemyWalk = "enemy-walk",

            // Player animations
            PlayerIdle = "player-idle",
            PlayerWalk = "player-walk"
        ;
    }

    public enum RenderTargets
    {
        Main,
        HUD,
    }

    public enum Actions
    {
        None,
        MoveRight,
        MoveLeft,
        MoveDown,
        MoveUp,
    }
    */

    /// <summary>
    /// The main class.
    /// </summary>
    public static class ProgramExample
    {
        //public static Array ActionsValues = Enum.GetValues(typeof(Actions));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Point
                resolution = new Point(320, 180),
                window = new Point(1280, 720)
            ;
            //int HUDHeight = 20;

            using (App app = new App("Game Name", "v1.0.0", resolution, window))
            {
                //App.windowManager.RoundZoom = false;

                App.assets.addMaps(
                    //new Map("testmap")
                );

                App.assets.addShaders(
                //new Shader("TestShader")
                );

                App.assets.addSprites(
                    //new SpriteSheet("16x16")
                );

                App.renderTargets = new RenderTarget[]
                {
                    //new RenderTarget((int)RenderTargets.Main, resolution - new Point(0, HUDHeight), new Point(0, HUDHeight)) { BGColor = Color.Black },
                    //new RenderTarget((int)RenderTargets.HUD, new Point(resolution.X, HUDHeight)) { BGColor = Color.Black },
                };

                App.systems = new IBabelSystem[]
                {
                    // TODO: Add ChangeSelected event
                    new MenuSystem(),
                    new MenuItemSelectGoToSceneSystem(),
                    // Your AI systems
                    new AABBSystem(),
                    // Your post-movement systems
                    new CameraFollowSystem(),
                    new AnimationSystem(),
                    new MusicBasicSystem(),
                };

                //App.Scenes.Add("test", new TiledScene("testmap"));

                //App.Factories.Add("player", new PlayerFactory());

#if DEBUG
                App.SaveConfig = false;
#endif

                //App.Scene = App.Scenes["test2"];

                app.Run();
            }
        }
    }
}
