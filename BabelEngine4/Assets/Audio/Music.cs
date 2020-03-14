using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets.Audio
{
    public class Music : Asset<SoundEffect>, IAudioAsset
    {
        public bool Inactive = false;

        public SoundEffectInstance Song
        {
            get;
            protected set;
        }

        public Music(string _Filename) : base(_Filename)
        {
        }

        public override void Load(ContentManager Content)
        {
            base.Load(Content);

            Song = Raw.CreateInstance();

            Song.IsLooped = true;
        }

        public void SetVolume(float Volume)
        {
            Volume = Math.Max(Math.Min(Volume, 1), 0);

            Song.Volume = Volume;
        }
    }
}
