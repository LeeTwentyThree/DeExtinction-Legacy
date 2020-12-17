using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    /// <summary>
    /// If the creature moves too far from the player, the creature returns, passing below them. Supposed to be a creepy visual thing.
    /// </summary>
    public class PassBelowPlayerAction : CreatureAction
    {
        bool performing;
        Vector3 target;

        public override float Evaluate(Creature creature)
        {
            if (performing)
            {
                float distance = Vector3.Distance(transform.position, target);
                if (distance > 10f && distance < 250f)
                {
                    return 0.9f;
                }
            }
            else
            {
                if(Vector3.Distance(transform.position, Player.main.transform.position) > 100f)
                {
                    return 0.9f;
                }
            }
            return 0f;
        }

        public override void StartPerform(Creature creature)
        {
            performing = true;
            target = Player.main.transform.position + new Vector3(0f, -20f, 0f);
            swimBehaviour.SwimTo(target, 50f);
        }
        public override void StopPerform(Creature creature)
        {
            performing = false;
        }
    }
}
