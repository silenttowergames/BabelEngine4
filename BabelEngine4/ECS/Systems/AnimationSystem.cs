﻿using BabelEngine4.ECS.Components;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class AnimationSystem : IBabelSystem
    {
        public void Update()
        {
            Span<Sprite> sprites = App.world.Get<Sprite>();

            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i].ticker.GetIsFinished())
                {
                    if (sprites[i].Frame++ >= sprites[i].Animation.Length)
                    {
                        sprites[i].Frame = 0;
                    }

                    sprites[i].ticker.Reset(sprites[i].sheet.Meta.frames.Values.ElementAt(sprites[i].Animation.from + sprites[i].Frame).duration / (1000 / 60));
                }
            }
        }
    }
}