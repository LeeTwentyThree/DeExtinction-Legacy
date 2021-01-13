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
    public class TwisteelPrefab : CreatureAsset
    {
        public TwisteelPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override TechType CreatureTraitsReference => TechType.BoneShark;

        public override BehaviourType BehaviourType => BehaviourType.Shark;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Medium;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(20f, 5f, 20f), 6f, 0.5f, 0.1f);

        public override EcoTargetType EcoTargetType => EcoTargetType.Shark;

        public override SmallVehicleAggressivenessSettings AggressivenessToSmallVehicles => new SmallVehicleAggressivenessSettings(0.5f, 25f);

        public override bool EnableAggression => true;

        public override float MaxVelocityForSpeedParameter => 12f;

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.31f, true, 8f);

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.3f, 15f, 3f, 6f, 5f, 15f);

        public override float BioReactorCharge => 630f;

        public override Vector2int SizeInInventory => new Vector2int(3, 3);

        public override float Mass => 200f;

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 30f);

        public override float TurnSpeed => 0.75f;

        public override float EyeFov => 0.7f;

        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.01f, 0.4f, 0.6f, 1.5f, false);

        public override BehaviourLODLevelsStruct BehaviourLODSettings => new BehaviourLODLevelsStruct(20f, 100f, 150f);

        public override AnimationCurve SizeDistribution => new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.5f), new Keyframe(1f, 1f) });

        public override RoarAbilityData RoarAbilitySettings => new RoarAbilityData(true, 1f, 20f, "TwisteelIdle", "roar", 0.12f, 7f, 20f);

        public override string GetEncyDesc => "A large eel-like predator found within a deep canyon.\n\n1. Body:\nA long and flexible body allows the Twisteel to snake around the environment with a low profile while hunting for prey. The Twisteel appears to share many characteristics with other shark-like creatures found on Planet 4546B.\n\n2. Jaws:\nDistantly related to other lifeforms on the planet possessing a quad-jaw arrangement, the lateral pair of jaws have been reduced to a vestigial point. The remaining jaws reach lengths of up to 3m, and are filled with rows of large teeth to trap relatively large prey items.\n\nAssessment: Avoid";

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 7f, "Lifeforms/Fauna/Carnivores", new string[] { "Lifeforms", "Fauna", "Carnivores" }, QPatch.assetBundle.LoadAsset<Sprite>("Twisteel_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("Twisteel_Ency"));

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            GameObject mouth = prefab.SearchChild("Head");
            TwisteelMeleeAttack meleeAttack = prefab.AddComponent<TwisteelMeleeAttack>();
            meleeAttack.mouth = mouth;
            meleeAttack.biteInterval = 2.5f;
            meleeAttack.biteDamage = 30f;
            meleeAttack.eatHungerDecrement = 0.05f;
            meleeAttack.eatHappyIncrement = 0.1f;
            meleeAttack.biteAggressionDecrement = 0.02f;
            meleeAttack.biteAggressionThreshold = 0.1f;
            meleeAttack.lastTarget = components.lastTarget;
            meleeAttack.creature = components.creature;
            meleeAttack.liveMixin = components.liveMixin;
            meleeAttack.animator = components.creature.GetAnimator();

            GameObject trailParent = prefab.SearchChild("Spine1");
            Transform[] trails = new Transform[] { prefab.SearchChild("Spine2").transform, prefab.SearchChild("Spine3").transform, prefab.SearchChild("Spine4").transform, prefab.SearchChild("Spine5").transform, prefab.SearchChild("Spine6").transform, prefab.SearchChild("Spine7").transform, prefab.SearchChild("Spine8").transform, prefab.SearchChild("Spine9").transform, prefab.SearchChild("Spine10").transform, prefab.SearchChild("Spine11").transform, prefab.SearchChild("Spine12").transform, prefab.SearchChild("Spine13").transform, prefab.SearchChild("Spine14").transform, prefab.SearchChild("Spine15").transform, prefab.SearchChild("Spine16").transform, prefab.SearchChild("Spine17").transform, prefab.SearchChild("Spine18").transform };
            CreateTrail(trailParent, trails, components, 4f, 0.5f);
            MakeAggressiveTo(27f, 2, EcoTargetType.Shark, 0f, 0.67f);
            MakeAggressiveTo(15f, 1, EcoTargetType.MediumFish, 0f, 0.4f);
            MakeAggressiveTo(7f, 1, EcoTargetType.SmallFish, 0.1f, 0.2f);
            MakeAggressiveTo(20f, 1, EcoTargetType.Shiny, 0.1f, 0.2f);
            MakeAggressiveTo(20f, 1, EcoTargetType.CuteFish, 0f, 0.6f);
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 250f;
        }

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome= BiomeType.UnderwaterIslands_OpenShallow_CreatureOnly,
                probability = 0.03f,
                count = 1
            },
            new LootDistributionData.BiomeData()
            {
                biome= BiomeType.UnderwaterIslands_OpenDeep_CreatureOnly,
                probability = 0.03f,
                count = 1
            },
            new LootDistributionData.BiomeData()
            {
                biome= BiomeType.UnderwaterIslands_IslandSides,
                probability = 0.2f,
                count = 1
            },
            new LootDistributionData.BiomeData()
            {
                biome= BiomeType.UnderwaterIslands_ValleyFloor,
                probability = 0.4f,
                count = 1
            },
            new LootDistributionData.BiomeData()
            {
                biome= BiomeType.UnderwaterIslands_ValleyLedge,
                probability = 0.1f,
                count = 1
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BloodKelp_UniqueCreatures,
                probability = 0.04f,
                count = 3
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BloodKelp_TrenchWall,
                probability = 0.6f,
                count = 1
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BloodKelp_WreckCreatures,
                probability = 0.2f,
                count = 1
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.KooshZone_Grass,
                probability = 0.1f,
                count = 1
            }
        };
    }
}
