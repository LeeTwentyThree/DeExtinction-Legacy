using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class ClownPincherEmerald : ClownPincherPrefab
    {
        public ClownPincherEmerald(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {

        }

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 2f, "Lifeforms/Fauna/Scavengers", new string[] { "Lifeforms", "Fauna", "Scavengers" }, QPatch.assetBundle.LoadAsset<Sprite>("ECP_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("ECP_Ency"));

        public override string GetEncyDesc => "A small colorful scavenger found amongst large plantlife.\n\nColoration:\nColoration appears to mimic the surrounding flora, with white stripes to break up the pattern.\n\nBehavior:\nA social species, the Emerald Clown Pincher can be found forming loose shoals while foraging on seed clusters and deceased creatures.\n\nSpecimen shows high genetic diversity suggesting many extant, closely related species that frequently mate.\n\nAssessment: Edible";

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
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
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Kelp_DenseVine,
                probability = 0.5f,
                count = 4
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Kelp_GrassDense,
                probability = 0.6f,
                count = 4
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Kelp_GrassSparse,
                probability = 0.5f,
                count = 4
            }
        };
    }
}
