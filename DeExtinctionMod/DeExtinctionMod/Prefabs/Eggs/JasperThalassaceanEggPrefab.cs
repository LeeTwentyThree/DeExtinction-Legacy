using DeExtinctionMod.Asset_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Prefabs.Eggs
{
    public class JasperThalassaceanEggPrefab : CreatureEggAsset
    {
        public override Vector2int SizeInInventory => new Vector2int(2, 2);

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.LostRiverCorridor_Open_CreatureOnly,
                count = 1,
                probability = 5f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.LostRiverJunction_Open_CreatureOnly,
                count = 1,
                probability = 5f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BonesField_Skeleton_Open_CreatureOnly,
                count = 1,
                probability = 5f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BonesField_LakePit_Open_CreatureOnly,
                count = 1,
                probability = 5f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GhostTree_Open_Big_CreatureOnly,
                count = 1,
                probability = 5f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Canyon_Open_CreatureOnly,
                count = 1,
                probability = 5f
            }
        };

        public JasperThalassaceanEggPrefab(string classId, string friendlyName, string description, GameObject model, TechType hatchingCreature, Texture2D spriteTexture) : base(classId, friendlyName, description, model, hatchingCreature, spriteTexture)
        {

        }

        public override float GetMaxHealth => 60f;
    }
}
