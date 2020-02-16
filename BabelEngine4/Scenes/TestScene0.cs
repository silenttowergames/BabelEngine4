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
            App.Factories["player"].Create();
            App.Factories["hud"].Create();
            App.Factories["tilemap-test"].Create();
        }
    }
}
