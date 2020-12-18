using DeExtinctionMod.AssetClasses;
using DeExtinctionMod.Mono;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class GrandGliderPrefab : CreatureAsset
    {
        const float myVelocity = 3f;

        public GrandGliderPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {

        }

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Medium;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(30f, 10f, 30f), myVelocity, 3f, 0.5f);

        public override EcoTargetType EcoTargetType => EcoTargetType.MediumFish;

        public override TechType CreatureTraitsReference => TechType.Spadefish;

        public override AnimationCurve SizeDistribution => new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.5f), new Keyframe(1f, 1f) });

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.9f, true, 2f);

        public override ScannableCreatureData ScannableSettings => new ScannableCreatureData(true, 4f, "Lifeforms/Fauna/LargeHerbivores", new string[] { "Lifeforms", "Fauna", "LargeHerbivores" }, null, null);

        public override Vector2int SizeInInventory => new Vector2int(3, 3);

        public override string GetEncyTitle => "Grand Glider";

        public override string GetEncyDesc => "A medium sized herbivore with an unusual ocular arrangement.\n\n1. Behavior:\nThis animal often forms large schools within the water column for safety, though this may have the unintended effect of attracting the attention of larger predators.\n\n2. Eyes:\nTwo pairs of three eyes give the Grand Glider dual trioptical vision, used to sense faint movements within the surrounding water.\n\nAssessment: Edible, but too large to catch efficiently.";

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 90f;
        }

        public override GameObject GetGameObject()
        {
            if (prefab == null)
            {
                SetupPrefab(out CreatureComponents<GrandGlider> components);

                SwimInSchool swimInSchool = prefab.AddComponent<SwimInSchool>();
                swimInSchool.priorityMultiplier = Helpers.Curve_Flat();
                swimInSchool.evaluatePriority = 0.7f;
                swimInSchool.swimInterval = 1f;
                swimInSchool.swimVelocity = myVelocity * 1.2f;
                swimInSchool.schoolSize = 4f;
                Helpers.SetPrivateField(typeof(SwimInSchool), swimInSchool, "percentFindLeaderRespond", 1f);
                Helpers.SetPrivateField(typeof(SwimInSchool), swimInSchool, "chanceLoseLeader", 0f);
                Helpers.SetPrivateField(typeof(SwimInSchool), swimInSchool, "kBreakDistance", 40f);
                creatureActions.Add(swimInSchool);

                CompletePrefab(components);
            }
            return prefab;
        }

        protected override void PostPatch()
        {
            Helpers.PatchBehaviorType(TechType, BehaviourType.MediumFish);
        }

        public override float BioReactorCharge => 400f;

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GrandReef_OpenDeep_CreatureOnly,
                count = 7,
                probability = 0.09f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GrandReef_OpenShallow_CreatureOnly,
                count = 4,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Mountains_OpenDeep_CreatureOnly,
                count = 10,
                probability = 0.05f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Mountains_OpenShallow_CreatureOnly,
                count = 10,
                probability = 0.05f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.CragField_OpenDeep_CreatureOnly,
                count = 10,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Mesas_Open,
                count = 10,
                probability = 0.1f
            }
        };

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            classId = ClassID,
            slotType = EntitySlot.Type.Creature,
            techType = TechType
        };

        public override float Mass => 15f;

    }
}
