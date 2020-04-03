using BabelEngine4.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
namespace BabelEngine4.ECS.Components.AI
{
    public struct Director
    {
        /// <summary>
        /// Defaults to 16
        /// </summary>
        public float Speed;

        Actions state;

        public Actions State
        {
            get
            {
                return state;
            }

            set
            {
                if (value == Actions.Run)
                {
                    return;
                }

                state = value;
            }
        }

        public Compass compass;

        public float
            SpeedWalk,
            SpeedRun
        ;

        Dictionary<Actions, bool> States;

        public bool this[Actions action]
        {
            get
            {
                if (States == null || !States.ContainsKey(action))
                {
                    return false;
                }

                return States[action];
            }

            set
            {
                if (States == null)
                {
                    States = new Dictionary<Actions, bool>();
                }

                if (!States.ContainsKey(action))
                {
                    States.Add(action, value);
                }
                else
                {
                    States[action] = value;
                }

                if (value)
                {
                    State = action;
                }
            }
        }

        public void Reset()
        {
            foreach (Actions action in Program.ActionsValues)
            {
                this[action] = false;
            }
        }
    }
}
*/
