using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Scenes
{
    public class TestScene0 : IScene
    {
        public void Load()
        {
            App.Factories["player"].Create(0, 0, 1);
            App.Factories["hud"].Create(0, 0, 1);
            App.Factories["tilemap-test"].Create(0, 0, 1);
        }
    }
}
