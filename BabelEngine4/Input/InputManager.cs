using BabelEngine4.Input.InputStates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Input
{
    enum BtnState
    {
        Down,
        Up,
        Pressed,
        Released,
        Held,
    }

    public class InputManager
    {
        public static int
            Held = 30,
            HeldUnit = 2
        ;

        public Keyboard keyboard = new Keyboard();

        public Gamepad
            gamepad1 = new Gamepad(PlayerIndex.One),
            gamepad2 = new Gamepad(PlayerIndex.Two),
            gamepad3 = new Gamepad(PlayerIndex.Three),
            gamepad4 = new Gamepad(PlayerIndex.Four)
        ;

        public void Update()
        {
            keyboard.Update();
            gamepad1.Update();
            gamepad2.Update();
            gamepad3.Update();
            gamepad4.Update();
        }
    }
}
