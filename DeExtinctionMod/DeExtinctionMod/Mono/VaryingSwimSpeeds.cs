using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class VaryingSwimSpeeds : MonoBehaviour
    {
        public float maxIncrease;
        public float variationRate;
        float baseVelocity;
        SwimRandom swim;

        void Start()
        {
            swim = GetComponent<SwimRandom>();
            baseVelocity = swim.swimVelocity;
        }

        void Update()
        {
            swim.swimVelocity = baseVelocity + (Mathf.PerlinNoise(Time.time * variationRate, 0f) * maxIncrease);
        }
    }
}
