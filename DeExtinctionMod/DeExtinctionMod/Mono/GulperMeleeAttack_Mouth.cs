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
			attackSource.maxDistance = 50f;
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
				if (component.Aggression.Value >= 0.1f)
				{
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
								timeCinematicAgain = Time.time + 2f;
								return;
							}
						}
						else if (gulperBehaviour.GetCanGrabVehicle())
						{
							SeaMoth component4 = target.GetComponent<SeaMoth>();
							if (component4 && !component4.docked)
							{
								gulperBehaviour.GrabGenericSub(component4);
								component.Aggression.Value -= 0.25f;
								return;
							}
							Exosuit component5 = target.GetComponent<Exosuit>();
							if (component5 && !component5.docked)
							{
								gulperBehaviour.GrabExosuit(component5);
								component.Aggression.Value -= 0.25f;
								return;
							}
						}
						LiveMixin liveMixin = target.GetComponent<LiveMixin>();
						if (liveMixin == null) return;
						if (CanSwallowWhole(collider.gameObject, liveMixin))
						{
								Destroy(liveMixin.gameObject, 0.5f);
								var suckInWhole = collider.gameObject.AddComponent<BeingSuckedInWhole>();
								suckInWhole.animationLength = 0.5f;
								suckInWhole.target = throat;
						}
						else
						{
							liveMixin.TakeDamage(GetBiteDamage(target));
						}
						creature.GetAnimator().SetTrigger("bite");
						component.Aggression.Value -= 0.15f;
					}
				}
			}
		}
		private float GetBiteDamage(GameObject target)
		{
			if (target.GetComponent<SubControl>() != null)
			{
				return 300f; //cyclops damage
			}
			return biteDamage; //base damage
		}
		private bool CanSwallowWhole(GameObject gameObject, LiveMixin liveMixin)
		{
			if (gameObject.GetComponent<Player>())
			{
				return false;
			}
			if (gameObject.GetComponent<Vehicle>())
			{
				return false;
			}
			if (gameObject.GetComponent<SubRoot>())
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
    }
}
