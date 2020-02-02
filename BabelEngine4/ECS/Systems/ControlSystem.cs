using BabelEngine4.ECS.Components.AI;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class ControlSystem : IBabelSystem
    {
        public void Update()
        {
            Span<Director> directors = App.world.Get<Director>();
            KeyboardState k = Keyboard.GetState();

            for (int i = 0; i < directors.Length; i++)
            {
                ref Director director = ref directors[i];

                director.MoveRight = k.IsKeyDown(Keys.Right);
                director.MoveLeft = k.IsKeyDown(Keys.Left);
                director.MoveDown = k.IsKeyDown(Keys.Down);
                director.MoveUp = k.IsKeyDown(Keys.Up);
            }
        }
    }
}
