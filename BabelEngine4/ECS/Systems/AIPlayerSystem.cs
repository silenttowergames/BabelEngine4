using BabelEngine4.ECS.Components.AI;
using DefaultEcs;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class AIPlayerSystem : SystemSkeleton
    {
        EntitySet Set = null;

        public override void Reset()
        {
            Set = App.world.GetEntities().With<Director>().With<AIPlayer>().AsSet();
        }

        public override void Update()
        {
            foreach (ref readonly Entity e in Set.GetEntities())
            {
                ref Director director = ref e.Get<Director>();

                director.MoveRight = App.input.keyboard.Down(Keys.Right) || App.input.gamepad1.Down(Buttons.DPadRight) || App.input.gamepad1.Down(Buttons.LeftThumbstickRight);
                director.MoveLeft = App.input.keyboard.Down(Keys.Left) || App.input.gamepad1.Down(Buttons.DPadLeft) || App.input.gamepad1.Down(Buttons.LeftThumbstickLeft);
                director.MoveDown = App.input.keyboard.Down(Keys.Down) || App.input.gamepad1.Down(Buttons.DPadDown) || App.input.gamepad1.Down(Buttons.LeftThumbstickDown);
                director.MoveUp = App.input.keyboard.Down(Keys.Up) || App.input.gamepad1.Down(Buttons.DPadUp) || App.input.gamepad1.Down(Buttons.LeftThumbstickUp);
            }
        }
    }
}
