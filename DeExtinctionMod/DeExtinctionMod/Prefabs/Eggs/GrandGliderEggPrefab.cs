﻿using DeExtinctionMod.AssetClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Prefabs.Eggs
{
    public class GrandGliderEggPrefab : CreatureEggAsset
    {
        public GrandGliderEggPrefab(string classId, string friendlyName, string description, GameObject model, TechType hatchingCreature, Texture2D spriteTexture, float hatchingTime) : base(classId, friendlyName, description, model, hatchingCreature, spriteTexture, hatchingTime)
        {

        }

        public override Vector2int SizeInInventory => new Vector2int(2, 2);

        public override string GetEncyDesc => "Each Grand Glider lays only a few eggs in its lifetime. If an individual Grand Glider is unable to find a safe and secluded place for her egg, the entire shoal will often be very determined to protect it.";

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
                biome = BiomeType.Mountains_CaveFloor,
                count = 1,
                probability = 7f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Mesas_Top,
                count = 1,
                probability = 5f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.CragField_Ground,
                count = 1,
                probability = 2f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.CragField_Rock,
                count = 1,
                probability = 2f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.CragField_Sand,
                count = 1,
                probability = 2f
            }
        };

        public override float GetMaxHealth => 60f;
    }
}
