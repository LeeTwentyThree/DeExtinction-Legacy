using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class ReactToPredatorAction : CreatureAction
    {
        void Start()
        {
            lastScare = GetComponent<LastScarePosition>();
        }

        public float maxReactDistance;
        public float actionLength;

        protected LastScarePosition lastScare;

        public override float Evaluate(Creature creature)
        {
            if (performingAction)
            {
                if(Time.time > timeStopAction)
                {
                    performingAction = false;
                }
            }
            else
            {
                float distanceMultipier = 1f;
                IEcoTarget closestTarget = EcoRegionManager.main.FindNearestTarget(EcoTargetType.Shark, transform.position, null, 1);
                if(closestTarget == null || closestTarget.GetGameObject() == null)
                {
                    closestTarget = EcoRegionManager.main.FindNearestTarget(EcoTargetType.Leviathan, transform.position, null, 2);
                    distanceMultipier = 2f;
                }
                if(closestTarget != null && closestTarget.GetGameObject() != null)
                {
                    if (Vector3.Distance(closestTarget.GetPosition(), transform.position) < maxReactDistance * distanceMultipier)
                    {
                        performingAction = true;
                        timeStopAction = Time.time + actionLength;
                        lastScare.lastScarePosition = closestTarget.GetPosition();
                    }
                }
            }

            if (performingAction)
            {
                return evaluatePriority;
            }
            else
            {
                return 0f;
            }
        }
        
        bool performingAction;
        float timeStopAction;
    }
}
