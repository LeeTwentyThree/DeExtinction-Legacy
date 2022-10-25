using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECCLibrary;
using UnityEngine;

namespace DeExtinctionMod.Prefabs.Eggs
{
    public class GulperEggPrefab : CreatureEggAsset
    {
        private LiveMixinData liveMixinData;

        public GulperEggPrefab(string classId, string friendlyName, string description, GameObject model, TechType hatchingCreature, Texture2D spriteTexture, float hatchingTime) : base(classId, friendlyName, description, model, hatchingCreature, spriteTexture, hatchingTime)
        {
            liveMixinData = ECCHelpers.CreateNewLiveMixinData();
            liveMixinData.maxHealth = 200f;
            liveMixinData.destroyOnDeath = true;
            liveMixinData.explodeOnDestroy = true;
            liveMixinData.broadcastKillOnDeath = true;
        }

        public override float GetMaxHealth => 200f;

        public override Vector2int SizeInInventory => new Vector2int(3, 3);

        public override bool IsScannable => true;

        public override string GetEncyDesc => "These eggs are large and vibrant and can be found on underwater islands and beaches. Small air pockets on the interior suggest that the eggs develop best when laid in shallow water, but this is impossible in many places due to the gradual sinking of islands.\n\nScans indicate a small translucent larva on the inside. This larva is very undeveloped, with a small mouth and a long, segmented body.\n\nData suggests that when hatched, these creatures rely on protection from their parents until their jaws can fully develop.\n\nThe creature will be helpless after hatching, and must be cared for by an adult during its unusually long period of development.";

        public override void AddCustomBehaviours()
        {
            ECCHelpers.MakeObjectScannerRoomScannable(prefab, false);
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = TechType.GenericEgg;
            prefab.EnsureComponent<LiveMixin>().data = liveMixinData;
        }

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_IslandPlants,
                count = 1,
                probability = 0.24f
            }
        };

        public override List<SpawnLocation> CoordinatedSpawns => new List<SpawnLocation>()
        {
            new SpawnLocation(new Vector3(-702.51f, -0.86f, -963.24f), new Vector3(18, 358, 345)),
            new SpawnLocation(new Vector3(-715.52f, -0.97f, -951.93f), new Vector3(0, 0, 250)),
            new SpawnLocation(new Vector3(-696.33f, -1f, -1069.41f), new Vector3(13, 0, 2)),
            new SpawnLocation(new Vector3(-727.81f, 0.10f, -1085.91f), new Vector3(278, 0, 354)),
            new SpawnLocation(new Vector3(393.37f, -9.91f, 1187.26f), new Vector3(77, 60, 180)),
            new SpawnLocation(new Vector3(-831.56f, -1.95f, -966.07f), new Vector3(77, 60, 180))
        };
    }
}
