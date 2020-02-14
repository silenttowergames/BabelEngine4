using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Input.InputStates
{
    public class Keyboard : ButtonInputState<Keys>
    {
        KeyboardState State;

        public static Keys[] AllKeys = null;

        public Keyboard()
        {
            if (AllKeys == null)
            {
                AllKeys = (Keys[])Enum.GetValues(typeof(Keys));
            }
        }

        public override void Update()
        {
            State = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            foreach (Keys K in AllKeys)
            {
                Set(K, State.IsKeyDown(K));
            }
        }
    }
}
