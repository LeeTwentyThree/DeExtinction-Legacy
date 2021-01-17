using DeExtinctionMod.Mono;
using System.Collections.Generic;
using UnityEngine;
using UWE;
using ECCLibrary;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class GrandGliderPrefab : CreatureAsset
    {
        const float kMyVelocity = 4f;

        public GrandGliderPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {

        }

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Medium;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(30f, 10f, 30f), kMyVelocity, 3f, 0.5f);

        public override float TurnSpeed => 0.5f;

        public override EcoTargetType EcoTargetType => EcoTargetType.MediumFish;

        public override TechType CreatureTraitsReference => TechType.Spadefish;

        public override AnimationCurve SizeDistribution => new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.25f), new Keyframe(1f, 1f) });

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.9f, true, 2f);

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 4f, "Lifeforms/Fauna/LargeHerbivores", new string[] { "Lifeforms", "Fauna", "LargeHerbivores" }, QPatch.assetBundle.LoadAsset<Sprite>("GrandGlider_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("GrandGlider_Ency"));

        public override Vector2int SizeInInventory => new Vector2int(3, 3);

        public override BehaviourType BehaviourType => BehaviourType.MediumFish;

        public override string GetEncyDesc => "A medium sized herbivore with an unusual ocular arrangement.\n\n1. Behavior:\nThis animal often forms large schools within the water column for safety, though this may have the unintended effect of attracting the attention of larger predators.\n\n2. Eyes:\nTwo pairs of three eyes give the Grand Glider dual trioptical vision, used to sense faint movements within the surrounding water.\n\nAssessment: Edible, but too large to catch efficiently.";

        public override string GetEncyTitle => "Grand Glider";

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 90f;
        }

        public override SwimInSchoolData SwimInSchoolSettings => new SwimInSchoolData(0.8f, kMyVelocity * 2f, 1.5f, 2f, 50f, 1f, 0f);

        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.025f, 0.15f, 0.5f, 1f, false);

        public override float MaxVelocityForSpeedParameter => kMyVelocity * 2f;

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            CreateTrail(prefab.SearchChild("Spine1"), new Transform[] { prefab.SearchChild("Spine2").transform, prefab.SearchChild("Spine3").transform, prefab.SearchChild("Spine4").transform, prefab.SearchChild("Spine5").transform, prefab.SearchChild("Spine6").transform, prefab.SearchChild("Spine7").transform, prefab.SearchChild("Spine8").transform, prefab.SearchChild("Spine9").transform }, components, 0.5f);
        }

        public override float BioReactorCharge => 830f;

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GrandReef_OpenDeep_CreatureOnly,
                count = 12,
                probability = 0.015f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GrandReef_OpenShallow_CreatureOnly,
                count = 12,
                probability = 0.015f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Mountains_OpenDeep_CreatureOnly,
                count = 12,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Mountains_OpenShallow_CreatureOnly,
                count = 12,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.CragField_OpenDeep_CreatureOnly,
                count = 12,
                probability = 0.02f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.CragField_OpenShallow_CreatureOnly,
                count = 12,
                probability = 0.02f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Mesas_Open,
                count = 12,
                probability = 0.04f
            }
        };

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            classId = ClassID,
            slotType = EntitySlot.Type.Creature,
            techType = TechType,
            localScale = Vector3.one
        };

        public override float Mass => 15f;

    }
}
