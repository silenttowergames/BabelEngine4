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

        public int Current;

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

        public void Update(float TimeMult = 1)
        {
            if (Current++ >= (Limit / TimeMult))
            {
                if (!DontReset)
                {
                    Current = 0;
                }
                else
                {
                    Current = (int)(Limit / TimeMult);
                }
            }
        }

        public bool IsFinished(float TimeMult = 1)
        {
            return Current >= (Limit / TimeMult);
        }

        public bool GetIsFinished(float TimeMult = 1)
        {
            Update(TimeMult);

            return IsFinished(TimeMult);
        }

        public float Percentage(float TimeMult = 1)
        {
            return 1f / ((Limit / TimeMult) / Current);
        }
    }
}
