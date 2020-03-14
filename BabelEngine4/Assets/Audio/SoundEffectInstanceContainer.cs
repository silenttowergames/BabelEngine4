using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets.Audio
{
    public enum AudioState
    {
        Stopped,
        Start,
        Playing,
        Paused,
        PausedGame,
    }

    public class SoundEffectInstanceContainer
    {
        public AudioState State;

        SoundEffectInstance instance;

        public float Volume
        {
            get
            {
                return instance.Volume;
            }

            set
            {
                instance.Volume = value;
            }
        }

        public SoundEffectInstanceContainer(SoundEffectInstance _instance)
        {
            instance = _instance;
        }

        public void Update()
        {
            if (instance == null)
            {
                return;
            }

            switch (State)
            {
                case AudioState.Stopped:
                    {
                        instance.Stop();

                        break;
                    }
                case AudioState.Start:
                    {
                        State = AudioState.Playing;

                        if (instance.State == SoundState.Playing)
                        {
                            instance.Stop();
                        }
                        else if (instance.State == SoundState.Paused)
                        {
                            instance.Resume();

                            return;
                        }

                        instance.Play();

                        break;
                    }
                case AudioState.Playing:
                    {
                        if (instance.State != SoundState.Playing)
                        {
                            State = AudioState.Stopped;
                        }

                        break;
                    }
                case AudioState.Paused:
                case AudioState.PausedGame:
                    {
                        instance.Pause();

                        break;
                    }
            }
        }
    }
}
