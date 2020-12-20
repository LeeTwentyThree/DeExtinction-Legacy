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
using DeExtinctionMod.Prefabs.Creatures;
using DeExtinctionMod.AssetClasses;
using SMLHelper.V2.Assets;
using UWE;

namespace DeExtinctionMod
{
    [QModCore]
    public static class QPatch
    {
        public static AssetBundle assetBundle;

        public static GargantuanLeviathanPrefab gargantuanPrefab;
        public static StellarThalassaceanPrefab stellarThalassaceanPrefab;
        public static JasperThalassaceanPrefab jasperThalassaceanPrefab;
        public static GrandGliderPrefab grandGliderPrefab;
        public static ClownPincherRuby rubyClownPincher;

        public static StellarThalassaceanEggPrefab stellarEgg;
        public static JasperThalassaceanEggPrefab jasperEgg;

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

            #region Creatures

            gargantuanPrefab = new GargantuanLeviathanPrefab("GargantuanLeviathan", "Gargantuan Leviathan", "An ancient creature thought to be extinct", assetBundle.LoadAsset<GameObject>("GargantuanPrefab"), null);
            gargantuanPrefab.Patch();

            stellarThalassaceanPrefab = new StellarThalassaceanPrefab("StellarThalassacean", "Stellar Thalassacean", "Large filter feeder, raised in containment.", assetBundle.LoadAsset<GameObject>("StellarThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Stellar_Item"));
            stellarThalassaceanPrefab.Patch();

            jasperThalassaceanPrefab = new JasperThalassaceanPrefab("JasperThalassacean", "Jasper Thalassacean", "Large cave-dwelling filter feeder, raised in containment.", assetBundle.LoadAsset<GameObject>("JasperThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Jasper_Item"));
            jasperThalassaceanPrefab.Patch();

            grandGliderPrefab = new GrandGliderPrefab("GrandGlider", "Grand Glider", "Medium sized prey animal, raised in containment.", assetBundle.LoadAsset<GameObject>("GrandGliderPrefab"), assetBundle.LoadAsset<Texture2D>("GrandGlider_Item"));
            grandGliderPrefab.Patch();

            /*rubyClownPincher = new ClownPincherRuby("RubyClownPincher", "Ruby Clown Pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("GrandGliderPrefab"), assetBundle.LoadAsset<Texture2D>("Jasper_Item"));
            rubyClownPincher.Patch();*/

            #endregion

            #region Eggs

            stellarEgg = new StellarThalassaceanEggPrefab("StellarThalassaceanEgg", "Stellar Thalassacean Egg", "Stellar Thallasaceans hatch from these.", assetBundle.LoadAsset<GameObject>("StellarThalassaceanEggPrefab"), stellarThalassaceanPrefab.TechType, assetBundle.LoadAsset<Texture2D>("StellarThalassaceanEgg_Icon"), 2f);
            stellarEgg.Patch();

            jasperEgg = new JasperThalassaceanEggPrefab("JasperThalassaceanEgg", "Jasper Thalassacean Egg", "Jasper Thallasaceans hatch from these.", assetBundle.LoadAsset<GameObject>("JasperThalassaceanEggPrefab"), jasperThalassaceanPrefab.TechType, assetBundle.LoadAsset<Texture2D>("JasperThalassaceanEgg_Icon"), 2f);
            jasperEgg.Patch();

            #endregion

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
