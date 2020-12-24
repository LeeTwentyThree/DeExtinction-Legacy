using ECCLibrary;
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
        public JasperThalassaceanEggPrefab(string classId, string friendlyName, string description, GameObject model, TechType hatchingCreature, Texture2D spriteTexture, float hatchingTime) : base(classId, friendlyName, description, model, hatchingCreature, spriteTexture, hatchingTime)
        {
        }

        public override Vector2int SizeInInventory => new Vector2int(3, 3);

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.LostRiverCorridor_LakeFloor,
                count = 1,
                probability = 1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.LostRiverJunction_LakeFloor,
                count = 1,
                probability = 1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BonesField_Lake_Floor,
                count = 1,
                probability = 1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BonesField_Cave_Ground,
                count = 1,
                probability = 1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Canyon_Lake_Floor,
                count = 1,
                probability = 1f
            }
        };

        public override float GetMaxHealth => 60f;

        public override bool AcidImmune => true;
    }
}
