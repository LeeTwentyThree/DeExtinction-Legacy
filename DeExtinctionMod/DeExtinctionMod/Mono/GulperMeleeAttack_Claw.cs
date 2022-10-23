using ECCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class GulperMeleeAttack_Claw : MeleeAttack
    {
        public string animationTriggerName;
        public string colliderName;
        public bool isBaby;

        LiveMixin damagingTarget;
        AudioSource source;
        ECCAudio.AudioClipPool clipPool;
        GameObject clawObject;

        void Start()
        {
            clawObject = gameObject.SearchChild(colliderName);
            OnTouch onTouch = clawObject.GetComponent<OnTouch>();
            onTouch.onTouch = new OnTouch.OnTouchEvent();
            onTouch.onTouch.AddListener(OnTouch);
            source = clawObject.AddComponent<AudioSource>();
            source.volume = ECCHelpers.GetECCVolume();
            source.spatialBlend = 1f;
            source.minDistance = 3f;
            source.maxDistance = 20f;
            clipPool = ECCAudio.CreateClipPool("GulperClawAttack");
        }
        public override void OnTouch(Collider collider)
        {
            if (frozen) return;
            if (liveMixin.IsAlive() && Time.time > timeLastBite + biteInterval)
            {
                    damagingTarget = collider.GetComponent<LiveMixin>();
                    if(damagingTarget == null || damagingTarget.maxHealth < 100f)
                    {
                        return;
                    }
                    Player player = collider.gameObject.GetComponent<Player>();
                    if (player != null && (!player.CanBeAttacked() || isBaby))
                    {
                        return;
                    }
                    animator.SetTrigger(animationTriggerName);
                    source.clip = clipPool.GetRandomClip();
                    source.Play();
                    Invoke("DamageTarget", 0.6f);
                    timeLastBite = Time.time;
                
            }
        }

        void DamageTarget()
        {
            if(damagingTarget != null)
            {
                damagingTarget.TakeDamage(25f, clawObject.transform.position, DamageType.Normal, gameObject);
            }
        }
    }
}
