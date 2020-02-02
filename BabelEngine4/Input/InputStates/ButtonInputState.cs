using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Input.InputStates
{
    public class ButtonInputState<E> where E : Enum
    {
        Dictionary<E, int> Buttons = new Dictionary<E, int>();

        public virtual void Update() { }

        public int Get(E Button)
        {
            if (!Buttons.ContainsKey(Button))
            {
                return 0;
            }

            return Buttons[Button];
        }

        public void Set(E Button, bool IsDown)
        {
            int _Get = Get(Button);

            if (IsDown)
            {
                if(_Get < 0)
                {
                    _Get = 0;
                }

                _Get++;
            }
            else
            {
                if(_Get > 0)
                {
                    _Get = 0;
                }
                else
                {
                    _Get--;
                }
            }

            if(Buttons.ContainsKey(Button))
            {
                Buttons[Button] = _Get;
            }
            else
            {
                Buttons.Add(Button, _Get);
            }
        }

        public bool Down(E Button)
        {
            return Get(Button) > 0;
        }

        public bool Up(E Button)
        {
            return Get(Button) <= 0;
        }

        public bool Pressed(E Button)
        {
            return Get(Button) == 1;
        }

        public bool Released(E Button)
        {
            return Get(Button) == 0;
        }

        public bool Held(E Button)
        {
            int _Get = Get(Button);

            return _Get == 1 || (_Get >= 10 && ((_Get - InputManager.Held) % InputManager.HeldUnit == 0));
        }
    }
}
