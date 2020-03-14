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

        public string Data
        {
            set
            {
                string[] Cols = value.Split(',');
                Tiles = new int[Cols.Length];

                for (int i = 0; i < Cols.Length; i++)
                {
                    Tiles[i] = int.Parse(Cols[i]) - 1;
                }
            }
        }

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

        public string spriteSheet;

        public SpriteSheet sheet
        {
            get
            {
                return App.assets.sprite(spriteSheet);
            }
        }

        public int this[int key]
        {
            get
            {
                return Tiles[key];
            }
        }

        public int this[int X, int Y]
        {
            get
            {
                int Tile = X + (Y * Dimensions.X);

                if (Tile < 0 || Tile > Tiles.Length)
                {
                    return -1;
                }

                return Tiles[Tile];
            }
        }
    }
}
