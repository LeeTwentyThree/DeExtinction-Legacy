using ECCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class ClownPincherPrefab : CreatureAsset
    {
        public ClownPincherPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {

        }

        public override BehaviourType BehaviourType => BehaviourType.SmallFish;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Medium;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(10f, 5f, 10f), 2f, 1.5f, 0.5f);

        public override EcoTargetType EcoTargetType => EcoTargetType.SmallFish;

        public override TechType CreatureTraitsReference => TechType.Hoverfish;

        public override EatableData EatableSettings => new EatableData(true, 16f, -3f, false);

        public override bool Pickupable => true;

        public override float BioReactorCharge => 300f;

        public override SwimInSchoolData SwimInSchoolSettings => new SwimInSchoolData(0.6f, 3f, 1f, 2f, 20f, 0.5f, 0.1f);

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 30f;
            liveMixinData.explodeOnDestroy = true;
            liveMixinData.destroyOnDeath = true;
        }

        public override GameObject GetGameObject()
        {
            if (prefab == null)
            {
                SetupPrefab(out CreatureComponents<Creature> components);

                CompletePrefab(components);
            }
            return prefab;
        }
    }
}
