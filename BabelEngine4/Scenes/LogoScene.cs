using BabelEngine4.Assets.Audio;
using BabelEngine4.ECS.Entities.LogoScene;
using BabelEngine4.ECS.Systems.LogoScene;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Scenes
{
    public class LogoScene : IScene
    {
        public static void Initialize(string NextScene)
        {
            App.Factories.Add("logoscene-company-name", new LogoSceneCompanyNameFactory());
            App.Factories.Add("logoscene-blank", new LogoSceneBlankFactory());
            App.Factories.Add("logoscene-logo", new LogoFactory());
            App.Factories.Add("logoscene-console", new LogoSceneConsoleTextFactory());
            App.Factories.Add("logoscene-cursor", new LogoSceneConsoleCursorFactory());

            App.assets.addSFX(
                new SFX("console_loaded"),
                new SFX("key_enter"),
                new SFX("key_loud_1"),
                new SFX("key_loud_2"),
                new SFX("key_medium_1"),
                new SFX("key_medium_2"),
                new SFX("key_quiet_1"),
                new SFX("key_quiet_2")
            );

            App.Scenes.Add("logoscene", new LogoScene(NextScene));
        }

        public LogoScene(string _NextScene)
        {
            LogoSceneConsoleTextSystem.NextScene = _NextScene;
        }

        public void Load()
        {
            App.Factories["logoscene-company-name"].Create(1, 3, 1, new Vector2((App.renderTargets[2].Resolution.X / 2) - 16 * (9), ((float)(App.renderTargets[2].Resolution.Y) * 1.5f) + 12));
            App.Factories["logoscene-console"].Create(0, 0, 1, new Vector2(4));
            App.Factories["logoscene-cursor"].Create(0, 0, 1);
            App.Factories["logoscene-logo"].Create(0, 2, 1, new Vector2(App.renderTargets[2].Resolution.X / 2, (App.renderTargets[2].Resolution.Y) + (128 * 1.5f)));
            App.Factories["logoscene-blank"].Create(0, 1, 1, new Vector2(App.renderTargets[2].Resolution.X / 2, ((float)(App.renderTargets[2].Resolution.Y) * 1.5f) + 8));

            LogoSceneConsoleTextSystem.IsActive = true;
        }
    }
}
