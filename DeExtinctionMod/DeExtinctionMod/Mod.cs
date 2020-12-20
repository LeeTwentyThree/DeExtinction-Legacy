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
using ECCLibrary;
using SMLHelper.V2.Assets;
using UWE;

namespace DeExtinctionMod
{
    [QModCore]
    public static class QPatch
    {
        public static AssetBundle assetBundle;

        public static GargantuanLeviathanPrefab gargantuanLeviathan;
        public static StellarThalassaceanPrefab stellarThalassacean;
        public static JasperThalassaceanPrefab jasperThalassacean;
        public static GrandGliderPrefab grandGlider;
        public static ClownPincherRuby rubyClownPincher;

        public static StellarThalassaceanEggPrefab stellarEgg;
        public static JasperThalassaceanEggPrefab jasperEgg;
        public static GrandGliderEggPrefab grandGliderEgg;

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

            gargantuanLeviathan = new GargantuanLeviathanPrefab("GargantuanLeviathan", "Gargantuan Leviathan", "An ancient creature thought to be extinct", assetBundle.LoadAsset<GameObject>("GargantuanPrefab"), null);
            gargantuanLeviathan.Patch();

            stellarThalassacean = new StellarThalassaceanPrefab("StellarThalassacean", "Stellar Thalassacean", "Large filter feeder, raised in containment.", assetBundle.LoadAsset<GameObject>("StellarThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Stellar_Item"));
            stellarThalassacean.Patch();

            jasperThalassacean = new JasperThalassaceanPrefab("JasperThalassacean", "Jasper Thalassacean", "Large cave-dwelling filter feeder, raised in containment.", assetBundle.LoadAsset<GameObject>("JasperThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Jasper_Item"));
            jasperThalassacean.Patch();

            grandGlider = new GrandGliderPrefab("GrandGlider", "Grand Glider", "Medium sized prey animal, raised in containment.", assetBundle.LoadAsset<GameObject>("GrandGliderPrefab"), assetBundle.LoadAsset<Texture2D>("GrandGlider_Item"));
            grandGlider.Patch();

            rubyClownPincher = new ClownPincherRuby("RubyClownPincher", "Ruby Clown Pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("GrandGliderPrefab"), assetBundle.LoadAsset<Texture2D>("Jasper_Item"));
            rubyClownPincher.Patch();

            #endregion

            #region Eggs

            LanguageHandler.SetLanguageLine("EncyPath_Lifeforms/Fauna/Eggs", "Creature Eggs");

            stellarEgg = new StellarThalassaceanEggPrefab("StellarThalassaceanEgg", "Stellar Thalassacean Egg", "Stellar Thallasaceans hatch from these.", assetBundle.LoadAsset<GameObject>("StellarThalassaceanEggPrefab"), stellarThalassacean.TechType, assetBundle.LoadAsset<Texture2D>("StellarThalassaceanEgg_Icon"), 2f);
            stellarEgg.Patch();

            jasperEgg = new JasperThalassaceanEggPrefab("JasperThalassaceanEgg", "Jasper Thalassacean Egg", "Jasper Thallasaceans hatch from these.", assetBundle.LoadAsset<GameObject>("JasperThalassaceanEggPrefab"), jasperThalassacean.TechType, assetBundle.LoadAsset<Texture2D>("JasperThalassaceanEgg_Icon"), 2f);
            jasperEgg.Patch();

            grandGliderEgg = new GrandGliderEggPrefab("GrandGliderEgg", "Grand Glider Egg", "Grand Gliders hatch from these.", assetBundle.LoadAsset<GameObject>("GGEggPrefab"), grandGlider.TechType, assetBundle.LoadAsset<Texture2D>("GGEgg_Item"), 1f);
            grandGliderEgg.Patch();

            #endregion

            modAudio = new ModAudio();
            modAudio.Init(assetBundle);

            Harmony harmony = new Harmony("Lee23.DeExtinctionMod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        static void LoadAssetBundle()
        {
            assetBundle = AssetBundle.LoadFromFile(AssetBundlePath);
        }
    }

    public class CreatureModReferences
    {
        public ModAudio modAudio;
        public AssetBundle assetBundle;
    }
}
