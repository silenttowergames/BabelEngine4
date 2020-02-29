using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Misc
{
    public static class Rand
    {
        private static Random random = null;

        public static Random Random
        {
            get
            {
                if (random == null)
                {
                    random = new Random(Guid.NewGuid().GetHashCode());
                }

                return random;
            }
        }

        public static int Number(int Min, int Max)
        {
            return Random.Next(Min, Max);
        }

        public static int Number(int Max)
        {
            return Number(0, Max);
        }

        public static string String(int Length)
        {
            // Not super secure I bet lol
            string Ret = "";

            for (int i = 0; i < Length; i++)
            {
                Ret += Convert.ToChar(Convert.ToInt32(26 * Random.NextDouble() + 65));
            }

            return Ret;
        }
    }
}
