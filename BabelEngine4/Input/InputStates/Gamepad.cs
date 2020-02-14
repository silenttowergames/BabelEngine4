using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Input.InputStates
{
    public class Gamepad : ButtonInputState<Buttons>
    {
        GamePadState State;

        PlayerIndex Index;

        public static Buttons[] AllButtons = null;

        public Gamepad(PlayerIndex _Index)
        {
            Index = _Index;

            if (AllButtons == null)
            {
                AllButtons = (Buttons[])Enum.GetValues(typeof(Buttons));
            }
        }

        public override void Update()
        {
            State = GamePad.GetState(Index);

            foreach (Buttons K in AllButtons)
            {
                Set(K, State.IsButtonDown(K));
            }
        }
    }
}
