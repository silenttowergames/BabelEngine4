using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets
{
    public class Asset<T>
    {
        public T Raw
        {
            get;
            protected set;
        }

        public string Filename
        {
            get;
            private set;
        }

        public Asset(string _Filename)
        {
            Filename = _Filename;
        }

        public virtual void Load(ContentManager Content)
        {
            Raw = Content.Load<T>(Filename);
        }
    }
}
