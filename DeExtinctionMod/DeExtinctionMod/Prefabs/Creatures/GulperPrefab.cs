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

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 15f, "Lifeforms/Fauna/Leviathans", new string[] { "Lifeforms", "Fauna", "Leviathans" }, QPatch.assetBundle.LoadAsset<Sprite>("Gulper_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("Gulper_Ency"));

        public override string GetEncyDesc => "This vast animal is at the top of the local food chain, and has been designated leviathan class.\n\n1. Jaws:\nSituated within the maw of the leviathan are four mobile jaws capable of launching forward and dragging prey in towards the mouth.\n\n2. Limbs:\nA pair of fins modified into muscular limbs serve an unknown purpose. Possibilities include restraint of other animals, propulsion off of the seabed, and slashing of potential prey or threats. Whatever the case, avoidance of limbs is highly recommended.\n\n3. Behavior:\nConsumes anything it can fit into its mouth. Will attempt to crush prey if too large or tough.\n\nHunts using electromagnetic signals, may be drawn to wrecked technology.\n\nAssessment: Extreme threat to whatever can fit into its mouth - Avoid in all circumstances";

        public override string GetEncyTitle => "Gulper Leviathan";

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(40f, 10f, 40f), 12f, 1.5f, 0.1f);

        public override EcoTargetType EcoTargetType => EcoTargetType.Leviathan;

        public override bool EnableAggression => true;

        public override SmallVehicleAggressivenessSettings AggressivenessToSmallVehicles => new SmallVehicleAggressivenessSettings(0.2f, 30f);

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.5f, 20f, 18f, 20f, 10f, 16f);

        public override float MaxVelocityForSpeedParameter => 24f;

        public override float EyeFov => 0.75f;

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.7f, true, 30f);

        public override RoarAbilityData RoarAbilitySettings => new RoarAbilityData(true, 10f, 100f, "GulperRoar", "roar", 0f, 10f, 20f);

        public override BehaviourLODLevelsStruct BehaviourLODSettings => new BehaviourLODLevelsStruct(30f, 100f, 200f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.4f, 40f);

        public override UBERMaterialProperties MaterialSettings => new UBERMaterialProperties(10f, 12f, 3f);

        public override float Mass => 2000f;

        public override float TurnSpeed => 0.4f;

        public override bool ScannerRoomScannable => true;

        public override RespawnData RespawnSettings => new RespawnData(false);

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            GameObject spine2 = prefab.SearchChild("Spine2");
            CreateTrail(spine2, new Transform[] { spine2.SearchChild("Spine3").transform, spine2.SearchChild("Spine4").transform, spine2.SearchChild("Spine5").transform, spine2.SearchChild("Spine6").transform, spine2.SearchChild("Spine7").transform, spine2.SearchChild("Spine8").transform, spine2.SearchChild("Spine9").transform }, components, 3f);
            MakeAggressiveTo(30f, 2, EcoTargetType.Shark, 0f, 1.5f);
            MakeAggressiveTo(35f, 2, EcoTargetType.SubDecoy, 0f, 2f);
            MakeAggressiveTo(15f, 1, EcoTargetType.MediumFish, 0f, 1.5f);
            GameObject mouth = prefab.SearchChild("Mouth");
            GameObject lClawTrigger = prefab.SearchChild("LClaw");
            GameObject rClawTrigger = prefab.SearchChild("RClaw");

            GulperBehaviour gulperBehaviour = prefab.AddComponent<GulperBehaviour>();
            gulperBehaviour.creature = components.creature;

            GulperMeleeAttack_Mouth meleeAttack = prefab.AddComponent<GulperMeleeAttack_Mouth>();
            meleeAttack.mouth = mouth;
            meleeAttack.canBeFed = false;
            meleeAttack.biteInterval = 1f;
            meleeAttack.biteDamage = 100f;
            meleeAttack.eatHungerDecrement = 0.05f;
            meleeAttack.eatHappyIncrement = 0.1f;
            meleeAttack.biteAggressionDecrement = 0.02f;
            meleeAttack.biteAggressionThreshold = 0.1f;
            meleeAttack.lastTarget = components.lastTarget;
            meleeAttack.creature = components.creature;
            meleeAttack.liveMixin = components.liveMixin;
            meleeAttack.animator = components.creature.GetAnimator();

            var avoidObstacels = prefab.GetComponent<AvoidObstacles>();
            avoidObstacels.avoidanceIterations = 20;
            avoidObstacels.scanInterval = 0.5f;

            mouth.AddComponent<OnTouch>();
            lClawTrigger.AddComponent<OnTouch>();
            rClawTrigger.AddComponent<OnTouch>();
            AddClawAttack("LClaw", "swipeL", components);
            AddClawAttack("RClaw", "swipeR", components);

            AttackCyclops actionAtkCyclops = prefab.AddComponent<AttackCyclops>();
            actionAtkCyclops.swimVelocity = 62f;
            actionAtkCyclops.aggressiveToNoise = new CreatureTrait(0f, 0.01f);
            actionAtkCyclops.evaluatePriority = 0.6f;
            actionAtkCyclops.priorityMultiplier = ECCHelpers.Curve_Flat();
            actionAtkCyclops.maxDistToLeash = 90f;
            actionAtkCyclops.attackAggressionThreshold = 0.4f;

            components.locomotion.driftFactor = 0.8f;
            components.locomotion.maxAcceleration = 18f;
            }

        void AddClawAttack(string triggerName, string animationName, CreatureComponents components)
        {
            GulperMeleeAttack_Claw meleeAttack = prefab.AddComponent<GulperMeleeAttack_Claw>();
            meleeAttack.mouth = prefab.SearchChild(triggerName);
            meleeAttack.canBeFed = false;
            meleeAttack.biteInterval = 1f;
            meleeAttack.biteDamage = 50f;
            meleeAttack.eatHungerDecrement = 0.05f;
            meleeAttack.eatHappyIncrement = 0.1f;
            meleeAttack.biteAggressionDecrement = 0.02f;
            meleeAttack.biteAggressionThreshold = 0.1f;
            meleeAttack.lastTarget = components.lastTarget;
            meleeAttack.creature = components.creature;
            meleeAttack.liveMixin = components.liveMixin;
            meleeAttack.animator = components.creature.GetAnimator();
            meleeAttack.colliderName = triggerName;
            meleeAttack.animationTriggerName = animationName;
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 5000f;
        }
    }
}
