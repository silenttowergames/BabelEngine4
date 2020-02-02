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

        public override void Update()
        {
            State = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            foreach (Keys K in Enum.GetValues(typeof(Keys)))
            {
                Set(K, State.IsKeyDown(K));
            }
        }
    }
}
