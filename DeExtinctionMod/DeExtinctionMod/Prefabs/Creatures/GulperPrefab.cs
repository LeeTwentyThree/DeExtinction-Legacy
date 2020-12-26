using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeExtinctionMod.Mono;
using ECCLibrary;
using UnityEngine;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class GulperPrefab : CreatureAsset
    {
        public GulperPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override TechType CreatureTraitsReference => TechType.ReaperLeviathan;

        public override BehaviourType BehaviourType => BehaviourType.Leviathan;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Far;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(30f, 10f, 30f), 14f, 2f, 0.1f);

        public override EcoTargetType EcoTargetType => EcoTargetType.Leviathan;

        public override bool EnableAggression => true;

        public override SmallVehicleAggressivenessSettings AggressivenessToSmallVehicles => new SmallVehicleAggressivenessSettings(0.25f, 50f);

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.5f, 28f, 12f, 20f, 3f, 16f);

        public override float MaxVelocityForSpeedParameter => 28f;

        public override float EyeFov => 0.75f;

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.3f, true, 10f);

        public override RoarAbilityData RoarAbilitySettings => new RoarAbilityData(true, 5f, 100f, "GulperRoar", "roar", 0.3f, 8f, 20f);

        public override BehaviourLODLevelsStruct BehaviourLODSettings => new BehaviourLODLevelsStruct(30f, 100f, 200f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.4f, 50f);

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            GameObject spine2 = prefab.SearchChild("Spine2");
            CreateTrail(spine2, new Transform[] { spine2.SearchChild("Spine3Phys").transform, spine2.SearchChild("Spine3").transform, spine2.SearchChild("Spine4Phys").transform, spine2.SearchChild("Spine4").transform, spine2.SearchChild("Spine5Phys").transform, spine2.SearchChild("Spine5").transform, spine2.SearchChild("Spine6Phys").transform, spine2.SearchChild("Spine6").transform, spine2.SearchChild("Spine7Phys").transform, spine2.SearchChild("Spine7").transform, spine2.SearchChild("Spine8Phys").transform, spine2.SearchChild("Spine8").transform, spine2.SearchChild("Spine9Phys").transform, spine2.SearchChild("Spine9").transform }, components, 3f);
            MakeAggressiveTo(35f, 2, EcoTargetType.Shark, 0f, 3f);
            GameObject mouth = prefab.SearchChild("Mouth");
            GulperBehaviour gulperBehaviour = prefab.AddComponent<GulperBehaviour>();
            gulperBehaviour.creature = components.creature;

            GulperMeleeAttack meleeAttack = prefab.AddComponent<GulperMeleeAttack>();
            meleeAttack.mouth = mouth;
            meleeAttack.canBeFed = false;
            meleeAttack.biteInterval = 1f;
            meleeAttack.biteDamage = 90f;
            meleeAttack.eatHungerDecrement = 0.05f;
            meleeAttack.eatHappyIncrement = 0.1f;
            meleeAttack.biteAggressionDecrement = 0.02f;
            meleeAttack.biteAggressionThreshold = 0.1f;
            meleeAttack.lastTarget = components.lastTarget;
            meleeAttack.creature = components.creature;
            meleeAttack.liveMixin = components.liveMixin;
            meleeAttack.animator = components.creature.GetAnimator();
            OnTouch onTouch = mouth.AddComponent<OnTouch>();
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 5000f;
        }
    }
}
