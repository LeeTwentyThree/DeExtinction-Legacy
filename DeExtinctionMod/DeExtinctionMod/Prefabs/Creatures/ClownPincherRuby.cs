using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DeExtinctionMod.AssetClasses;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class ClownPincherRuby : ClownPincherPrefab
    {
        public ClownPincherRuby(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {

        }

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 2f, "Lifeforms/Fauna/SmallHerbivores", new string[] { "Lifeforms", "Fauna", "SmallHerbivores" }, QPatch.assetBundle.LoadAsset<Sprite>("Stellar_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("Stellar_Ency"));

        public override string GetEncyTitle => "Ruby Clown Pincher";

        public override string GetEncyDesc => "A small colorful herbivore found within a volatile cave system.\n\nColoration:\nColoration appears to mimic the surrounding rock, with white stripes to break up the pattern.\n\nBehavior:\nA social species, the Ruby Clown Pincher can be found forming lose shoals while foraging.\n\nSpecimen shows high genetic diversity suggesting many extant, closely related species that frequently mate.\n\nAssessment: Edible";

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.InactiveLavaZone_Chamber_Open_CreatureOnly,
                probability = 0.1f,
                count = 2
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.InactiveLavaZone_LavaPit_Open_CreatureOnly,
                probability = 0.1f,
                count = 2
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.ActiveLavaZone_Chamber_Open_CreatureOnly,
                probability = 0.1f,
                count = 2
            }
        };
    }
}
