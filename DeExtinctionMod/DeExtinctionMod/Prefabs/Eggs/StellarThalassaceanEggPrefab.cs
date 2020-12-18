using DeExtinctionMod.AssetClasses;
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
        public StellarThalassaceanEggPrefab(string classId, string friendlyName, string description, GameObject model, TechType hatchingCreature, Texture2D spriteTexture, float hatchingTime) : base(classId, friendlyName, description, model, hatchingCreature, spriteTexture, hatchingTime)
        {
        }

        public override Vector2int SizeInInventory => new Vector2int(3, 3);

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GrandReef_CaveFloor,
                count = 1,
                probability = 10f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.KooshZone_CaveFloor,
                count = 1,
                probability = 6f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Dunes_CaveFloor,
                count = 1,
                probability = 5f
            }
        };

       

        public override float GetMaxHealth => 60f;
    }
}
