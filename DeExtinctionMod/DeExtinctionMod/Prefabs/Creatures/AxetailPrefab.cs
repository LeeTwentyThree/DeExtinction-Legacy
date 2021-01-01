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
    public class AxetailPrefab : CreatureAsset
    {
        public AxetailPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override TechType CreatureTraitsReference => TechType.Spadefish;

        public override BehaviourType BehaviourType => BehaviourType.SmallFish;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Near;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(20f, 20f, 20f), 3.5f, 0.5f, 0.1f);

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.3f, true, 5f);

        public override EcoTargetType EcoTargetType => EcoTargetType.SmallFish;

        public override EatableData EatableSettings => new EatableData(true, 12f, -7f, false);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 12f);

        public override float MaxVelocityForSpeedParameter => 7f;

        public override string GetEncyDesc => "A solitary herbivore with a derived body plan compared to surrounding lifeforms.\n\nBody:\nTwo pairs of eyes protrude from the frontwards dorsal and ventral sides of the animal’s body, which is otherwise largely conical save for a fan-shaped tail.This body plan is unusual even compared to surrounding fauna, suggesting adaptations for environments no longer present in the Axetail’s range.\n\nDefense:\nIn addition to quick bursts of speed the Axetail is armored with tough scales to deter potential predators.\n\nAssessment: Edible beyond the rough exterior.";

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 3f, "Lifeforms/Fauna/SmallHerbivores", new string[] { "Lifeforms", "Fauna", "SmallHerbivores" }, QPatch.assetBundle.LoadAsset<Sprite>("Axetail_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("Axetail_Ency"));

        public override HeldFishData ViewModelSettings => new HeldFishData(TechType.GarryFish, "Axetail", "AxetailViewModel");

        public override float Mass => 30f;

        public override float TurnSpeed => 0.75f;

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            var fleeOnDamage = prefab.AddComponent<FleeOnDamage>();
            fleeOnDamage.damageThreshold = 10f;
            fleeOnDamage.evaluatePriority = 0.5f;
            fleeOnDamage.swimVelocity = 7f;

            CreateTrail(prefab.SearchChild("Spine1", ECCStringComparison.Contains), new Transform[] { prefab.SearchChild("Spine2", ECCStringComparison.Contains).transform, prefab.SearchChild("Spine3", ECCStringComparison.Contains).transform , prefab.SearchChild("Spine4", ECCStringComparison.Contains).transform , prefab.SearchChild("Spine5", ECCStringComparison.Contains).transform , prefab.SearchChild("Spine6", ECCStringComparison.Contains).transform }, components, 0.5f, 0.5f);
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 30f;
        }

        public override bool Pickupable => true;

        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.1f, 0.7f, 0.9f, 1f, true);

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GrandReef_OpenDeep_CreatureOnly,
                probability = 0.4f,
                count = 1
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.KooshZone_OpenDeep_CreatureOnly,
                probability = 0.4f,
                count = 1
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.KooshZone_Grass,
                probability = 0.4f,
                count = 1
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SeaTreaderPath_OpenDeep_CreatureOnly,
                probability = 0.4f,
                count = 1
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SeaTreaderPath_Path,
                probability = 0.4f,
                count = 1
            },
        };
    }
}
