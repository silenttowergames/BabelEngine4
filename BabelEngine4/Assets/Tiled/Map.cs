using BabelEngine4.ECS.Components.Rendering;
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
            //base.Load(Content);

            Raw = new TiledMapContainer(System.IO.File.ReadAllText("./Content/" + Filename + ".tmx"));

            Raw.LoadMap();
        }

        public void Generate()
        {
            for (int L = 0; L < Raw.map.allLayers.Count; L++)
            {
                if (Raw.map.allLayers[L] is TiledLayerTile)
                {
                    TiledLayerTile layer = (TiledLayerTile)Raw.map.allLayers[L];

                    Entity t = App.world.CreateEntity();
                    t.Set(new TileMap()
                    {
                        Data = layer.data.value,
                        Dimensions = new Point(
                            layer.width,
                            layer.height
                        ),
                        LayerID = L,
                        LayerName = layer.name,
                        spriteSheet = Raw.map.tileset.name,
                        LayerDepth = 0,
                        Solid = (layer.Property("collisions") == "true")
                    });
                }
                else if(Raw.map.allLayers[L] is TiledObjectGroup)
                {
                    TiledObjectGroup layer = (TiledObjectGroup)Raw.map.allLayers[L];

                    for (int o = 0; o < layer.objects.Count; o++)
                    {
                        float Parallax;

                        if (!float.TryParse(TiledProperty.prop(layer.properties, "parallax"), out Parallax))
                        {
                            Parallax = 1;
                        }

                        App.Factories[layer.objects[o].type].Create(
                            0,
                            L,
                            Parallax,
                            new Vector2(
                                layer.objects[o].x,
                                layer.objects[o].y
                            ),
                            layer.objects[o]
                        );
                    }
                }
            }
        }
    }
}
