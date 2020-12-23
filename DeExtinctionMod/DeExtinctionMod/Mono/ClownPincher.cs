using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class ClownPincherBehaviour : MonoBehaviour
    {
        public AnimateByVelocity animateByVelocity;
        public Creature creature;
        public ClownPincherNibble nibble;
        public ClownPincherScavengeBehaviour scavengeBehaviour;
        public SwimBehaviour swimBehaviour;

        public void Start()
        {
            animateByVelocity = GetComponent<AnimateByVelocity>();
            creature = GetComponent<Creature>();
            nibble = GetComponentInChildren<ClownPincherNibble>();
            creature.initialHunger = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });
            scavengeBehaviour = GetComponent<ClownPincherScavengeBehaviour>();
            swimBehaviour = GetComponent<SwimBehaviour>();
        }

        public void PlayEatAnimation()
        {
            animateByVelocity.EvaluateRandom();
            creature.GetAnimator().SetTrigger("eat");
        }
    }
}
