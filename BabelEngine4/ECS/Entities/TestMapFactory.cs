using BabelEngine4.ECS.Components.Rendering;
using DefaultEcs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Entities
{
    public class TestMapFactory : IEntityFactory
    {
        public void Create()
        {
            Entity m = App.world.CreateEntity();
            m.Set(new TileMap()
            {
                sheet = App.assets.sprite("8x8"),
                Dimensions = new Point(8, 4),
                Tiles = new int[] {
                    2, 2, 2, 2, 2, 2, 2, 2,
                    2, 0, 0, 0, 0, 0, 0, 2,
                    2, 0, 0, 2, 2, 3, 0, 2,
                    2, 2, 2, 2, 2, 2, 2, 2,
                }
            });
        }
    }
}
