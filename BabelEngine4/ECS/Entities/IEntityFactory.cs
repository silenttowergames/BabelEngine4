using BabelEngine4.Assets.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Entities
{
    public interface IEntityFactory
    {
        void Create(float LayerDepth, int LayerID, float Parallax, List<TiledProperty> properties = null);
    }
}
