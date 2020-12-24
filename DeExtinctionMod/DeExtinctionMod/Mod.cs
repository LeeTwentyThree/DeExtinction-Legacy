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
using ECCLibrary.Internal;

namespace DeExtinctionMod
{
    [QModCore]
    public static class QPatch
    {
        public static AssetBundle assetBundle;

        public static StellarThalassaceanPrefab stellarThalassacean;
        public static JasperThalassaceanPrefab jasperThalassacean;
        public static GrandGliderPrefab grandGlider;
        public static ClownPincherRuby rubyClownPincher;
        public static ClownPincherSapphire sapphireClownPincher;
        public static ClownPincherEmerald emeraldClownPincher;

        public static EatableAsset rcpCooked;
        public static EatableAsset rcpCured;

        public static EatableAsset scpCooked;
        public static EatableAsset scpCured;

        public static EatableAsset ecpCooked;
        public static EatableAsset ecpCured;

        public static StellarThalassaceanEggPrefab stellarEgg;
        public static JasperThalassaceanEggPrefab jasperEgg;
        public static GrandGliderEggPrefab grandGliderEgg;

        public static EcoTargetType clownPincherSpecialEdible;

        [QModPatch]
        public static void Patch()
        {
            assetBundle = ECCHelpers.LoadAssetBundleFromAssetsFolder(Assembly.GetExecutingAssembly(), "deextinctionassets");
            ECCAudio.RegisterClips(assetBundle);

            clownPincherSpecialEdible = (EcoTargetType)531513; //Just a random value. Please don't copy this! It will cause incompatibility. Thanks.

            MakeItemClownPincherEdible("WorldEntities/Natural/SeaTreaderPoop");
            MakeItemClownPincherEdible("WorldEntities/Natural/CreepvineSeedCluster");

            #region Creatures

            stellarThalassacean = new StellarThalassaceanPrefab("StellarThalassacean", "Stellar Thalassacean", "Large filter feeder, raised in containment.", assetBundle.LoadAsset<GameObject>("StellarThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Stellar_Item"));
            stellarThalassacean.Patch();

            jasperThalassacean = new JasperThalassaceanPrefab("JasperThalassacean", "Jasper Thalassacean", "Large cave-dwelling filter feeder, raised in containment.", assetBundle.LoadAsset<GameObject>("JasperThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Jasper_Item"));
            jasperThalassacean.Patch();

            grandGlider = new GrandGliderPrefab("GrandGlider", "Grand Glider", "Medium sized prey animal, raised in containment.", assetBundle.LoadAsset<GameObject>("GrandGliderPrefab"), assetBundle.LoadAsset<Texture2D>("GrandGlider_Item"));
            grandGlider.Patch();

            rubyClownPincher = new ClownPincherRuby("RubyClownPincher", "Ruby Clown Pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("RCP_Prefab"), assetBundle.LoadAsset<Texture2D>("RCP_Item"));
            rubyClownPincher.Patch();

            sapphireClownPincher = new ClownPincherSapphire("SapphireClownPincher", "Sapphire Clown Pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("SCP_Prefab"), assetBundle.LoadAsset<Texture2D>("SCP_Item"));
            sapphireClownPincher.Patch();

            emeraldClownPincher = new ClownPincherEmerald("EmeraldClownPincher", "Emerald Clown Pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("ECP_Prefab"), assetBundle.LoadAsset<Texture2D>("ECP_Item"));
            emeraldClownPincher.Patch();

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

            #region Edibles
            rcpCooked = new EatableAsset("CookedRubyClownPincher", "Cooked Ruby Clown Pincher", "1,219 Scoville Heat Unit meal.", assetBundle.LoadAsset<GameObject>("RCP_Prefab"), rubyClownPincher.TechType, new EatableData(true, 41f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("RCP_Cooked"));
            rcpCooked.Patch();
            rcpCured = new EatableAsset("CuredRubyClownPincher", "Cured Ruby Clown Pincher", "Tastes like igneous. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("RCP_Prefab"), rubyClownPincher.TechType, new EatableData(true, 41f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("RCP_Cured"));
            rcpCured.Patch();

            scpCooked = new EatableAsset("CookedSapphireClownPincher", "Cooked Sapphire Clown Pincher", "The slime enhances flavor.", assetBundle.LoadAsset<GameObject>("SCP_Prefab"), sapphireClownPincher.TechType, new EatableData(true, 41f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("SCP_Cooked"));
            scpCooked.Patch();
            scpCured = new EatableAsset("CuredSapphireClownPincher", "Cured Sapphire Clown Pincher", "Tastes like milk. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("SCP_Prefab"), sapphireClownPincher.TechType, new EatableData(true, 41f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("SCP_Cured"));
            scpCured.Patch();

            ecpCooked = new EatableAsset("CookedEmeraldClownPincher", "Cooked Emerald Clown Pincher", "Pre-sautéed.", assetBundle.LoadAsset<GameObject>("ECP_Prefab"), emeraldClownPincher.TechType, new EatableData(true, 41f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("ECP_Cooked"));
            ecpCooked.Patch();
            ecpCured = new EatableAsset("CuredEmeraldClownPincher", "Cured Emerald Clown Pincher", "Tastes like lettuce. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("ECP_Prefab"), emeraldClownPincher.TechType, new EatableData(true, 41f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("ECP_Cured"));
            ecpCured.Patch();

            #endregion

            Harmony harmony = new Harmony("SpaceCatCreations.DeExtinctionMod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        static void MakeItemClownPincherEdible(string path)
        {
            GameObject obj = Resources.Load<GameObject>(path);
            if (obj == null)
            {
                Debug.Log("DE EXTINCTION: No prefab found at path " + path);
                return;
            }
            Debug.Log("DE EXTINCTION: Added item to clown pincher edible list");
            obj.AddComponent<EcoTarget>().type = clownPincherSpecialEdible;
            foreach(Component comp in obj.GetComponents<Component>())
            {
                Debug.Log("DE EXTINCTION: " + comp.GetType().ToString());
            }
        }
    }
}
