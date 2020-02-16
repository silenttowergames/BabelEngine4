using BabelEngine4.Assets.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Components.Rendering
{
    public struct TileMap
    {
        public bool Solid;

        public int LayerID, RenderTargetID;

        public float LayerDepth;

        public int[] Tiles;

        Point sizeEst;

        public Point SizeEst
        {
            get
            {
                if (sizeEst.X == 0)
                {
                    sizeEst = new Point(sheet.Meta.frames.Values.ElementAt(0).frame.w, sheet.Meta.frames.Values.ElementAt(0).frame.h);
                }

                return sizeEst;
            }
        }

        public Point Dimensions;

        public SpriteSheet sheet;
    }
}
