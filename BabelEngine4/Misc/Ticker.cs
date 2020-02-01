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

        public Ticker(int _Limit)
        {
            Current = 0;
            Limit = _Limit;
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
                Current = 0;
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
