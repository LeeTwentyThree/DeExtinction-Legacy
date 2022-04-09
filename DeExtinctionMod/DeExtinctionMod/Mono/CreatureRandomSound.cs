using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class CreatureRandomSound : MonoBehaviour
    {
        private FMOD_CustomEmitter emitter;

        public Creature creature;
        public FMODAsset asset;
        public bool playRandomly = true;
        public float minInterval;
        public float maxInterval;
        public string animatorParameter = "roar";

        private float _timeNextRoar;

        private void Start()
        {
            emitter = gameObject.EnsureComponent<FMOD_CustomEmitter>();
            emitter.followParent = true;
            emitter.SetAsset(asset);

            creature = GetComponent<Creature>();

            _timeNextRoar = Time.time + Random.Range(minInterval, maxInterval);
        }

        private void Update()
        {
            if (Time.time > _timeNextRoar)
            {
                emitter.Play();
                creature.GetAnimator().SetTrigger(animatorParameter);
                _timeNextRoar = Time.time + Random.Range(minInterval, maxInterval);
            }
        }
    }
}
