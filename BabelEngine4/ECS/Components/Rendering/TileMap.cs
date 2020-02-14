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
        public int RenderTargetID;

        public float LayerDepth;

        public int[] Tiles;

        public Point Dimensions;

        public SpriteSheet sheet;
    }
}
