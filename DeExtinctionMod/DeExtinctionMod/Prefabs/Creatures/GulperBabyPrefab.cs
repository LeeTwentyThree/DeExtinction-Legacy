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
    public class GulperBabyPrefab : CreatureAsset
    {
        public GulperBabyPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override BehaviourType BehaviourType => BehaviourType.Leviathan;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Medium;

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 3f, "Lifeforms/Fauna/Leviathans", new string[] { "Lifeforms", "Fauna", "Leviathans" }, QPatch.assetBundle.LoadAsset<Sprite>("Gulper_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("GulperBaby_Ency"));

        public override string GetEncyDesc => "A very young offspring of an apex predator.\n\n1. Jaws:\nThe four jaws of this leviathan are not yet developed to maintain a fulfilling diet. At this age it is impossible to gulp its prey, which is common behavior found in adult specimens, but it can still deal a devastating blow with its jaws if it has the intent of doing so.\n\n2. Behavior:\nThis baby is extremely aggressive, but also shows signs of intelligence. It appears to be very curious, and can even be seen playing with other fish that are too large to be eaten.\n\nAssessment: Minor threat. May attack if provoked, and will reduce local fish populations.";

        public override string GetEncyTitle => "Gulper Leviathan Baby";

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(20f, 10f, 20f), 6f, 1.5f, 0.1f);

        public override float TurnSpeedHorizontal => 0.4f;

        public override EcoTargetType EcoTargetType => EcoTargetType.Shark;

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.5f, 12f, 18f, 20f, 10f, 16f);

        public override float MaxVelocityForSpeedParameter => 10f;

        public override float EyeFov => 0.75f;

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.7f, false, 7f);

        public override BehaviourLODLevelsStruct BehaviourLODSettings => new BehaviourLODLevelsStruct(35f, 60f, 120f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.4f, 30f);

        public override UBERMaterialProperties MaterialSettings => new UBERMaterialProperties(10f, 12f, 3f);

        public override float Mass => 300f;

        public override RespawnData RespawnSettings => new RespawnData(false);

        public override float BioReactorCharge => 800f;

        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.02f, 0.5f, 1f, 1.25f, false);

        public override Vector2int SizeInInventory => new Vector2int(4, 4);

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            GameObject spine2 = prefab.SearchChild("Spine2");
            CreateTrail(spine2, new Transform[] { spine2.SearchChild("Spine3").transform, spine2.SearchChild("Spine4").transform, spine2.SearchChild("Spine5").transform, spine2.SearchChild("Spine6").transform, spine2.SearchChild("Spine7").transform, spine2.SearchChild("Spine8").transform, spine2.SearchChild("Spine9").transform }, components, 3f);
            MakeAggressiveTo(30f, 2, EcoTargetType.SmallFish, 0.2f, 1f);;
            MakeAggressiveTo(40f, 2, EcoTargetType.Shark, 0f, 1f).ignoreSameKind = false;
            MakeAggressiveTo(35f, 2, EcoTargetType.SubDecoy, 0f, 2f);
            MakeAggressiveTo(35f, 1, EcoTargetType.Leviathan, 0f, 1f);
            GameObject mouth = prefab.SearchChild("Mouth");
            GameObject lClawTrigger = prefab.SearchChild("LClaw");
            GameObject rClawTrigger = prefab.SearchChild("RClaw");

            GulperMeleeAttack_Mouth meleeAttack = prefab.AddComponent<GulperMeleeAttack_Mouth>();
            meleeAttack.isBaby = true;
            meleeAttack.mouth = mouth;
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

            var avoidObstacels = prefab.GetComponent<AvoidObstacles>();
            avoidObstacels.avoidanceIterations = 15;
            avoidObstacels.scanInterval = 0.5f;

            mouth.AddComponent<OnTouch>();
            lClawTrigger.AddComponent<OnTouch>();
            rClawTrigger.AddComponent<OnTouch>();
            AddClawAttack("LClaw", "swipeL", components);
            AddClawAttack("RClaw", "swipeR", components);

            components.locomotion.driftFactor = 0.4f;
            components.locomotion.maxAcceleration = 10f;
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
            meleeAttack.isBaby = true;
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 5000f;
        }
    }
}
