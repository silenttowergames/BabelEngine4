using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class SystemSkeleton : IBabelSystem
    {
        public virtual void Reset() { }
        public virtual void OnLoad() { }
        public virtual void OnUnload() { }
        public virtual void Update() { }
    }
}
