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
            
            for (int i = 0; i < directors.Length; i++)
            {
                ref Director director = ref directors[i];

                director.MoveRight = App.input.keyboard.Held(Keys.Right) || App.input.gamepad1.Down(Buttons.DPadRight) || App.input.gamepad1.Down(Buttons.LeftThumbstickRight);
                director.MoveLeft = App.input.keyboard.Held(Keys.Left) || App.input.gamepad1.Down(Buttons.DPadLeft) || App.input.gamepad1.Down(Buttons.LeftThumbstickLeft);
                director.MoveDown = App.input.keyboard.Held(Keys.Down) || App.input.gamepad1.Down(Buttons.DPadDown) || App.input.gamepad1.Down(Buttons.LeftThumbstickDown);
                director.MoveUp = App.input.keyboard.Held(Keys.Up) || App.input.gamepad1.Down(Buttons.DPadUp) || App.input.gamepad1.Down(Buttons.LeftThumbstickUp);
            }
        }

        public void Reset()
        {
            // nothing
        }

        public void OnLoad()
        {
            // nothing
        }
    }
}
