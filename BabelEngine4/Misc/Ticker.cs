using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Misc
{
    public struct Ticker
    {
        public int Limit
        {
            get;
            private set;
        }

        public int Current
        {
            get;
            private set;
        }

        bool DontReset;

        public bool ShouldReset
        {
            get
            {
                return !DontReset;
            }

            set
            {
                DontReset = !value;
            }
        }

        public Ticker(int _Limit)
        {
            Current = 0;
            Limit = _Limit;
            DontReset = false;
        }


        public void Reset()
        {
            Current = 0;
        }

        public void Reset(int _Limit)
        {
            Current = 0;
            Limit = _Limit;
        }

        public void Update()
        {
            if (Current++ >= Limit)
            {
                if (!DontReset)
                {
                    Current = 0;
                }
                else
                {
                    Current = Limit;
                }
            }
        }

        public bool IsFinished()
        {
            return Current >= Limit;
        }

        public bool GetIsFinished()
        {
            Update();

            return IsFinished();
        }
    }
}
