using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeExtinctionMod.Mono
{
    public class ClownPincherBehaviour : Creature
    {
        public AnimateByVelocity animateByVelocity;

        public override void Start()
        {
            base.Start();
            animateByVelocity = GetComponent<AnimateByVelocity>();
        }

        public void PlayEatAnimation()
        {
            animateByVelocity.EvaluateRandom();
            GetAnimator().SetTrigger("eat");
        }
    }
}
