using BabelEngine4.Assets.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Components
{
    public struct Jukebox
    {
        public string music;

        public Music Music
        {
            get
            {
                return App.assets.music(music);
            }
        }
    }
}
