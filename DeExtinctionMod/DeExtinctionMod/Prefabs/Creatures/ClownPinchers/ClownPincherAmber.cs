using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class ClownPincherAmber : ClownPincherPrefab
    {
        public ClownPincherAmber(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {

        }

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 2f, "Lifeforms/Fauna/Scavengers", new string[] { "Lifeforms", "Fauna", "Scavengers" }, QPatch.assetBundle.LoadAsset<Sprite>("ACP_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("ACP_Ency"));

        public override string GetEncyDesc => "A small colorful scavenger found amongst large organic formations.\n\nColoration:\nColoration appears to mimic the surrounding coral, with white stripes to break up the pattern.\n\nBehavior:\nA social species, the Amber Clown Pincher can be found forming loose shoals while foraging on organic matter.\n\nSpecimen shows high genetic diversity suggesting many extant, closely related species that frequently mate.\n\nAssessment: Edible";

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.MushroomForest_Grass,
                probability = 0.4f,
                count = 4
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.MushroomForest_Sand,
                probability = 0.4f,
                count = 4
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.MushroomForest_GiantTreeExterior,
                probability = 0.4f,
                count = 4
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_OpenShallow_CreatureOnly,
                probability = 0.15f,
                count = 4
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_IslandTop,
                probability = 0.15f,
                count = 4
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SparseReef_OpenDeep_CreatureOnly,
                probability = 0.1f,
                count = 4
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SparseReef_OpenShallow_CreatureOnly,
                probability = 0.05f,
                count = 4
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SparseReef_Spike,
                probability = 0.2f,
                count = 4
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.SparseReef_Coral,
                probability = 0.2f,
                count = 4
            }
        };
    }
}
