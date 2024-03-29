﻿using BabelEngine4.Assets.Aseprite;
using BabelEngine4.Assets.Sprites;
using BabelEngine4.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Components.Rendering
{
    public struct Sprite
    {
        public bool Invisible;
        
        public Color color;

        public Compass compass;

        string spriteSheet;

        public SpriteSheet sheet
        {
            get
            {
                return App.assets.sprite(spriteSheet);
            }
        }

        string animationID;

        public string AnimationID
        {
            get
            {
                return animationID;
            }

            set
            {
                if (animationID == value)
                {
                    return;
                }

                animationID = value;

                Frame = 0;

                animationTicker = new Ticker();
            }
        }

        public SpriteEffects Effect;

        public bool FlippedX
        {
            get
            {
                return Effect == SpriteEffects.FlipHorizontally;
            }

            set
            {
                if (value)
                {
                    Effect = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    Effect = SpriteEffects.None;
                }
            }
        }

        public int
            Frame,
            LayerID,
            RenderTargetID
        ;

        public float
            LayerDepth,
            Parallax
        ;

        public Ticker animationTicker;

        public Vector2 Origin, Scale;

        public AsepriteAnimation Animation => sheet.GetAnimation(AnimationID);

        public Sprite(string _sheet, string _AnimationID)
        {
            animationID = _AnimationID;
            color = Color.White;
            Frame = 0;
            animationTicker = new Ticker();
            spriteSheet = _sheet;
            Effect = SpriteEffects.None;
            Scale = new Vector2(1);
            LayerID = 0;
            LayerDepth = 0f;
            compass = new Compass();
            RenderTargetID = 0;
            Parallax = 1f;
            Origin = new Vector2((App.assets.sprite(spriteSheet)?.SizeEst.X ?? 8) / 2, (App.assets.sprite(spriteSheet)?.SizeEst.Y ?? 8) / 2);
            Invisible = false;
        }

        /// <summary>
        /// Simple way to alternate Effect between None and FlipHorizontally
        /// </summary>
        public void Flip()
        {
            switch (Effect)
            {
                case SpriteEffects.None:
                    {
                        Effect = SpriteEffects.FlipHorizontally;

                        break;
                    }
                case SpriteEffects.FlipHorizontally:
                    {
                        Effect = SpriteEffects.None;

                        break;
                    }
            }
        }
    }
}
