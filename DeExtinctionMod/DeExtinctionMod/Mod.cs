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
        public static RibbonRayPrefab ribbonRay;
        public static TwisteelPrefab twisteel;
        public static FiltorbPrefab filtorb;
        public static JellySpinnerPrefab jellySpinner;
        public static TrianglefishPrefab triangleFish;

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

        public static EatableAsset ribbonRayCooked;
        public static EatableAsset ribbonRayCured;

        public static EatableAsset filtorbCooked;
        public static EatableAsset filtorbCured;

        public static EatableAsset jellySpinnerCooked;
        public static EatableAsset jellySpinnerCured;

        public static EatableAsset trianglefishCooked;
        public static EatableAsset trianglefishCured;

        public static StellarThalassaceanEggPrefab stellarEgg;
        public static JasperThalassaceanEggPrefab jasperEgg;
        public static GrandGliderEggPrefab grandGliderEgg;
        public static TwisteelEggPrefab twisteelEgg;

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

            stellarThalassacean = new StellarThalassaceanPrefab("StellarThalassacean", "Stellar thalassacean", "Large filter feeder, raised in containment.", assetBundle.LoadAsset<GameObject>("StellarThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Stellar_Item"));
            stellarThalassacean.Patch();

            jasperThalassacean = new JasperThalassaceanPrefab("JasperThalassacean", "Jasper thalassacean", "Large cave-dwelling filter feeder, raised in containment.", assetBundle.LoadAsset<GameObject>("JasperThalassaceanPrefab"), assetBundle.LoadAsset<Texture2D>("Jasper_Item"));
            jasperThalassacean.Patch();

            grandGlider = new GrandGliderPrefab("GrandGlider", "Grand glider", "Medium sized prey animal, raised in containment.", assetBundle.LoadAsset<GameObject>("GrandGliderPrefab"), assetBundle.LoadAsset<Texture2D>("GrandGlider_Item"));
            grandGlider.Patch();

            gulper = new GulperPrefab("GulperLeviathan", "Gulper leviathan", "Leviathan-class predator with a huge mouth.", assetBundle.LoadAsset<GameObject>("Gulper_Prefab"), assetBundle.LoadAsset<Texture2D>("Gulper_ScannerRoom"));
            gulper.Patch();

            axetail = new AxetailPrefab("Axetail", "Axetail", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("Axetail_Prefab"), assetBundle.LoadAsset<Texture2D>("Axetail_Item"));
            axetail.Patch();

            ribbonRay = new RibbonRayPrefab("RibbonRay", "Ribbon ray", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("RibbonRay_Prefab"), assetBundle.LoadAsset<Texture2D>("RibbonRay_Item"));
            ribbonRay.Patch();

            twisteel = new TwisteelPrefab("Twisteel", "Twisteel", "Thin eel-like organism, raised in containment.", assetBundle.LoadAsset<GameObject>("Twisteel_Prefab"), assetBundle.LoadAsset<Texture2D>("Twisteel_Item"));
            twisteel.Patch();

            filtorb = new FiltorbPrefab("Filtorb", "Filtorb", "Small, filter feeding organism.", assetBundle.LoadAsset<GameObject>("Filtorb_Prefab"), assetBundle.LoadAsset<Texture2D>("Filtorb_Item"));
            filtorb.Patch();

            jellySpinner = new JellySpinnerPrefab("JellySpinner", "Jelly spinner", "Small organism.", assetBundle.LoadAsset<GameObject>("JellySpinner_Prefab"), assetBundle.LoadAsset<Texture2D>("JellySpinner_Item"));
            jellySpinner.Patch();

            triangleFish = new TrianglefishPrefab("TriangleFish", "Trianglefish", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("Trianglefish_Prefab"), assetBundle.LoadAsset<Texture2D>("Trianglefish_Item"));
            triangleFish.Patch();

            #region ClownPinchers
            rubyClownPincher = new ClownPincherRuby("RubyClownPincher", "Ruby clown pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("RCP_Prefab"), assetBundle.LoadAsset<Texture2D>("RCP_Item"));
            rubyClownPincher.Patch();

            sapphireClownPincher = new ClownPincherSapphire("SapphireClownPincher", "Sapphire clown pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("SCP_Prefab"), assetBundle.LoadAsset<Texture2D>("SCP_Item"));
            sapphireClownPincher.Patch();

            emeraldClownPincher = new ClownPincherEmerald("EmeraldClownPincher", "Emerald clown pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("ECP_Prefab"), assetBundle.LoadAsset<Texture2D>("ECP_Item"));
            emeraldClownPincher.Patch();

            amberClownPincher = new ClownPincherAmber("AmberClownPincher", "Amber clown pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("ACP_Prefab"), assetBundle.LoadAsset<Texture2D>("ACP_Item"));
            amberClownPincher.Patch();

            citrineClownPincher = new ClownPincherCitrine("CitrineClownPincher", "Citrine clown pincher", "Small, edible prey fish.", assetBundle.LoadAsset<GameObject>("CCP_Prefab"), assetBundle.LoadAsset<Texture2D>("CCP_Item"));
            citrineClownPincher.Patch();
            #endregion

            #endregion

            #region Eggs

            LanguageHandler.SetLanguageLine("EncyPath_Lifeforms/Fauna/Eggs", "Creature Eggs");

            stellarEgg = new StellarThalassaceanEggPrefab("StellarThalassaceanEgg", "Stellar Thalassacean Egg", "Stellar Thallasaceans hatch from these.", assetBundle.LoadAsset<GameObject>("StellarThalassaceanEggPrefab"), stellarThalassacean.TechType, assetBundle.LoadAsset<Texture2D>("StellarThalassaceanEgg_Icon"), 2f);
            stellarEgg.Patch();

            jasperEgg = new JasperThalassaceanEggPrefab("JasperThalassaceanEgg", "Jasper Thalassacean Egg", "Jasper Thallasaceans hatch from these.", assetBundle.LoadAsset<GameObject>("JasperThalassaceanEggPrefab"), jasperThalassacean.TechType, assetBundle.LoadAsset<Texture2D>("JasperThalassaceanEgg_Icon"), 2f);
            jasperEgg.Patch();

            grandGliderEgg = new GrandGliderEggPrefab("GrandGliderEgg", "Grand Glider Egg", "Grand Gliders hatch from these.", assetBundle.LoadAsset<GameObject>("GGEggPrefab"), grandGlider.TechType, assetBundle.LoadAsset<Texture2D>("GGEgg_Item"), 1f);
            grandGliderEgg.Patch();

            twisteelEgg = new TwisteelEggPrefab("TwisteelEgg", "Twisteel Egg", "Twisteels hatch from these", assetBundle.LoadAsset<GameObject>("TwisteelEgg_Prefab"), twisteel.TechType, assetBundle.LoadAsset<Texture2D>("TwisteelEgg_Item"), 1.5f);
            twisteelEgg.Patch();

            #endregion

            #region Edibles
            rcpCooked = new EatableAsset("CookedRubyClownPincher", "Cooked ruby clown pincher", "1,219 Scoville Heat Unit meal.", assetBundle.LoadAsset<GameObject>("RCP_Prefab"), rubyClownPincher.TechType, new EatableData(true, 41f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("RCP_Cooked"));
            rcpCooked.Patch();
            rcpCured = new EatableAsset("CuredRubyClownPincher", "Cured ruby clown pincher", "Tastes like igneous. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("RCP_Prefab"), rubyClownPincher.TechType, new EatableData(true, 41f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("RCP_Cured"));
            rcpCured.Patch();

            scpCooked = new EatableAsset("CookedSapphireClownPincher", "Cooked sapphire clown pincher", "The slime enhances flavor.", assetBundle.LoadAsset<GameObject>("SCP_Prefab"), sapphireClownPincher.TechType, new EatableData(true, 41f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("SCP_Cooked"));
            scpCooked.Patch();
            scpCured = new EatableAsset("CuredSapphireClownPincher", "Cured sapphire clown pincher", "Tastes like milk. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("SCP_Prefab"), sapphireClownPincher.TechType, new EatableData(true, 41f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("SCP_Cured"));
            scpCured.Patch();

            ecpCooked = new EatableAsset("CookedEmeraldClownPincher", "Cooked emerald clown pincher", "Pre-sautéed.", assetBundle.LoadAsset<GameObject>("ECP_Prefab"), emeraldClownPincher.TechType, new EatableData(true, 41f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("ECP_Cooked"));
            ecpCooked.Patch();
            ecpCured = new EatableAsset("CuredEmeraldClownPincher", "Cured emerald clown pincher", "Tastes like lettuce. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("ECP_Prefab"), emeraldClownPincher.TechType, new EatableData(true, 41f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("ECP_Cured"));
            ecpCured.Patch();

            acpCooked = new EatableAsset("CookedAmberClownPincher", "Cooked amber clown pincher", "Not the worst tasting thing on the planet.", assetBundle.LoadAsset<GameObject>("ACP_Prefab"), amberClownPincher.TechType, new EatableData(true, 41f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("ACP_Cooked"));
            acpCooked.Patch();
            acpCured = new EatableAsset("CuredAmberClownPincher", "Cured amber clown pincher", "Tastes like radish. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("ACP_Prefab"), amberClownPincher.TechType, new EatableData(true, 41f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("ACP_Cured"));
            acpCured.Patch();

            ccpCooked = new EatableAsset("CookedCitrineClownPincher", "Cooked citrine clown pincher", "The secret is in the claws.", assetBundle.LoadAsset<GameObject>("CCP_Prefab"), citrineClownPincher.TechType, new EatableData(true, 41f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("CCP_Cooked"));
            ccpCooked.Patch();
            ccpCured = new EatableAsset("CuredCitrineClownPincher", "Cured citrine clown pincher", "Tastes like potatoes. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("CCP_Prefab"), citrineClownPincher.TechType, new EatableData(true, 41f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("CCP_Cured"));
            ccpCured.Patch();

            axetailCooked = new EatableAsset("CookedAxetail", "Cooked axetail", "A sharp taste. Somewhat hydrating.", assetBundle.LoadAsset<GameObject>("Axetail_Prefab"), axetail.TechType, new EatableData(true, 20f, 13f, true), false, assetBundle.LoadAsset<Texture2D>("Axetail_Cooked"));
            axetailCooked.Patch();
            axetailCured = new EatableAsset("CuredAxetail", "Cured axetail", "Eat around the pointy bits. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("Axetail_Prefab"), axetail.TechType, new EatableData(true, 20f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("Axetail_Cured"));
            axetailCured.Patch();

            ribbonRayCooked = new EatableAsset("CookedRibbonRay", "Cooked ribbon ray", "A hefty meal.", assetBundle.LoadAsset<GameObject>("RibbonRay_Prefab"), ribbonRay.TechType, new EatableData(true, 36f, 7f, true), false, assetBundle.LoadAsset<Texture2D>("RibbonRay_Cooked"));
            ribbonRayCooked.Patch();
            ribbonRayCured = new EatableAsset("CuredRibbonRay", "Cured ribbon ray", "Rubbery and stringy. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("RibbonRay_Prefab"), ribbonRay.TechType, new EatableData(true, 36f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("RibbonRay_Cured"));
            ribbonRayCured.Patch();

            filtorbCooked = new EatableAsset("CookedFiltorb", "Cooked filtorb", "Juicy.", assetBundle.LoadAsset<GameObject>("Filtorb_Prefab"), filtorb.TechType, new EatableData(true, 5f, 20f, true), false, assetBundle.LoadAsset<Texture2D>("Filtorb_Cooked"));
            filtorbCooked.Patch();
            filtorbCured = new EatableAsset("CuredFiltorb", "Cured filtorb", "Chalky. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("Filtorb_Prefab"), filtorb.TechType, new EatableData(true, 5f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("Filtorb_Cured"));
            filtorbCured.Patch();

            jellySpinnerCooked = new EatableAsset("CookedJellySpinner", "Cooked jelly spinner", "Pops in your mouth.", assetBundle.LoadAsset<GameObject>("JellySpinner_Prefab"), jellySpinner.TechType, new EatableData(true, 9f, 2f, true), false, assetBundle.LoadAsset<Texture2D>("JellySpinner_Cooked"));
            jellySpinnerCooked.Patch();
            jellySpinnerCured = new EatableAsset("CuredJellySpinner", "Cured jelly spinner", "Like eating bubble wrap. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("JellySpinner_Prefab"), jellySpinner.TechType, new EatableData(true, 9f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("JellySpinner_Cured"));
            jellySpinnerCured.Patch();

            trianglefishCooked = new EatableAsset("CookedTriangleFish", "Cooked trianglefish", "Small yet filling.", assetBundle.LoadAsset<GameObject>("Trianglefish_Prefab"), triangleFish.TechType, new EatableData(true, 22f, 3f, true), false, assetBundle.LoadAsset<Texture2D>("Trianglefish_Cooked"));
            trianglefishCooked.Patch();
            trianglefishCured = new EatableAsset("CuredTriangleFish", "Cured trianglefish", "Unusually crunchy. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("Trianglefish_Prefab"), triangleFish.TechType, new EatableData(true, 22f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("Trianglefish_Cured"));
            trianglefishCured.Patch();

            #endregion

            const float gulperSpawnDistance = 125f;
            //Mountains
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(1169, -370, 903), "Mountains+KooshGulper", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(1400, -348, 1281), "Mountains+KooshGulper2", gulperSpawnDistance));

            //Underwater islands
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-72, -300, 867), "UWIGulper1", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-174, -460, 1070), "UWIGulper2", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-71, -464, 869), "UWIGulper3", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-49, -308, 1184), "Mountains+UWIGulper", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-265, -287, 1118), "BK+UWIGulper", gulperSpawnDistance));

            //Sparse reef
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-683, -129, -717), "SparseReefGulper", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-717, -100, -1088), "FloatingIslandGulper", gulperSpawnDistance));

            //Blood kelp
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-573, -448, 1311), "Lifepod2Gulper", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-970, -216, -509), "BKTGulper", gulperSpawnDistance));

            Harmony harmony = new Harmony("SpaceCatCreations.DeExtinctionMod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        static void MakeItemClownPincherEdible(string path)
        {
            GameObject obj = Resources.Load<GameObject>(path);
            if (obj == null)
            {
                ECCLog.AddMessage("DE EXTINCTION: No prefab found at path " + path);
                return;
            }
            obj.AddComponent<EcoTarget>().type = clownPincherSpecialEdible;
        }
    }
}
