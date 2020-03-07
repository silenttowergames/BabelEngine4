using BabelEngine4.ECS.Components;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Scenes
{
    public class TestScene2 : IScene
    {
        public void Load()
        {
            Entity p = App.Factories["player"].Create(0, 1, 1);
            p.Get<Body>().Position = new Microsoft.Xna.Framework.Vector2(54, 60);

            Entity pd = App.Factories["player-dead"].Create(0, 1, 1);
            pd.Get<Body>().Position = new Microsoft.Xna.Framework.Vector2(64);
        }
    }
}
