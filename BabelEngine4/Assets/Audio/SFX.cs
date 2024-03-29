﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets.Audio
{
    public class SFX : Asset<SoundEffect>, IAudioAsset
    {
        public enum SFXCondition
        {
            /// <summary>
            /// Grab a new instance if available, hell or high water
            /// </summary>
            New,
            /// <summary>
            /// Grab a new instance if nothing else is playing
            /// </summary>
            NewIfQuiet,
            /// <summary>
            /// Grab a new instance & quiet all of the playing SFX
            /// </summary>
            NewQuietAll,
        }

        /// <summary>
        /// You can only have up to 4 unique instances of a sound effect. Only 16 can play at a time anyway!
        /// </summary>
        SoundEffectInstanceContainer[] instances = new SoundEffectInstanceContainer[4];

        public SFX(string _Filename) : base(_Filename)
        {
        }

        public override void Load(ContentManager Content)
        {
            base.Load(Content);

            for (int i = 0; i < instances.Length; i++)
            {
                instances[i] = new SoundEffectInstanceContainer(Raw.CreateInstance());
            }
        }

        public SoundEffectInstanceContainer Play(SFXCondition Condition = default)
        {
            int ret = -1;

            for (int i = 0; i < instances.Length; i++)
            {
                if (instances[i].State == AudioState.Playing)
                {
                    // New: this isn't the one, keep looking for an available instance
                    // NewIfQuiet: get out of here, it isn't quiet
                    // NewQuietAll: quiet this one down, and if we're on 0, don't continue

                    if (Condition == SFXCondition.NewQuietAll)
                    {
                        instances[i].State = AudioState.Stopped;

                        if (i != 0)
                        {
                            continue;
                        }
                    }

                    if (Condition == SFXCondition.New)
                    {
                        continue;
                    }

                    if (Condition == SFXCondition.NewIfQuiet)
                    {
                        return null;
                    }
                }

                ret = i;

                instances[i].State = AudioState.Start;

                break;
            }

            if (ret < 0 || ret >= instances.Length)
            {
                return null;
            }

            return instances[ret];
        }

        public void SetVolume(float Volume)
        {
            for (int i = 0; i < instances.Length; i++)
            {
                instances[i].Volume = Math.Min(1, Math.Max(0, Volume));
            }
        }

        public void Update(bool Inactive = false)
        {
            for (int i = 0; i < instances.Length; i++)
            {
                if (Inactive)
                {
                    if (instances[i].State == AudioState.Playing)
                    {
                        instances[i].State = AudioState.PausedGame;
                    }
                }
                else if (instances[i].State == AudioState.PausedGame)
                {
                    instances[i].State = AudioState.Start;
                }

                instances[i].Update();
            }
        }
    }
}
