using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class SleepAtNight : CreatureAction
    {
        float timeSwimAgain;
        public float lightThreshold = 0.2f;
        public float sleepSwimVelocity = 0.5f;
        public float swimInterval = 2f;
        public Vector3 swimRadius = new Vector3(15f, 5f, 15f);

        public override float Evaluate(Creature creature)
        {
            if(transform.position.y < -300f)
            {
                return 0f;
            }
            if (DayNightCycle.main.GetLocalLightScalar() < 0.2f) 
            {
                return evaluatePriority;
            }
            return 0f;
        }

        public override void StartPerform(Creature creature)
        {
            timeSwimAgain = Time.time;
        }
        public override void Perform(Creature creature, float deltaTime)
        {
            if(Time.time > timeSwimAgain)
            {
                timeSwimAgain = Time.time + swimInterval;
                swimBehaviour.SwimTo(transform.position + Vector3.Scale(Random.insideUnitSphere + (transform.forward * 0.25f), swimRadius), sleepSwimVelocity);
            }
        }
    }
}
