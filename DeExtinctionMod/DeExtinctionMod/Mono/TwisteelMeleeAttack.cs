using ECCLibrary;
using ECCLibrary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class TwisteelMeleeAttack : MeleeAttack
    {
		private AudioSource attackSource;
		private ECCAudio.AudioClipPool biteClipPool;
		private ECCAudio.AudioClipPool cinematicClipPool;
		private PlayerCinematicController playerDeathCinematic;
		float timeCinematicAgain;

		void Start()
		{
			attackSource = gameObject.AddComponent<AudioSource>();
			attackSource.minDistance = 2f;
			attackSource.maxDistance = 11f;
			attackSource.spatialBlend = 1f;
			attackSource.volume = ECCHelpers.GetECCVolume();
			biteClipPool = ECCAudio.CreateClipPool("TwisteelBite");
			cinematicClipPool = ECCAudio.CreateClipPool("TwisteelCin");
			gameObject.SearchChild("Head").EnsureComponent<OnTouch>().onTouch = new OnTouch.OnTouchEvent();
			gameObject.SearchChild("Head").EnsureComponent<OnTouch>().onTouch.AddListener(OnTouch);
			playerDeathCinematic = gameObject.AddComponent<PlayerCinematicController>();
			playerDeathCinematic.animatedTransform = gameObject.SearchChild("PlayerCam").transform;
			playerDeathCinematic.animator = creature.GetAnimator();
			playerDeathCinematic.animParamReceivers = new GameObject[0];
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
					if (!playerDeathCinematic.IsCinematicModeActive())
					{
						Player player = target.GetComponent<Player>();
						if (player != null)
						{
							if (Time.time > timeCinematicAgain && player.CanBeAttacked() && player.liveMixin.IsAlive() && !player.cinematicModeActive)
							{
								float num = DamageSystem.CalculateDamage(biteDamage, DamageType.Normal, player.gameObject, null);
								if (player.liveMixin.health - num <= 0f)
								{
									Invoke("KillPlayer", 2.5f);
									playerDeathCinematic.StartCinematicMode(player);
									attackSource.clip = cinematicClipPool.GetRandomClip();
									attackSource.Play();
									timeCinematicAgain = Time.time + 5f;
									timeLastBite = Time.time;
									return;
								}
							}
						}
						LiveMixin liveMixin = target.GetComponent<LiveMixin>();
						if (liveMixin == null) return;
						if (!liveMixin.IsAlive())
						{
							return;
						}
						if(liveMixin == Player.main.liveMixin)
						{
							if(!player.CanBeAttacked())
							{
								return;
							}
						}
						if (!CanAttackTargetFromPosition(target))
						{
							return;
						}
						liveMixin.TakeDamage(GetBiteDamage(target));
						attackSource.clip = biteClipPool.GetRandomClip();
						attackSource.Play();
						timeLastBite = Time.time;
						creature.GetAnimator().SetTrigger("bite");
						component.Aggression.Value -= 0.15f;
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
		private void KillPlayer()
		{
			Player.main.liveMixin.Kill(DamageType.Normal);
			playerDeathCinematic.OnPlayerCinematicModeEnd();
		}
    }
}
