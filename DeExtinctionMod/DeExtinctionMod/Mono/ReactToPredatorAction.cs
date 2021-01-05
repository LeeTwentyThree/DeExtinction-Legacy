using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary.Internal;

namespace DeExtinctionMod.Mono
{
    public class ReactToPredatorAction : CreatureAction
    {
        void Start()
        {
            lastScare = gameObject.EnsureComponent<LastScarePosition>();
        }

        public float maxReactDistance;
        public EcoTargetType targetType = EcoTargetType.Shark;
        public float actionLength = 1f;

        protected LastScarePosition lastScare;

        public override float Evaluate(Creature creature)
        {
                IEcoTarget closestTarget = EcoRegionManager.main.FindNearestTarget(targetType, transform.position, null, 1);
                if (closestTarget != null && closestTarget.GetGameObject() != null)
                {
                    if (Vector3.Distance(closestTarget.GetPosition(), transform.position) < maxReactDistance)
                    {
                        performingAction = true;
                        timeStopAction = Time.time + actionLength;
                        if(lastScare) lastScare.lastScarePosition = closestTarget.GetPosition();
                    }
                }
            if (performingAction)
            {
                if (Time.time > timeStopAction)
                {
                    performingAction = false;
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
        
        protected bool performingAction;
        protected float timeStopAction;
    }
}
