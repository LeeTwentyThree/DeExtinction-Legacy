using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class RoarAbility : MonoBehaviour
    {
        AudioSource source;
        Creature creature;
        public float minRoarDistance = 1f;
        public float maxRoarDistance = 50f;
        public string animationName;
        public string clipPrefix;
        public bool createCurrent;
        public float currentStrength;
        public ModAudio modAudio;

        ModAudio.AudioClipPool clipPool;

        void Start()
        {
            source = gameObject.AddComponent<AudioSource>();
            source.minDistance = minRoarDistance;
            source.maxDistance = maxRoarDistance;
            source.spatialBlend = 1f;

            clipPool = modAudio.CreateClipPool(clipPrefix);

            creature = GetComponent<Creature>();
        }

        public void PlayRoar()
        {
            creature.GetAnimator().SetTrigger(animationName);
            AudioClip clip = clipPool.GetRandomClip();
            source.PlayOneShot(clip);
            if (createCurrent)
            {
                WorldForces.AddCurrent(source.transform.position, DayNightCycle.main.timePassed, 40f, transform.forward, currentStrength, 5f);
            }
        }
    }
}
