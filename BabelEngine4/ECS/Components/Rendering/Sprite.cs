using BabelEngine4.Assets.Aseprite;
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
                animationID = value;
                Frame = 0;
            }
        }

        public SpriteEffects Effect;

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
            Origin = new Vector2(App.assets.sprite(spriteSheet).SizeEst.X / 2, App.assets.sprite(spriteSheet).SizeEst.Y / 2);
        }
    }
}
