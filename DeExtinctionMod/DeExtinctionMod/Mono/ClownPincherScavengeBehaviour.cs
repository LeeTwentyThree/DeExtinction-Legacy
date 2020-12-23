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

        public override float Evaluate(Creature creature)
        {
            if (clownPincher.nibble.CurrentlyEating)
            {
                return evaluatePriority;
            }
            if (creature.Hunger.Value >= 0.9f)
            {
                if (TrySearchForFood(out IEcoTarget result))
                {
                    return evaluatePriority;
                }
            }
            return 0f;
        }
        public override void StartPerform(Creature creature)
        {
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
                if(currentTarget.GetGameObject() == null)
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
        bool TrySearchForFood(out IEcoTarget result)
        {
            result = null;
            IEcoTarget specialEdible = EcoRegionManager.main.FindNearestTarget(QPatch.clownPincherSpecialEdible, transform.position, null, 2);
            if (specialEdible != null && specialEdible.GetGameObject() != null)
            {
                result = specialEdible;
                return true;
            }
            IEcoTarget deadMeat = EcoRegionManager.main.FindNearestTarget(EcoTargetType.DeadMeat, transform.position, null, 2);
            if(deadMeat != null && deadMeat.GetGameObject() != null)
            {
                result = deadMeat;
                return true;
            }
            return false;
        }

        public float swimVelocity;

        float timeSearchAgain;

        const float kFoodSearchInterval = 2f;

        IEcoTarget currentTarget;
    }
}
