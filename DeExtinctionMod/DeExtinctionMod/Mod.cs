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
        public static ClownPincherAmber amberClownPincher;
        public static ClownPincherCitrine citrineClownPincher;
        public static AxetailPrefab axetail;
        public static GulperPrefab gulper;

        public static EatableAsset rcpCooked;
        public static EatableAsset rcpCured;

        public static EatableAsset scpCooked;
        public static EatableAsset scpCured;

        public static EatableAsset ecpCooked;
        public static EatableAsset ecpCured;

        public static EatableAsset acpCooked;
        public static EatableAsset acpCured;

        public static EatableAsset ccpCooked;
        public static EatableAsset ccpCured;

        public static EatableAsset axetailCooked;
        public static EatableAsset axetailCured;

        public static StellarThalassaceanEggPrefab stellarEgg;
        public static JasperThalassaceanEggPrefab jasperEgg;
        public static GrandGliderEggPrefab grandGliderEgg;

        public static EcoTargetType clownPincherSpecialEdible;

        [QModPatch]
        public static void Patch()
        {
            ErrorMessage.AddMessage("Hello world!");
            assetBundle = ECCHelpers.LoadAssetBundleFromAssetsFolder(Assembly.GetExecutingAssembly(), "deextinctionassets");
            ECCAudio.RegisterClips(assetBundle);

            clownPincherSpecialEdible = (EcoTargetType)531513; //Just a random value. Please don't copy this! It will cause incompatibility. Thanks.

            MakeItemClownPincherEdible("WorldEntities/Natural/SeaTreaderPoop");
            MakeItemClownPincherEdible("WorldEntities/Eggs/StalkerEgg");

            #region Creatures

            stellarThalassacean = new StellarThalassaceanPrefab("StellarThalassacean", "Stellar Thalassacean", "Large filter feeder, raised in containment.", assetBundle.LoadAsset<GameObject>("StellarThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Stellar_Item"));
            stellarThalassacean.Patch();

            jasperThalassacean = new JasperThalassaceanPrefab("JasperThalassacean", "Jasper Thalassacean", "Large cave-dwelling filter feeder, raised in containment.", assetBundle.LoadAsset<GameObject>("JasperThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Jasper_Item"));
            jasperThalassacean.Patch();

            grandGlider = new GrandGliderPrefab("GrandGlider", "Grand Glider", "Medium sized prey animal, raised in containment.", assetBundle.LoadAsset<GameObject>("GrandGliderPrefab"), assetBundle.LoadAsset<Texture2D>("GrandGlider_Item"));
            grandGlider.Patch();

            gulper = new GulperPrefab("GulperLeviathan", "Gulper Leviathan", "Leviathan-class predator with a huge mouth.", assetBundle.LoadAsset<GameObject>("Gulper_Prefab"), null);
            gulper.Patch();

            #region ClownPinchers
            rubyClownPincher = new ClownPincherRuby("RubyClownPincher", "Ruby Clown Pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("RCP_Prefab"), assetBundle.LoadAsset<Texture2D>("RCP_Item"));
            rubyClownPincher.Patch();

            sapphireClownPincher = new ClownPincherSapphire("SapphireClownPincher", "Sapphire Clown Pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("SCP_Prefab"), assetBundle.LoadAsset<Texture2D>("SCP_Item"));
            sapphireClownPincher.Patch();

            emeraldClownPincher = new ClownPincherEmerald("EmeraldClownPincher", "Emerald Clown Pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("ECP_Prefab"), assetBundle.LoadAsset<Texture2D>("ECP_Item"));
            emeraldClownPincher.Patch();

            amberClownPincher = new ClownPincherAmber("AmberClownPincher", "Amber Clown Pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("ACP_Prefab"), assetBundle.LoadAsset<Texture2D>("ACP_Item"));
            amberClownPincher.Patch();

            citrineClownPincher = new ClownPincherCitrine("CitrineClownPincher", "Citrine Clown Pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("CCP_Prefab"), assetBundle.LoadAsset<Texture2D>("CCP_Item"));
            citrineClownPincher.Patch();
            #endregion

            axetail = new AxetailPrefab("Axetail", "Axetail", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("Axetail_Prefab"), assetBundle.LoadAsset<Texture2D>("Axetail_Item"));
            axetail.Patch();

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

            acpCooked = new EatableAsset("CookedAmberClownPincher", "Cooked Amber Clown Pincher", "Not the worst tasting thing on the planet.", assetBundle.LoadAsset<GameObject>("ACP_Prefab"), amberClownPincher.TechType, new EatableData(true, 41f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("ACP_Cooked"));
            acpCooked.Patch();
            acpCured = new EatableAsset("CuredAmberClownPincher", "Cured Amber Clown Pincher", "Tastes like radish. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("ACP_Prefab"), amberClownPincher.TechType, new EatableData(true, 41f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("ACP_Cured"));
            acpCured.Patch();

            ccpCooked = new EatableAsset("CookedCitrineClownPincher", "Cooked Citrine Clown Pincher", "The secret is in the claws.", assetBundle.LoadAsset<GameObject>("CCP_Prefab"), citrineClownPincher.TechType, new EatableData(true, 41f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("CCP_Cooked"));
            ccpCooked.Patch();
            ccpCured = new EatableAsset("CuredCitrineClownPincher", "Cured Citrine Clown Pincher", "Tastes like potatoes. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("CCP_Prefab"), citrineClownPincher.TechType, new EatableData(true, 41f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("CCP_Cured"));
            ccpCured.Patch();

            axetailCooked = new EatableAsset("CookedAxetail", "Cooked Axetail", "A sharp taste. Hydrating.", assetBundle.LoadAsset<GameObject>("Axetail_Prefab"), axetail.TechType, new EatableData(true, 20f, 13f, true), false, assetBundle.LoadAsset<Texture2D>("Axetail_Cooked"));
            axetailCooked.Patch();
            axetailCured = new EatableAsset("CuredAxetail", "Cooked Axetail", "Eat around the pointy bits. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("Axetail_Prefab"), axetail.TechType, new EatableData(true, 20f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("Axetail_Cured"));
            axetailCured.Patch();

            #endregion

            const float gulperSpawnDistance = 125f;
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-970f, -216f, -509f), "BKTGulper", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(1169, -370, 903), "Mountains+KooshGulper", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-72, -300, 867), "UWIGulper1", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-174, -460, 1070), "UWIGulper2", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-265, -287, 1118), "BK+UWIGulper", gulperSpawnDistance));

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
            obj.AddComponent<EcoTarget>().type = clownPincherSpecialEdible;
        }
    }
}
