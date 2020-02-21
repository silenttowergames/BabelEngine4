using BabelEngine4.Assets.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Scenes
{
    public class TiledScene : IScene
    {
        public Map map;

        public TiledScene(string _Filename)
        {
            map = App.assets.map(_Filename);
        }

        public void Load()
        {
            map.Generate();

            App.Factories["menu"].Create(1, 3, 0);
        }
    }
}
