using BabelEngine4.Assets.Audio;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.LogoScene;
using BabelEngine4.ECS.Components.Rendering;
using BabelEngine4.Misc;
using DefaultEcs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BabelEngine4.Assets.Audio.SFX;

namespace BabelEngine4.ECS.Systems.LogoScene
{
    public class LogoSceneConsoleTextSystem : SystemSkeleton
    {
        static string
            CompanyName = "Silent Tower Games",
            GoalString = null
        ;

        public static string NextScene;

        public static bool IsActive = false;

        EntitySet
            commands,
            companyNames,
            cursors
        ;

        int
            lengthConsole = 2,
            lengthCompanyName = 0
        ;

        bool
            ProgramStarted,
            EngineTyped,
            TechTyped,
            LogoScrolled,
            Finished
        ;

        Ticker
            initialTicker,
            typeTicker,
            untilEnterTicker,
            typeEngineStuffTicker,
            typeTechStuffTicker,
            scrollingLogoTicker,
            companyNameTicker,
            fadeToBlackTicker,
            nextSceneTicker
        ;

        List<SFX> typingNoises = null;

        public LogoSceneConsoleTextSystem(string EXEtitle)
        {
            GoalString = "> " + EXEtitle;
        }

        public override void Reset()
        {
            commands = App.world.GetEntities().With<Text>().With<LogoSceneConsoleText>().AsSet();
            companyNames = App.world.GetEntities().With<Text>().With<LogoSceneCompanyName>().AsSet();
            cursors = App.world.GetEntities().With<Sprite>().With<LogoSceneCursor>().AsSet();

            ProgramStarted = EngineTyped = TechTyped = LogoScrolled = Finished = false;

            initialTicker = new Ticker(60) { ShouldReset = false, };
            typeTicker = new Ticker(3);
            untilEnterTicker = new Ticker(90) { ShouldReset = false };
            typeEngineStuffTicker = new Ticker(30) { ShouldReset = false, };
            typeTechStuffTicker = new Ticker(20) { ShouldReset = false, };
            scrollingLogoTicker = new Ticker(90) { ShouldReset = false, };
            companyNameTicker = new Ticker(60) { ShouldReset = false, };
            fadeToBlackTicker = new Ticker(60) { ShouldReset = false, };
            nextSceneTicker = new Ticker(60) { ShouldReset = false, };
        }

        List<SFX> newTypingNoises()
        {
            return typingNoises = new List<SFX>
            {
                App.assets.sfx("key_loud_1"),
                App.assets.sfx("key_medium_1"),
                App.assets.sfx("key_quiet_1"),
                App.assets.sfx("key_loud_2"),
                App.assets.sfx("key_medium_2"),
                App.assets.sfx("key_quiet_2"),
            };
        }

        void PlayTypingNoise()
        {
            if (typingNoises == null || typingNoises.Count <= 0)
            {
                newTypingNoises();
            }

            int i = Rand.Number(typingNoises.Count);
            typingNoises[i].Play(SFXCondition.New);
            typingNoises.RemoveAt(i);
        }

        public override void Update()
        {
            if(!IsActive)
            {
                return;
            }

            /// === SETUP LOGIC === ///
            
            if (!ProgramStarted)
            {
                if (!initialTicker.GetIsFinished())
                {
                    // nothing
                }
                else if (lengthConsole < GoalString.Length && typeTicker.GetIsFinished())
                {
                    lengthConsole++;

                    PlayTypingNoise();
                }
                else if (untilEnterTicker.GetIsFinished())
                {
                    ProgramStarted = true;

                    App.assets.sfx("key_enter").Play();
                }
            }
            else if(!EngineTyped)
            {
                if (typeEngineStuffTicker.GetIsFinished())
                {
                    GoalString += "\n\n\n\nBabel Engine 4\n\n(c) Silent Tower Games";
                    EngineTyped = true;
                }
            }
            else if(!TechTyped)
            {
                if(typeTechStuffTicker.GetIsFinished())
                {
                    GoalString += "\n\n\n\nInitializing Memory... DONE\n\nLoading Assets... DONE\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nTwitter: @SilentTowerGame\n\nhttps://silent-tower-games.itch.io\n\nhttps://silenttowergames.com/";
                    TechTyped = true;
                }
            }
            else if(!LogoScrolled)
            {
                if(scrollingLogoTicker.GetIsFinished())
                {
                    App.renderTargets[2].camera.Position.Y += 16;

                    if(App.renderTargets[2].camera.Position.Y >= App.renderTargets[2].Resolution.Y)
                    {
                        LogoScrolled = true;
                    }
                }
            }
            else if(companyNameTicker.GetIsFinished() && !Finished)
            {
                if(lengthCompanyName < CompanyName.Length)
                {
                    if (typeTicker.GetIsFinished())
                    {
                        lengthCompanyName = CompanyName.Length;

                        if (lengthCompanyName == CompanyName.Length)
                        {
                            Finished = true;

                            App.assets.sfx("console_loaded").Play();
                        }
                    }
                }
            }
            else if(Finished)
            {
                if(fadeToBlackTicker.GetIsFinished())
                {
                    App.renderTargets[2].camera.Position.Y = App.renderTargets[2].Resolution.Y * 4;

                    if(nextSceneTicker.GetIsFinished())
                    {
                        App.Scene = App.Scenes[NextScene];

                        IsActive = false;
                    }
                }
            }

            /// === DATA LOGIC === ///

            foreach (ref readonly Entity entity in commands.GetEntities())
            {
                ref Text text = ref entity.Get<Text>();

                if (!ProgramStarted)
                {
                    text.Message = GoalString.Substring(0, lengthConsole);
                }
                else
                {
                    text.Message = GoalString;
                }
            }

            foreach (ref readonly Entity entity in cursors.GetEntities())
            {
                ref Body body = ref entity.Get<Body>();

                if (ProgramStarted)
                {
                    body.Position = new Vector2(-64);

                    continue;
                }

                body.Position.X = (lengthConsole * 8) + 8;
                body.Position.Y = 8;
            }

            foreach (ref readonly Entity entity in companyNames.GetEntities())
            {
                ref Body body = ref entity.Get<Body>();
                ref Sprite sprite = ref entity.Get<Sprite>();
                ref Text text = ref entity.Get<Text>();

                sprite.Invisible = !LogoScrolled;
                
                if (!LogoScrolled)
                {
                    text.Message = "";
                }
                else
                {
                    sprite.Scale = new Vector2(lengthCompanyName, 1);
                    sprite.Origin.X = (lengthCompanyName / 18) - (lengthCompanyName == CompanyName.Length ? 1 : 0);
                    sprite.Origin.Y = 0;
                    text.Message = CompanyName.Substring(0, lengthCompanyName);
                }
            }
        }
    }
}
