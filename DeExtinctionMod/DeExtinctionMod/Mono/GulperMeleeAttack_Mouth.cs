using ECCLibrary;
using ECCLibrary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class GulperMeleeAttack_Mouth : MeleeAttack
    {
        private AudioSource attackSource;
        private ECCAudio.AudioClipPool clipPool;
        private PlayerCinematicController playerDeathCinematic;
        private Transform throat;
        float timeCinematicAgain;

        void Start()
        {
            attackSource = gameObject.AddComponent<AudioSource>();
            attackSource.minDistance = 10f;
            attackSource.maxDistance = 40f;
            attackSource.spatialBlend = 1f;
            attackSource.volume = ECCHelpers.GetECCVolume();
            clipPool = ECCAudio.CreateClipPool("GulperAttack");
            gameObject.SearchChild("Mouth").GetComponent<OnTouch>().onTouch = new OnTouch.OnTouchEvent();
            gameObject.SearchChild("Mouth").GetComponent<OnTouch>().onTouch.AddListener(OnTouch);
            playerDeathCinematic = gameObject.AddComponent<PlayerCinematicController>();
            playerDeathCinematic.animatedTransform = gameObject.SearchChild("PlayerCam").transform;
            playerDeathCinematic.animator = creature.GetAnimator();
            playerDeathCinematic.animParamReceivers = new GameObject[0];
            throat = gameObject.SearchChild("Throat").transform;
        }
        public override void OnTouch(Collider collider)
        {
            if (frozen)
            {
                return;
            }
            if (liveMixin.IsAlive() && Time.time > timeLastBite + biteInterval)
            {
                Creature component = GetComponent<Creature>();

                GameObject target = GetTarget(collider);
                GulperBehaviour gulperBehaviour = GetComponent<GulperBehaviour>();
                if (!gulperBehaviour.IsHoldingVehicle() && !playerDeathCinematic.IsCinematicModeActive())
                {
                    Player player = target.GetComponent<Player>();
                    if (player != null)
                    {
                        if (Time.time > timeCinematicAgain && player.CanBeAttacked() && player.liveMixin.IsAlive() && !player.cinematicModeActive)
                        {
                            Invoke("KillPlayer", 0.9f);
                            playerDeathCinematic.StartCinematicMode(player);
                            attackSource.clip = clipPool.GetRandomClip();
                            attackSource.Play();
                            timeCinematicAgain = Time.time + 4f;
                            timeLastBite = Time.time;
                            return;
                        }
                    }
                    else if (gulperBehaviour.GetCanGrabVehicle() && component.Aggression.Value > 0.1f)
                    {
                        SeaMoth seamoth = target.GetComponent<SeaMoth>();
                        if (seamoth && !seamoth.docked)
                        {
                            gulperBehaviour.GrabGenericSub(seamoth);
                            component.Aggression.Value -= 0.5f;
                            return;
                        }
                        Exosuit exosuiit = target.GetComponent<Exosuit>();
                        if (exosuiit && !exosuiit.docked)
                        {
                            gulperBehaviour.GrabExosuit(exosuiit);
                            component.Aggression.Value -= 0.5f;
                            return;
                        }
                    }
                    LiveMixin liveMixin = target.GetComponent<LiveMixin>();
                    if (liveMixin == null) return;
                    if (!liveMixin.IsAlive())
                    {
                        return;
                    }
                    if (!CanAttackTargetFromPosition(target))
                    {
                        return;
                    }
                    if (CanSwallowWhole(collider.gameObject, liveMixin))
                    {
                        Destroy(liveMixin.gameObject, 0.5f);
                        var suckInWhole = collider.gameObject.AddComponent<BeingSuckedInWhole>();
                        suckInWhole.animationLength = 0.5f;
                        suckInWhole.target = throat;
                        creature.GetAnimator().SetTrigger("bite");
                        return;
                    }
                    else
                    {
                        if (component.Aggression.Value >= 0.2f)
                        {
                            liveMixin.TakeDamage(GetBiteDamage(target));
                            timeLastBite = Time.time;
                            attackSource.clip = clipPool.GetRandomClip();
                            attackSource.Play();
                            creature.GetAnimator().SetTrigger("bite");
                            component.Aggression.Value = 0f;
                            return;
                        }
                    }
                }
            }
        }
        private bool CanAttackTargetFromPosition(GameObject target)
        {
            Vector3 direction = target.transform.position - transform.position;
            float magnitude = direction.magnitude;
            int num = UWE.Utils.RaycastIntoSharedBuffer(transform.position, direction, magnitude, -5, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < num; i++)
            {
                Collider collider = UWE.Utils.sharedHitBuffer[i].collider;
                GameObject gameObject = (collider.attachedRigidbody != null) ? collider.attachedRigidbody.gameObject : collider.gameObject;
                if (!(gameObject == target) && !(gameObject == base.gameObject) && !(gameObject.GetComponent<Creature>() != null))
                {
                    return false;
                }
            }
            return true;
        }
        private float GetBiteDamage(GameObject target)
        {
            if (target.GetComponent<SubControl>() != null)
            {
                return 50f; //cyclops damage
            }
            return biteDamage; //base damage
        }
        private bool CanSwallowWhole(GameObject gameObject, LiveMixin liveMixin)
        {
            if (gameObject.GetComponentInParent<Player>())
            {
                return false;
            }
            if (gameObject.GetComponentInChildren<Player>())
            {
                return false;
            }
            if (gameObject.GetComponentInParent<Vehicle>())
            {
                return false;
            }
            if (gameObject.GetComponentInParent<SubRoot>())
            {
                return false;
            }
            if (liveMixin.maxHealth > 600f)
            {
                return false;
            }
            if (liveMixin.invincible)
            {
                return false;
            }
            return true;
        }
        private void KillPlayer()
        {
            Player.main.liveMixin.Kill(DamageType.Normal);
            playerDeathCinematic.OnPlayerCinematicModeEnd();
        }
        public void OnVehicleReleased()
        {
            timeLastBite = Time.time;
        }
    }
}
