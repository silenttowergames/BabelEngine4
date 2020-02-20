using BabelEngine4.Assets.Fonts;
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
    public struct Text
    {
        public Color color;

        public Compass compass;

        public int LayerID;

        public float
            LayerDepth,
            Parallax
        ;

        public Font font;

        public int RenderTargetID;

        public SpriteEffects effect;

        public string Message;

        public Vector2
            Origin,
            Scale
        ;

        public Text(string _Message)
        {
            color = Color.Black;
            LayerDepth = 0f;
            compass = new Compass();
            effect = SpriteEffects.None;
            font = null;
            LayerID = 0;
            RenderTargetID = 0;
            Message = _Message;
            Origin = new Vector2();
            Scale = new Vector2(1);
            Parallax = 1f;
        }
    }
}
