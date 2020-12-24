using ECCLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class ClownPincherScavengeBehaviour : CreatureAction
    {
        public ClownPincherBehaviour clownPincher;
        public float priorityWhileScavenging;
        EcoRegion.TargetFilter targetFilter;
        void Start()
        {
            targetFilter = new EcoRegion.TargetFilter(IsValidTarget);
        }
        public override float Evaluate(Creature creature)
        {
            if (creature.GetLastAction() == this && Time.time > timeStarted + 20f)
            {
                if(Time.time > timeStarted + 20f)
                {                
                    return 0f;
                }
                else
                {
                    return priorityWhileScavenging;
                }
            }
            if (clownPincher.nibble.CurrentlyEating)
            {
                return priorityWhileScavenging;
            }
            if (creature.Hunger.Value >= 0.9f)
            {
                if (TrySearchForFood(out IEcoTarget result))
                {
                    return evaluatePriority;
                }
                else
                {
                    clownPincher.nibble.NibbleOnce();
                }
            }
            return 0f;
        }
        public override void StartPerform(Creature creature)
        {
            timeStarted = Time.time;
            if (TrySearchForFood(out IEcoTarget ecoTarget))
            {
                SetCurrentTarget(ecoTarget);
            }
        }
        public override void Perform(Creature creature, float deltaTime)
        {
            if (Time.time > timeSearchAgain)
            {
                timeSearchAgain = Time.time + kFoodSearchInterval;
                if(currentTarget == null || currentTarget.GetGameObject() == null)
                {
                    if(TrySearchForFood(out IEcoTarget result))
                    {
                        SetCurrentTarget(result);
                    }
                }
            }
            if(currentTarget != null && currentTarget.GetGameObject() != null)
            {
                if (clownPincher.nibble.CurrentlyEating)
                {
                    swimBehaviour.LookAt(clownPincher.nibble.objectEating.transform);
                }
                else
                {
                    swimBehaviour.SwimTo(currentTarget.GetPosition(), swimVelocity);
                }
            }
        }
        void SetCurrentTarget(IEcoTarget newTarget)
        {
            currentTarget = newTarget;
            if(newTarget != null && newTarget.GetGameObject() != null)
            {
                swimBehaviour.SwimTo(currentTarget.GetPosition(), swimVelocity);
            }
        }
        private bool IsValidTarget(IEcoTarget target)
        {
            if (Random.value > 0.25f) return false;
            if (target == null || target.GetGameObject() == null) return false;
            return Vector3.Distance(transform.position, target.GetPosition()) < 20f;
        }
        bool TrySearchForFood(out IEcoTarget result)
        {
            result = null;
            IEcoTarget specialEdible = EcoRegionManager.main.FindNearestTarget(QPatch.clownPincherSpecialEdible, transform.position, targetFilter, 1);
            if (specialEdible != null && specialEdible.GetGameObject() != null)
            {
                result = specialEdible;
                return true;
            }
            IEcoTarget deadMeat = EcoRegionManager.main.FindNearestTarget(EcoTargetType.DeadMeat, transform.position, targetFilter, 1);
            if(deadMeat != null && deadMeat.GetGameObject() != null)
            {
                result = deadMeat;
                return true;
            }
            return false;
        }

        public float swimVelocity;

        float timeSearchAgain;

        float timeStarted;

        const float kFoodSearchInterval = 2f;

        IEcoTarget currentTarget;
    }
}
