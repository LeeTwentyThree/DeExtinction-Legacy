using ECCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeExtinctionMod.Mono
{
    public class ClownPincherEatBehaviour : CreatureAction
    {
        public override float Evaluate(Creature creature)
        {
            ErrorMessage.AddMessage(creature.Hunger.Value.ToString());
            if(creature.Hunger.Value > 0.2f)
            {
                return 0.6f;
            }
            return 0f;
        }
    }
}
