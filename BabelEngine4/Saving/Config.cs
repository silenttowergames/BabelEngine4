﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Saving
{
    public class Config : IStorable
    {
        Dictionary<int, List<Buttons>> BControl = new Dictionary<int, List<Buttons>>();

        Dictionary<int, List<Keys>> KControl = new Dictionary<int, List<Keys>>();

        float
            volumeMaster = 0.5f,
            volumeMusic = 0.5f,
            volumeSFX = 0.5f
        ;

        public float VolumeMaster
        {
            get
            {
                return volumeMaster;
            }

            set
            {
                volumeMaster = value;

                SetVolume();
            }
        }

        public float VolumeMusic
        {
            get
            {
                return volumeMusic;
            }

            set
            {
                volumeMusic = value;

                SetVolume();
            }
        }

        public float VolumeSFX
        {
            get
            {
                return volumeSFX;
            }

            set
            {
                volumeSFX = value;

                SetVolume();
            }
        }

        public Point WindowSize;

        string filename;

        public Config() { }

        public Config(string _Filename = null)
        {
            filename = _Filename;
        }

        public void OnLoad()
        {
            if (WindowSize != new Point())
            {
                App.windowManager.WindowSize = WindowSize;
            }
            else
            {
                WindowSize = App.windowManager.WindowSize;
            }

            SetVolume();
        }

        public void Save()
        {
            WindowSize = App.windowManager.WindowSize;

            Storage.Save(filename, this);
        }

        public string Filename(string _Filename = null)
        {
            if (_Filename != null)
            {
                filename = _Filename;
            }

            return filename;
        }

        public static float Euler(float i)
        {
            return (float)Math.Pow(i, 2.7183);
        }

        void SetVolume()
        {
            if (!App.assets.Loaded)
            {
                return;
            }

            float
                Music = VolumeMusic * VolumeMaster,
                SFX = VolumeSFX * VolumeMaster
            ;

            App.assets.SetVolumeMusic(Music);
            App.assets.SetVolumeSFX(SFX);
        }

        List<T> GetInput<T>(int i, Dictionary<int, List<T>> _dictionary)
        {
            if (!_dictionary.ContainsKey(i))
            {
                _dictionary.Add(i, new List<T>());
            }

            return _dictionary[i];
        }

        bool SetInput<T>(int i, T Val, Dictionary<int, List<T>> _dictionary)
        {
            if (!_dictionary.ContainsKey(i))
            {
                _dictionary.Add(i, new List<T>());
            }

            if (_dictionary[i].Contains(Val))
            {
                _dictionary[i].Remove(Val);

                return false;
            }

            _dictionary[i].Add(Val);

            return true;
        }

        public List<Keys> GetKeys(int i)
        {
            return GetInput(i, KControl);
        }

        public List<Buttons> GetButtons(int i)
        {
            return GetInput(i, BControl);
        }

        public bool SetKeys(int i, Keys Val)
        {
            return SetInput(i, Val, KControl);
        }

        public bool SetButtons(int i, Buttons Val)
        {
            return SetInput(i, Val, BControl);
        }
    }
}
