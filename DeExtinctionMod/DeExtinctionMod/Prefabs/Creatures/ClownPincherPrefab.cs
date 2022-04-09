using DeExtinctionMod.Mono;
using ECCLibrary;
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

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.85f, true, 0.5f);
            
        public override float MaxVelocityForSpeedParameter => 4f;

        public override AnimationCurve SizeDistribution => new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.25f), new Keyframe(1f, 1f) });

        public override float Mass => 4f;

        public override HeldFishData ViewModelSettings => new HeldFishData(TechType.Peeper, "WorldModel", "ViewModel");

        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.01f, 0.8f, 0.9f, 1f, true);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.79f, 15f);

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 30f;
        }

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            var clownPincherBehaviour = prefab.AddComponent<ClownPincherBehaviour>();

            var scavengeBehaviour = prefab.AddComponent<ClownPincherScavengeBehaviour>();
            scavengeBehaviour.clownPincher = clownPincherBehaviour;
            scavengeBehaviour.swimVelocity = 3f;
            scavengeBehaviour.evaluatePriority = 0.8f;
            scavengeBehaviour.priorityWhileScavenging = 0.8f;

            var fleeFromPredators = prefab.AddComponent<SwimAwayFromPredators>();
            fleeFromPredators.swimVelocity = 4f;
            fleeFromPredators.maxReactDistance = 5f;
            fleeFromPredators.actionLength = 3f;
            fleeFromPredators.evaluatePriority = 0.89f;

            var nibble = prefab.SearchChild("Mouth").AddComponent<ClownPincherNibble>();
            nibble.creature = components.creature;
            nibble.clownPincher = clownPincherBehaviour;
            nibble.liveMixin = components.liveMixin;

            var idleSounds = prefab.AddComponent<CreatureRandomSound>();
            idleSounds.asset = QPatch.CreateFMODAsset("ClownPincherIdle");
            idleSounds.minInterval = 30f;
            idleSounds.maxInterval = 120f;

            prefab.AddComponent<SleepAtNight>().evaluatePriority = 0.9f;

            GameObject worldModel = prefab.SearchChild("WorldModel");
            CreateTrail(worldModel.SearchChild("Spine3", ECCStringComparison.StartsWith), new Transform[] {worldModel.SearchChild("Spine4", ECCStringComparison.StartsWith).transform }, components, 1f);

            components.creature.Hunger = new CreatureTrait(0f, -0.01f);
        }
    }
}
