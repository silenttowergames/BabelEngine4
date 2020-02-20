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

        public SpriteSheet sheet;

        public string AnimationID
        {
            get;
            private set;
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

        public Sprite(SpriteSheet _sheet, string _AnimationID)
        {
            AnimationID = _AnimationID;
            color = Color.White;
            Frame = 0;
            animationTicker = new Ticker();
            sheet = _sheet;
            Effect = SpriteEffects.None;
            Origin = new Vector2(sheet.SizeEst.X / 2, sheet.SizeEst.Y / 2);
            Scale = new Vector2(1);
            LayerID = 0;
            LayerDepth = 0f;
            compass = new Compass();
            RenderTargetID = 0;
            Parallax = 1f;
        }

        public void SetAnimation(string _AnimationID)
        {
            AnimationID = _AnimationID;
            Frame = 0;
        }
    }
}
