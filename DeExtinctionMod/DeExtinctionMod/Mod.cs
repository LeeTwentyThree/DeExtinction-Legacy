using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QModManager.API.ModLoading;
using UnityEngine;
using System.IO;
using System.Reflection;
using DeExtinctionMod.Prefabs;
using DeExtinctionMod.Mono;
using SMLHelper.V2.Handlers;
using HarmonyLib;
using SMLHelper.V2.Utility;
using DeExtinctionMod.Prefabs.Eggs;
using DeExtinctionMod.Asset_Classes;

namespace DeExtinctionMod
{
    [QModCore]
    public static class QPatch
    {
        public static AssetBundle assetBundle;

        public static GargantuanLeviathanPrefab gargantuanPrefab;
        public static StellarThalassaceanPrefab stellarThalassaceanPrefab;
        public static JasperThalassaceanPrefab jasperThalassaceanPrefab;
        public static StellarThalassaceanEggPrefab stellarEgg;
        public static JasperThalassaceanEggPrefab jasperEgg;
        public static CreatureEggAsset reaperEgg;

        public static ModAudio modAudio;

        public static string ModFolderPath
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }
        public static string AssetsPath
        {
            get
            {
                return Path.Combine(ModFolderPath, "Assets");
            }
        }
        public static string AssetBundlePath
        {
            get
            {
                return Path.Combine(AssetsPath, "deextinctionassets");
            }
        }

        [QModPatch]
        public static void Patch()
        {
            LoadAssetBundle();
            gargantuanPrefab = new GargantuanLeviathanPrefab("gargantuanleviathan", "Gargantuan Leviathan", "An ancient creature thought to be extinct", assetBundle.LoadAsset<GameObject>("GargantuanPrefab"), null);
            gargantuanPrefab.Patch();
            stellarThalassaceanPrefab = new StellarThalassaceanPrefab("stellarthalassacean", "Stellar Thalassacean", "A large, friendly filter feeder.", assetBundle.LoadAsset<GameObject>("StellarThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Stellar_Item"));
            stellarThalassaceanPrefab.Patch();
            jasperThalassaceanPrefab = new JasperThalassaceanPrefab("jasperthalassacean", "Jasper Thalassacean", "A large, friendly filter feeder. A deep-water relative of the Stellar Thalassacean.", assetBundle.LoadAsset<GameObject>("JasperThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Jasper_Item"));
            jasperThalassaceanPrefab.Patch();

            stellarEgg = new StellarThalassaceanEggPrefab("stellarthalassaceanegg", "Stellar Thalassacean Egg", "Stellar Thallasaceans hatch from these.", assetBundle.LoadAsset<GameObject>("StellarThalassaceanEggPrefab"), stellarThalassaceanPrefab.TechType, assetBundle.LoadAsset<Texture2D>("StellarThalassaceanEgg_Icon"));
            stellarEgg.Patch();

            jasperEgg = new JasperThalassaceanEggPrefab("jasperthalassaceanegg", "Jasper Thalassacean Egg", "Jasper Thallasaceans hatch from these.", assetBundle.LoadAsset<GameObject>("JasperThalassaceanEggPrefab"), jasperThalassaceanPrefab.TechType, assetBundle.LoadAsset<Texture2D>("JasperThalassaceanEgg_Icon"));
            jasperEgg.Patch();

            reaperEgg = new CreatureEggAsset("reaperegg", "Reaper Leviathan Egg", "Reaper Leviathans hatch from these.", assetBundle.LoadAsset<GameObject>("JasperThalassaceanEggPrefab"), TechType.ReaperLeviathan, null);
            WaterParkCreature.waterParkCreatureParameters.Add(TechType.ReaperLeviathan, new WaterParkCreatureParameters(0.05f, 0.15f, 0.3f, 3f, false)); ;
            reaperEgg.Patch();

            modAudio = new ModAudio();
            modAudio.Init();

            Harmony harmony = new Harmony("Lee23.DeExtinctionMod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        static void LoadAssetBundle()
        {
            assetBundle = AssetBundle.LoadFromFile(AssetBundlePath);
        }
    }

    public struct SaveData
    {
        public bool GargantuanHasSpawned;

        public SaveData(bool gargantuanHasSpawned)
        {
            GargantuanHasSpawned = gargantuanHasSpawned;
        }
    }
}
