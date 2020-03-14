﻿using BabelEngine4.ECS.Components.Rendering;
using DefaultEcs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets.Tiled
{
    public class Map : Asset<TiledMapContainer>
    {
        public Map(string _Filename) : base(_Filename)
        {
        }

        public override void Load(ContentManager Content)
        {
            base.Load(Content);

            Raw.LoadMap();
        }

        public void Generate()
        {
            // Tile layers
            for (int L = 0; L < Raw.map.layers.Count; L++)
            {
                TiledLayerTile layer = (TiledLayerTile)Raw.map.layers[L];

                Entity t = App.world.CreateEntity();
                t.Set(new TileMap()
                {
                    Data = layer.data.value,
                    Dimensions = new Point(
                        layer.width,
                        layer.height
                    ),
                    LayerID = layer.ID,
                    spriteSheet = Raw.map.tileset.name,
                    LayerDepth = 1,
                    Solid = (layer.Property("collisions") == "true")
                });
            }

            // Object layers
            for (int O = 0; O < Raw.map.objectGroups.Count; O++)
            {
                for (int o = 0; o < Raw.map.objectGroups[O].objects.Count; o++)
                {
                    float Parallax;

                    if(!float.TryParse(TiledProperty.prop(Raw.map.objectGroups[O].properties, "parallax"), out Parallax))
                    {
                        Parallax = 1;
                    }

                    App.Factories[Raw.map.objectGroups[O].objects[o].type].Create(
                        0,
                        Raw.map.objectGroups[O].ID,
                        Parallax,
                        new Vector2(
                            Raw.map.objectGroups[O].objects[o].x,
                            Raw.map.objectGroups[O].objects[o].y
                        ),
                        Raw.map.objectGroups[O].objects[o]
                    );
                }
            }
        }
    }
}
