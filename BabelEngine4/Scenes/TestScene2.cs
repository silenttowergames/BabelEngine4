using BabelEngine4.ECS.Components;
using DefaultEcs;
using Microsoft.Xna.Framework;
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
            App.Factories["player"].Create(0, 1, 1, new Vector2(32, 32));

            App.Factories["player-dead"].Create(0, 1, 1, new Vector2(40, 40));
            App.Factories["player-dead"].Create(0, 1, 1, new Vector2(48, 40));
            App.Factories["player-dead"].Create(0, 1, 1, new Vector2(64, 40));
        }
    }
}
