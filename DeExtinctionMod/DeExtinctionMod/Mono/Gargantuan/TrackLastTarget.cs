using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono.Gargantuan
{
    public class TrackLastTarget : MonoBehaviour
    {
        private Vector3 defaultDirection;
        public LastTarget lastTarget;

        void Awake()
        {
            defaultDirection = transform.forward;
        }

        void Update()
        {
            Vector3 viewDirection;
            if (lastTarget.target == null)
            {
                viewDirection = defaultDirection;
            }
            else
            {
                viewDirection = lastTarget.target.transform.position - transform.position;
            }
            if (viewDirection == Vector3.zero) return;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(viewDirection), Time.deltaTime * 360f);
        }
    }
}
