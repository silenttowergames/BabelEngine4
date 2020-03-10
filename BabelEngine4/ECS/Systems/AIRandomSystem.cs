using BabelEngine4.ECS.Components.AI;
using BabelEngine4.Misc;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class AIRandomSystem : SystemSkeleton
    {
        EntitySet Set = null;

        public override void Reset()
        {
            Set = App.world.GetEntities().With<Director>().With<AIRandom>().AsSet();
        }

        public override void Update()
        {
            return;
            foreach (ref readonly Entity e in Set.GetEntities())
            {
                ref Director director = ref e.Get<Director>();
                
                director.MoveRight = Rand.Number(4) == 0;
                director.MoveLeft = Rand.Number(4) == 1;
                director.MoveDown = Rand.Number(4) == 2;
                director.MoveUp = Rand.Number(4) == 3;
            }
        }
    }
}
