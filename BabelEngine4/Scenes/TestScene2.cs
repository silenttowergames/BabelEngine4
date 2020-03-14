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
        Point SpawnMeasurements = new Point(16, 16);

        public void Load()
        {
            App.Factories["player"].Create(0, 1, 1, new Vector2(24));

            for (int X = 0; X < SpawnMeasurements.X; X++)
            {
                for (int Y = 0; Y < SpawnMeasurements.Y; Y++)
                {
                    App.Factories["player-dead"].Create(0, 1, 1, new Vector2(X * 16, Y * 16));
                }
            }
        }
    }
}
