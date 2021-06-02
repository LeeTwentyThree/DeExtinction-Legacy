using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class FreeFloating : CreatureAction
    {
        Vector3 lookDir;
        private Rigidbody rb;
        public float force = 0.02f;
        public Vector3 moveDirection = Vector3.forward;

        float timeUpdateRotation;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public override float Evaluate(Creature creature)
        {
            return evaluatePriority;
        }

        public override void Perform(Creature creature, float deltaTime)
        {
            if (Time.time > timeUpdateRotation)
            {
                timeUpdateRotation = Time.time + 0.5f + (Random.value * 0.5f);
                lookDir = Random.onUnitSphere;
            }
        }

        void FixedUpdate()
        {
            rb.AddForce(moveDirection * force * rb.mass);
            if(lookDir != Vector3.zero)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDir), Time.fixedDeltaTime * 15f);
        }
    }
}
