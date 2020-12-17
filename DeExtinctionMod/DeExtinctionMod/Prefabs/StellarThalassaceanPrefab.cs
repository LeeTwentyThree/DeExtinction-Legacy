using DeExtinctionMod.Mono;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;

namespace DeExtinctionMod.Prefabs
{
    public class StellarThalassaceanPrefab : ThalassaceanPrefab
    {
        public override string GetEncyTitle => "Stellar Thalassacean";

        public override string GetEncyDesc => "A large, docile filter feeder, nearly reaching leviathan class sizes. Presence suggests areas of more plentiful planktonic life compared to surrounding waters.\n\n1. Mouth:\nA large mouth sitauted on the front of the Thalassacean’s body is used to filter small zooplankton-like organisms from the water.\n\n2. Defense:\nDespite moving at very slow speeds a majority of the time, Thalassaceans are capable of short bursts of much higher speeds.This is likely the creature’s primary defense against larger predators.\n\n3. Taxonomy:\nGenetic evidence shows distant relation to the “Peeper” of shallower waters. This relation can still be observed in the creature’s quad-jaw mouth structure.\n\nAssessment: Filter feeding organism commonly predated by other large creatures.";

        public override ScannableCreatureData ScannableSettings => new ScannableCreatureData(true, 5f, "Lifeforms/Fauna/Carnivores", new string[] { "Lifeforms", "Fauna", "Carnivores" }, QPatch.assetBundle.LoadAsset<Sprite>("Stellar_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("Stellar_Ency"));

        public StellarThalassaceanPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GrandReef_OpenShallow_CreatureOnly,
                count = 1,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GrandReef_OpenDeep_CreatureOnly,
                count = 1,
                probability = 0.05f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.KooshZone_OpenShallow_CreatureOnly,
                count = 1,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.KooshZone_OpenDeep_CreatureOnly,
                count = 1,
                probability = 0.05f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Dunes_OpenShallow_CreatureOnly,
                count = 1,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.Dunes_OpenDeep_CreatureOnly,
                count = 1,
                probability = 0.05f
            }
        };

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            techType = TechType,
            classId = ClassID,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Creature
        };
    }
}
