﻿using BabelEngine4.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class MusicBasicSystem : SystemSkeleton
    {
        public override void Update()
        {
            // There should be only one

            ReadOnlySpan<Jukebox> musics = App.world.Get<Jukebox>();

            if (musics.Length <= 0)
            {
                return;
            }

            musics[0].Music.Song.Play();
        }

        public override void OnUnload()
        {
            ReadOnlySpan<Jukebox> musics = App.world.Get<Jukebox>();

            if (musics.Length <= 0)
            {
                return;
            }

            musics[0].Music.Song.Stop();
        }
    }
}
