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

namespace BabelEngine4.ECS.Components
{
    public struct Sprite
    {
        public SpriteSheet sheet;

        public string AnimationID
        {
            get;
            private set;
        }

        public SpriteEffects Effect;

        public int Frame;

        public float
            LayerDepth,
            Rotation
        ;

        public Ticker ticker;

        public Vector2 Origin, Scale;

        public AsepriteAnimation Animation => sheet.GetAnimation(AnimationID);

        public Sprite(SpriteSheet _sheet, string _AnimationID)
        {
            AnimationID = _AnimationID;
            Frame = 0;
            ticker = new Ticker();
            sheet = _sheet;
            Effect = SpriteEffects.None;
            Origin = new Vector2();
            Scale = new Vector2(1);
            LayerDepth = 0;
            Rotation = 0;
        }

        public void SetAnimation(string _AnimationID)
        {
            AnimationID = _AnimationID;
            Frame = 0;
        }
    }
}
