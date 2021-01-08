using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECCLibrary;
using UnityEngine;
using DeExtinctionMod.Mono;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class TrianglefishPrefab : CreatureAsset
    {
        public TrianglefishPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {

        }

        public override TechType CreatureTraitsReference => TechType.Peeper;

        public override BehaviourType BehaviourType => BehaviourType.SmallFish;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Near;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(1f, 5f, 1f), 4f, 0.5f, 0.1f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 15f);

        public override EcoTargetType EcoTargetType => EcoTargetType.SmallFish;

        public override bool Pickupable => true;

        public override HeldFishData ViewModelSettings => new HeldFishData(TechType.GarryFish, "WorldModel", "ViewModel");

        public override EatableData EatableSettings => new EatableData(true, 7f, -5f, false);

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.4f, true, 4f);

        public override string GetEncyDesc => "Small herbivore generally found in shallow waters.\n\nEye Stalks:\nTwo eyes on stalks give the Trianglefish a mobile field of vision.\n\nBehavior: A skittish animal, Trianglefish will generally try to stay out of sight of larger animals, and dart away when confronted.\n\nAssessment: Edible";

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 2f, "Lifeforms/Fauna/SmallHerbivores", new string[] { "Lifeforms", "Fauna", "SmallHerbviores" }, QPatch.assetBundle.LoadAsset<Sprite>("TriangleFish_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("Trianglefish_Ency"));

        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.1f, 0.7f, 0.9f, 1f, true);

        public override float Mass => 3f;

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            var fleeFromPredators = prefab.AddComponent<SwimAwayFromPredators>();
            fleeFromPredators.swimVelocity = 8f;
            fleeFromPredators.maxReactDistance = 6f;
            fleeFromPredators.actionLength = 2f;
            fleeFromPredators.evaluatePriority = 0.25f;

            var sleep = prefab.AddComponent<SleepAtNight>();
            sleep.evaluatePriority = 0.3f;
            sleep.swimRadius = new Vector3(2f, 3f, 2f);
            sleep.sleepSwimVelocity = 0.5f;
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 30f;
        }

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SafeShallows_Grass,
                count = 3,
                probability = 0.4f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SafeShallows_SandFlat,
                count = 3,
                probability = 0.3f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SparseReef_Coral,
                count = 3,
                probability = 0.2f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SparseReef_OpenDeep_CreatureOnly,
                count = 3,
                probability = 0.2f
            }
        };
    }
}
