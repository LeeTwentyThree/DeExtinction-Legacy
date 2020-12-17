using DeExtinctionMod.Asset_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Prefabs.Eggs
{
    public class StellarThalassaceanEggPrefab : CreatureEggAsset
    {
        public override Vector2int SizeInInventory => new Vector2int(2, 2);

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GrandReef_Ground,
                count = 1,
                probability = 10f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.KooshZone_Sand,
                count = 1,
                probability = 6f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Dunes_SandPlateau,
                count = 1,
                probability = 5f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Dunes_Rock,
                count = 1,
                probability = 5f
            }
        };

        public StellarThalassaceanEggPrefab(string classId, string friendlyName, string description, GameObject model, TechType hatchingCreature, Texture2D spriteTexture) : base(classId, friendlyName, description, model, hatchingCreature, spriteTexture)
        {

        }

        public override float GetMaxHealth => 60f;
    }
}
