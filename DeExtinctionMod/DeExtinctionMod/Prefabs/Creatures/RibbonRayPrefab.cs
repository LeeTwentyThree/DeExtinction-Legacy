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
    public class RibbonRayPrefab : CreatureAsset
    {
        public RibbonRayPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {

        }

        public override TechType CreatureTraitsReference => TechType.GarryFish;

        public override BehaviourType BehaviourType => BehaviourType.SmallFish;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Near;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(15f, 1f, 15f), 3f, 3f, 0.1f);

        public override SwimInSchoolData SwimInSchoolSettings => new SwimInSchoolData(0.15f, 4f, 2f, 3f, 20f, 0.8f, 0.04f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 20f);

        public override EcoTargetType EcoTargetType => EcoTargetType.SmallFish;

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.3f, true, 4f);

        public override bool Pickupable => true;

        public override HeldFishData ViewModelSettings => new HeldFishData(TechType.Peeper, "WorldModel", "ViewModel");

        public override EatableData EatableSettings => new EatableData(true, 12f, -6f, false);

        public override AnimationCurve SizeDistribution => new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.8f), new Keyframe(1f, 1f) });

        public override float BioReactorCharge => 400f;

        public override Vector2int SizeInInventory => new Vector2int(2, 1);

        public override float Mass => 3f;

        public override float TurnSpeed => 0.5f;

        public override string GetEncyDesc => "Medium sized herbivore found in a diverse biome.\n\nBanded Coloration:\nWhile some of the Ribbon Ray’s coloration is similar to the surrounding environment, this pattern seems to serve the purpose of display more than anything else.\n\nBehavior:\nFound commonly taking shelter under the massive coral-like lifeforms towering over the surrounding biome. Ribbon Rays are not particularly skittish, but are still cautious in open areas.\n\nAssessment: Edible";

        public override string GetEncyTitle => "Ribbon Ray";

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 2f, "Lifeforms/Fauna/SmallHerbivores", new string[] { "Lifeforms", "Fauna", "SmallHerbivores" }, QPatch.assetBundle.LoadAsset<Sprite>("RibbonRay_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("RibbonRay_Ency"));

        public override float MaxVelocityForSpeedParameter => 6f;

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            var sleep = prefab.AddComponent<SleepAtNight>();
            sleep.evaluatePriority = 0.9f;
            sleep.swimRadius = new Vector3(15f, 0f, 15f);

            CreateTrail(prefab.SearchChild("LTail1Spade"), components, 1f);
            CreateTrail(prefab.SearchChild("RTail1Spade"), components, 1f);
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 30f;
        }

        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.1f, 0.7f, 0.9f, 1f, true);

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.MushroomForest_GiantTreeExterior,
                probability = 2f,
                count = 2
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.MushroomForest_GiantTreeExteriorBase,
                probability = 0.9f,
                count = 2
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.MushroomForest_CaveSand,
                probability = 1f,
                count = 2
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.MushroomForest_Grass,
                probability = 1f,
                count = 2
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.MushroomForest_MushroomTreeBase,
                probability = 1f,
                count = 2
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.MushroomForest_Sand,
                probability = 1f,
                count = 2
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SparseReef_Coral,
                count = 3,
                probability = 0.2f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SafeShallows_ShellTunnelHuge,
                count = 2,
                probability = 3f
            }
        };
    }
}
