using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class DiveAction : CreatureAction
    {
        public override float Evaluate(Creature creature)
        {
            if(creature.transform.position.y > Ocean.main.GetOceanLevel() - 90f || (creature.transform.position.y < Ocean.main.GetOceanLevel() - 150f && creature.transform.position.y > Ocean.main.GetOceanLevel() - 350f))
            {
                return 0.75f;
            }
            return 0f;
        }

        public override void StartPerform(Creature creature)
        {
            swimBehaviour.SwimTo(new Vector3(transform.position.x, -100f, transform.position.z), 25f);
        }
    }
}
