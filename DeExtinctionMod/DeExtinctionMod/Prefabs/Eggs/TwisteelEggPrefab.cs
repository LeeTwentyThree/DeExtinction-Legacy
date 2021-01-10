using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECCLibrary;
using UnityEngine;

namespace DeExtinctionMod.Prefabs.Eggs
{
    public class TwisteelEggPrefab : CreatureEggAsset
    {
        public TwisteelEggPrefab(string classId, string friendlyName, string description, GameObject model, TechType hatchingCreature, Texture2D spriteTexture, float hatchingTime) : base(classId, friendlyName, description, model, hatchingCreature, spriteTexture, hatchingTime)
        {
        }

        public override float GetMaxHealth => 60f;

        public override Vector2int SizeInInventory => new Vector2int(2, 2);

        public override bool IsScannable => true;
        public override string GetEncyDesc => "Twisteels display a very simple life cycle from birth to death, and will often lay small quantities of eggs in secluded areas.";


        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_IslandCaveFloor,
                count = 1,
                probability = 0.8f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_ValleyFloor,
                count = 1,
                probability = 0.09f
            }
        };
    }
}
