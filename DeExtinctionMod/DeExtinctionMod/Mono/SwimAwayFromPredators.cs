using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class SwimAwayFromPredators : ReactToPredatorAction
    {
        public float swimVelocity;

        public override void Perform(Creature creature, float deltaTime)
        {
            Vector3 targetDirection = (transform.position - lastScare.lastScarePosition).normalized;
            swimBehaviour.SwimTo(transform.position + (targetDirection * swimVelocity * 5f), swimVelocity);
        }
    }
}
