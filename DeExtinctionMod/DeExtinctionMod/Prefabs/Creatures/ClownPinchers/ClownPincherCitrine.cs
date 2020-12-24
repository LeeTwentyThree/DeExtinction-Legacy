using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class ClownPincherCitrine : ClownPincherPrefab
    {
        public ClownPincherCitrine(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {

        }

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 2f, "Lifeforms/Fauna/Scavengers", new string[] { "Lifeforms", "Fauna", "Scavengers" }, QPatch.assetBundle.LoadAsset<Sprite>("CCP_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("CCP_Ency"));

        public override string GetEncyDesc => "A small colorful scavenger found along a barren seabed.\n\nColoration:\nColoration appears to mimic the surrounding seabed, with white stripes to break up the pattern.\n\nBehavior:\nA social species, the Citrine Clown Pincher can be found forming loose shoals while foraging for organic debris. If they are lucky they will feed on the carcasses of dead sharks and leviathans.\n\nSpecimen shows high genetic diversity suggesting many extant, closely related species that frequently mate.\n\nAssessment: Edible";

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
                biome = BiomeType.Dunes_OpenShallow_CreatureOnly,
                count = 4,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Dunes_OpenDeep_CreatureOnly,
                count = 4,
                probability = 0.2f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Mountains_OpenShallow_CreatureOnly,
                count = 4,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Mountains_OpenDeep_CreatureOnly,
                count = 4,
                probability = 0.2f
            },
        };
    }
}
