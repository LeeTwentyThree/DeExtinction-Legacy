using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class PhotosynthesizeAction : CreatureAction
    {
        public override float Evaluate(Creature creature)
        {
            if(DayNightCycle.main.GetLightScalar() > 0.5f && creature.transform.position.y < -15f)
            {
                return 0.85f;
            }
            return 0f;
        }

        public override void StartPerform(Creature creature)
        {
            swimBehaviour.SwimTo(new Vector3(transform.position.x, 0f, transform.position.z), 20f);
        }
    }
}
