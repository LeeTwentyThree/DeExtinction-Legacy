using ECCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class JellySpinnerPrefab : CreatureAsset
    {
        public JellySpinnerPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override TechType CreatureTraitsReference => TechType.Spadefish;

        public override BehaviourType BehaviourType => BehaviourType.SmallFish;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Near;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(10f, 10f, 10f), 2f, 2f, 0.1f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 30f);

        public override EcoTargetType EcoTargetType => EcoTargetType.SmallFish;

        public override EatableData EatableSettings => new EatableData(true, 2f, -2f, false);

        public override bool Pickupable => true;

        public override float MaxVelocityForSpeedParameter => 4f;

        public override AnimationCurve SizeDistribution => new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.8f), new Keyframe(1f, 1f) });

        public override HeldFishData ViewModelSettings => new HeldFishData(TechType.Peeper, "WorldModel", "ViewModel");

        public override float BioReactorCharge => 300f;

        public override ItemSoundsType ItemSounds => ItemSoundsType.Floater;

        public override string GetEncyDesc => "Small bell-shaped herbivore.\n\nBody Plan:\nA large bell surrounds a lengthy flagellum used for propulsion.A tube situated the base of the bell is used for consumption of small pieces of biomatter,\n\nBehavior: Jelly Spinners can generally be found close to the seabed or higher in the water column foraging for food.Primary defense is not being particularly edible.\n\nAssessment: Edible, but not particularly filling.";

        public override string GetEncyTitle => "Jelly Spinner";

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 1f, "Lifeforms/Fauna/SmallHerbivores", new string[] { "Lifeforms", "Fauna", "SmallHerbivores" }, QPatch.assetBundle.LoadAsset<Sprite>("JellySpinner_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("JellySpinner_Ency"));

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            var burst = prefab.AddComponent<VFXBurstModel>();
            burst.displaceTex = CraftData.GetPrefabForTechType(TechType.AcidMushroom).GetComponent<VFXBurstModel>().displaceTex;
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 30f;
        }

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BloodKelp_Grass,
                probability = 0.25f,
                count = 6
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BloodKelp_OpenDeep_CreatureOnly,
                probability = 0.2f,
                count = 6
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BloodKelp_TrenchFloor,
                probability = 0.3f,
                count = 6
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BloodKelp_CaveFloor,
                probability = 0.5f,
                count = 6
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_IslandTop,
                probability = 3f,
                count = 6
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_ValleyFloor,
                probability = 0.1f,
                count = 5
            },
             new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_OpenShallow_CreatureOnly,
                probability = 0.12f,
                count = 6
            },
             new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_OpenDeep_CreatureOnly,
                probability = 0.12f,
                count = 6
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.LostRiverCorridor_Open_CreatureOnly,
                probability = 0.12f,
                count = 6
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.LostRiverJunction_Open_CreatureOnly,
                probability = 0.12f,
                count = 6
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BonesField_Corridor_CreatureOnly,
                probability = 0.12f,
                count = 6
            }, 
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BonesField_Open_Creature,
                probability = 0.12f,
                count = 6
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BonesField_Corridor_CreatureOnly,
                probability = 0.12f,
                count = 6
            },
        };

    }
}
