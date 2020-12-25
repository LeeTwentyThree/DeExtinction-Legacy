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
        public EcoTargetType targetType = EcoTargetType.Shark;
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
                IEcoTarget closestTarget = EcoRegionManager.main.FindNearestTarget(targetType, transform.position, null, 1);
                if(closestTarget != null && closestTarget.GetGameObject() != null)
                {
                    if (Vector3.Distance(closestTarget.GetPosition(), transform.position) < maxReactDistance)
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
