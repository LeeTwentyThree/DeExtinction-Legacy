using ECCLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
	public class ClownPincherNibble : MonoBehaviour
	{
		void Awake()
		{
			rb = GetComponentInParent<Rigidbody>();
			eatingSound = gameObject.AddComponent<AudioSource>();
			eatingSound.volume = ECCHelpers.GetECCVolume();
			eatingSound.clip = QPatch.assetBundle.LoadAsset<AudioClip>("ClownPincherEating");
			eatingSound.spatialBlend = 1f;
		}

		public bool TouchingFood
		{
			get
			{
				return objectEating != null;
			}
		}

		public bool CurrentlyEating
		{
			get
			{
				return objectEating != null && nibbling;
			}
		}

		private void OnTriggerEnter(Collider collider)
		{
			if (creature.Hunger.Value < 0.2f) return;
			GameObject nibbleGameObject = collider.gameObject;
			if (liveMixin.IsAlive() && !frozen)
			{
				timeStartNibbling = Time.time;
				EcoTarget ecoTarget = nibbleGameObject.GetComponentInParent<EcoTarget>();
				LiveMixin lm = nibbleGameObject.GetComponentInParent<LiveMixin>();
				bool isDead = (lm != null && !lm.IsAlive()) || (ecoTarget != null && ecoTarget.type == EcoTargetType.DeadMeat);

				bool isSpecialConsumable = ecoTarget != null && ecoTarget.type == QPatch.clownPincherSpecialEdible;

				if (isDead || isSpecialConsumable)
				{
					objectEating = collider.gameObject;
				}
			}
		}

		void OnTriggerExit(Collider collider)
		{
			if(collider.gameObject == objectEating)
			{
				objectEating = null;
			}
		}

		void Update()
		{
			if (TouchingFood && creature.Hunger.Value > 0.9f)
			{
				StartNibbling();
			}
			if (!frozen && nibbling && objectEating != null && Time.time > timeNextNibble)
			{
				timeNextNibble = Time.time + kNibbleInterval;
				NibbleOnce();
			}
			if (nibbling && clownPincher.creature.Hunger.Value < 0.1f || !TouchingFood)
			{
				StopNibbling();
			}
		}

		public void NibbleOnce()
		{
			if (clownPincher.Sleeping || !clownPincher.creature.liveMixin.IsAlive()) return;
			clownPincher.PlayEatAnimation();
			creature.Hunger.Add(-nibbleHungerDecrement);
			rb.AddForce(-rb.velocity * 0.75f, ForceMode.VelocityChange);
			rb.AddTorque(-rb.angularVelocity * 0.75f, ForceMode.VelocityChange);
		}

		public void StartNibbling()
		{
			nibbling = true;
			eatingSound.Play();
			clownPincher.swimBehaviour.LookAt(objectEating.transform);
			Rigidbody otherRigidbody = objectEating.GetComponentInParent<Rigidbody>();
			if(otherRigidbody != null)
			{
				otherRigidbody.isKinematic = true;
			}
		}
		public void StopNibbling()
		{
			nibbling = false;
			objectEating = null;
			clownPincher.swimBehaviour.LookAt(null);
			eatingSound.Stop();
		}

		public void OnFreeze()
		{
			frozen = true;
		}

		public void OnUnfreeze()
		{
			frozen = false;
		}

		public Creature creature;

		public ClownPincherBehaviour clownPincher;

		public LiveMixin liveMixin;

		public float nibbleHungerDecrement = 0.12f;

		public float nibbleMassToRemove = 1f;

		protected bool frozen;

		private Rigidbody rb;

		float timeNextNibble = 0f;
		public const float kNibbleInterval = 1f;

		public GameObject objectEating;

		AudioSource eatingSound;

		float timeStartNibbling;

		bool nibbling;
	}
}
