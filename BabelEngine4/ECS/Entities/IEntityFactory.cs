using BabelEngine4.Assets.Tiled;
using DefaultEcs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Entities
{
    public interface IEntityFactory
    {
        Entity Create(float LayerDepth, int LayerID, float Parallax, Vector2 Position = default, List<TiledProperty> properties = null);
    }
}
