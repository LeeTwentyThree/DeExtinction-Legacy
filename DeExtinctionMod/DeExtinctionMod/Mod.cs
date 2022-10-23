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
            rcpCooked = new EatableAsset("CookedRubyClownPincher", "Cooked ruby clown pincher", "1,219 Scoville Heat Unit meal.", assetBundle.LoadAsset<GameObject>("RCP_Prefab"), rubyClownPincher.TechType, new EatableData(true, 30f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("RCP_Cooked"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("RCP_Popup"));
            rcpCooked.Patch();
            rcpCured = new EatableAsset("CuredRubyClownPincher", "Cured ruby clown pincher", "Tastes like igneous. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("RCP_Prefab"), rubyClownPincher.TechType, new EatableData(true, 30f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("RCP_Cured"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("RCP_Popup"));
            rcpCured.Patch();

            scpCooked = new EatableAsset("CookedSapphireClownPincher", "Cooked sapphire clown pincher", "The slime enhances flavor.", assetBundle.LoadAsset<GameObject>("SCP_Prefab"), sapphireClownPincher.TechType, new EatableData(true, 30f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("SCP_Cooked"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("SCP_Popup"));
            scpCooked.Patch();
            scpCured = new EatableAsset("CuredSapphireClownPincher", "Cured sapphire clown pincher", "Tastes like milk. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("SCP_Prefab"), sapphireClownPincher.TechType, new EatableData(true, 30f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("SCP_Cured"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("SCP_Popup"));
            scpCured.Patch();

            ecpCooked = new EatableAsset("CookedEmeraldClownPincher", "Cooked emerald clown pincher", "Pre-sautéed.", assetBundle.LoadAsset<GameObject>("ECP_Prefab"), emeraldClownPincher.TechType, new EatableData(true, 30f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("ECP_Cooked"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("ECP_Popup"));
            ecpCooked.Patch();
            ecpCured = new EatableAsset("CuredEmeraldClownPincher", "Cured emerald clown pincher", "Tastes like lettuce. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("ECP_Prefab"), emeraldClownPincher.TechType, new EatableData(true, 30f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("ECP_Cured"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("ECP_Popup"));
            ecpCured.Patch();

            acpCooked = new EatableAsset("CookedAmberClownPincher", "Cooked amber clown pincher", "Not the worst tasting thing on the planet.", assetBundle.LoadAsset<GameObject>("ACP_Prefab"), amberClownPincher.TechType, new EatableData(true, 30f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("ACP_Cooked"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("ACP_Popup"));
            acpCooked.Patch();
            acpCured = new EatableAsset("CuredAmberClownPincher", "Cured amber clown pincher", "Tastes like radish. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("ACP_Prefab"), amberClownPincher.TechType, new EatableData(true, 30f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("ACP_Cured"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("ACP_Popup"));
            acpCured.Patch();

            ccpCooked = new EatableAsset("CookedCitrineClownPincher", "Cooked citrine clown pincher", "The secret is in the claws.", assetBundle.LoadAsset<GameObject>("CCP_Prefab"), citrineClownPincher.TechType, new EatableData(true, 30f, 9f, true), false, assetBundle.LoadAsset<Texture2D>("CCP_Cooked"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("CCP_Popup"));
            ccpCooked.Patch();
            ccpCured = new EatableAsset("CuredCitrineClownPincher", "Cured citrine clown pincher", "Tastes like potatoes. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("CCP_Prefab"), citrineClownPincher.TechType, new EatableData(true, 30f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("CCP_Cured"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("CCP_Popup"));
            ccpCured.Patch();

            axetailCooked = new EatableAsset("CookedAxetail", "Cooked axetail", "A sharp taste. Somewhat hydrating.", assetBundle.LoadAsset<GameObject>("Axetail_Prefab"), axetail.TechType, new EatableData(true, 20f, 13f, true), false, assetBundle.LoadAsset<Texture2D>("Axetail_Cooked"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("Axetail_Popup"));
            axetailCooked.Patch();
            axetailCured = new EatableAsset("CuredAxetail", "Cured axetail", "Eat around the pointy bits. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("Axetail_Prefab"), axetail.TechType, new EatableData(true, 20f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("Axetail_Cured"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("Axetail_Popup"));
            axetailCured.Patch();

            ribbonRayCooked = new EatableAsset("CookedRibbonRay", "Cooked ribbon ray", "A hefty meal.", assetBundle.LoadAsset<GameObject>("RibbonRay_Prefab"), ribbonRay.TechType, new EatableData(true, 36f, 7f, true), false, assetBundle.LoadAsset<Texture2D>("RibbonRay_Cooked"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("RibbonRay_Popup"));
            ribbonRayCooked.Patch();
            ribbonRayCured = new EatableAsset("CuredRibbonRay", "Cured ribbon ray", "Rubbery and stringy. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("RibbonRay_Prefab"), ribbonRay.TechType, new EatableData(true, 36f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("RibbonRay_Cured"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("RibbonRay_Popup"));
            ribbonRayCured.Patch();

            filtorbCooked = new EatableAsset("CookedFiltorb", "Cooked filtorb", "Juicy.", assetBundle.LoadAsset<GameObject>("Filtorb_Prefab"), filtorb.TechType, new EatableData(true, 5f, 20f, true), false, assetBundle.LoadAsset<Texture2D>("Filtorb_Cooked"), ItemSoundsType.StillSuitWater, assetBundle.LoadAsset<Sprite>("Filtorb_Popup"));
            filtorbCooked.Patch();
            filtorbCured = new EatableAsset("CuredFiltorb", "Cured filtorb", "Chalky. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("Filtorb_Prefab"), filtorb.TechType, new EatableData(true, 5f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("Filtorb_Cured"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("Filtorb_Popup"));
            filtorbCured.Patch();

            jellySpinnerCooked = new EatableAsset("CookedJellySpinner", "Cooked jelly spinner", "Pops in your mouth.", assetBundle.LoadAsset<GameObject>("JellySpinner_Prefab"), jellySpinner.TechType, new EatableData(true, 9f, 2f, true), false, assetBundle.LoadAsset<Texture2D>("JellySpinner_Cooked"), ItemSoundsType.StillSuitWater, assetBundle.LoadAsset<Sprite>("JellySpinner_Popup"));
            jellySpinnerCooked.Patch();
            jellySpinnerCured = new EatableAsset("CuredJellySpinner", "Cured jelly spinner", "Like eating bubble wrap. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("JellySpinner_Prefab"), jellySpinner.TechType, new EatableData(true, 9f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("JellySpinner_Cured"), ItemSoundsType.StillSuitWater, assetBundle.LoadAsset<Sprite>("JellySpinner_Popup"));
            jellySpinnerCured.Patch();

            trianglefishCooked = new EatableAsset("CookedTriangleFish", "Cooked trianglefish", "Small yet filling.", assetBundle.LoadAsset<GameObject>("Trianglefish_Prefab"), triangleFish.TechType, new EatableData(true, 22f, 3f, true), false, assetBundle.LoadAsset<Texture2D>("Trianglefish_Cooked"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("Trianglefish_Popup"));
            trianglefishCooked.Patch();
            trianglefishCured = new EatableAsset("CuredTriangleFish", "Cured trianglefish", "Unusually crunchy. Dehydrating, but keeps well.", assetBundle.LoadAsset<GameObject>("Trianglefish_Prefab"), triangleFish.TechType, new EatableData(true, 22f, -2f, false), true, assetBundle.LoadAsset<Texture2D>("Trianglefish_Cured"), ItemSoundsType.Default, assetBundle.LoadAsset<Sprite>("Trianglefish_Popup"));
            trianglefishCured.Patch();
            #endregion

            const float gulperSpawnDistance = 125f;
            //Mountains
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(1169, -370, 903), "Mountains+KooshGulper", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(1400, -348, 1281), "Mountains+KooshGulper2", gulperSpawnDistance));

            //Underwater islands
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-72, -300, 867), "UWIGulper1", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-174, -460, 1070), "UWIGulper2", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-49, -308, 1184), "Mountains+UWIGulper", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-265, -287, 1118), "BK+UWIGulper", gulperSpawnDistance));

            //Shallow
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-717, -100, -1088), "FloatingIslandGulper", gulperSpawnDistance));

            //Blood kelp
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-573, -448, 1311), "Lifepod2Gulper", gulperSpawnDistance));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gulper, new Vector3(-970, -216, -509), "BKTGulper", gulperSpawnDistance));

            Harmony harmony = new Harmony("Lee23.DeExtinctionMod");

            FixSpikeTrap();

            string bonesharkClassId = CraftData.GetClassIdForTechType(TechType.BoneShark);
            string bloomPlanktonClassId = CraftData.GetClassIdForTechType(TechType.Bloom);
            string mohawkClassId = CraftData.GetClassIdForTechType(TechType.Mohawk);
            string jellyrayPrefab = CraftData.GetClassIdForTechType(TechType.Jellyray);
            string plantMiddle11ClassId = "a4ad13d2-8f28-4b0b-abb3-d51cc4271d7a";
            string reefbackCoral01ClassId = "a711c0fa-f31e-4426-9164-a9a65557a9a2";
            string breakableBarnacleClassId = "31ccc496-c26b-4ed9-8e86-3334582d8d5b";
            string barnacleClusterId = "73658f8a-7f66-404e-a645-466bc604e15b";
            string drillableSulphurId = "697beac5-e39a-4809-854d-9163da9f997e";
            string drillableRubyId = "109bbd29-c445-4ad8-a4bf-be7bc6d421d6";
            string drillableDiamondId = "e7c097ac-e7be-4808-aaaa-70178d96f68b";
            string whiteCaveCrawler = "7ce2ca9d-6154-4988-9b02-38f670e741b8";
            string grandReefCrystal = "d0be2a21-7134-4641-a058-20e9da4a9b37";
            LootDistributionHandler.EditLootDistributionData(bonesharkClassId, BiomeType.UnderwaterIslands_OpenDeep_CreatureOnly, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(bonesharkClassId, BiomeType.UnderwaterIslands_ValleyFloor, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(bonesharkClassId, BiomeType.UnderwaterIslands_IslandSides, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(bonesharkClassId, BiomeType.UnderwaterIslands_ValleyLedge, 0f, 0);
            //Bloom plankton
            LootDistributionHandler.EditLootDistributionData(bloomPlanktonClassId, BiomeType.TreeCove_Ground, 0.05f, 1);
            LootDistributionHandler.EditLootDistributionData(bloomPlanktonClassId, BiomeType.TreeCove_Wall, 0.05f, 1);
            LootDistributionHandler.EditLootDistributionData(bloomPlanktonClassId, BiomeType.TreeCove_Ceiling, 0.05f, 1);
            LootDistributionHandler.EditLootDistributionData(bloomPlanktonClassId, BiomeType.TreeCove_TreeOpen_CreatureOnly, 0.25f, 1);
            LootDistributionHandler.EditLootDistributionData(bloomPlanktonClassId, BiomeType.Kelp_CaveFloor, 0.3f, 1);
            LootDistributionHandler.EditLootDistributionData(bloomPlanktonClassId, BiomeType.GrassyPlateaus_CaveCeiling, 0.4f, 1);
            LootDistributionHandler.EditLootDistributionData(bloomPlanktonClassId, BiomeType.DeepGrandReef_Ceiling, 0.25f, 1);
            //Random mohawk stuff
            LootDistributionHandler.EditLootDistributionData(mohawkClassId, BiomeType.DeepGrandReef_BlueCoral, 0.1f, 1);
            LootDistributionHandler.EditLootDistributionData(mohawkClassId, BiomeType.SafeShallows_Plants, 0.3f, 1);
            LootDistributionHandler.EditLootDistributionData(mohawkClassId, BiomeType.KooshZone_CaveFloor, 0.2f, 1);
            LootDistributionHandler.EditLootDistributionData(mohawkClassId, BiomeType.UnderwaterIslands_IslandPlants, 0.5f, 1);
            //Underwater islands
            LootDistributionHandler.EditLootDistributionData(reefbackCoral01ClassId, BiomeType.UnderwaterIslands_IslandPlants, 0.5f, 1);
            //Grand reef deco
            LootDistributionHandler.EditLootDistributionData(reefbackCoral01ClassId, BiomeType.DeepGrandReef_BlueCoral, 0.2f, 1);
            LootDistributionHandler.EditLootDistributionData(whiteCaveCrawler, BiomeType.DeepGrandReef_Ceiling, 1f, 3);
            LootDistributionHandler.EditLootDistributionData(whiteCaveCrawler, BiomeType.DeepGrandReef_Wall, 0.2f, 3);
            LootDistributionHandler.EditLootDistributionData(reefbackCoral01ClassId, BiomeType.GrandReef_Grass, 0.2f, 1);
            LootDistributionHandler.EditLootDistributionData(reefbackCoral01ClassId, BiomeType.GrandReef_Ground, 0.2f, 1);
            LootDistributionHandler.EditLootDistributionData(mohawkClassId, BiomeType.GrandReef_Grass, 0.3f, 1);
            LootDistributionHandler.EditLootDistributionData(mohawkClassId, BiomeType.GrandReef_Ground, 0.1f, 1);
            LootDistributionHandler.EditLootDistributionData(grandReefCrystal, BiomeType.DeepGrandReef_Ceiling, 0.7f, 1);
            LootDistributionHandler.EditLootDistributionData(grandReefCrystal, BiomeType.DeepGrandReef_Wall, 0.1f, 1);
            LootDistributionHandler.EditLootDistributionData(grandReefCrystal, BiomeType.DeepGrandReef_Ground, 0.1f, 1);
            LootDistributionHandler.EditLootDistributionData(jellyrayPrefab, BiomeType.GrandReef_OpenShallow_CreatureOnly, 0.04f, 2);
            LootDistributionHandler.EditLootDistributionData(jellyrayPrefab, BiomeType.GrandReef_OpenDeep_CreatureOnly, 0.02f, 1);
            //Sea Treader's path Twistybridge-ification
            LootDistributionHandler.EditLootDistributionData(breakableBarnacleClassId, BiomeType.SeaTreaderPath_CaveFloor, 2f, 1);
            LootDistributionHandler.EditLootDistributionData(breakableBarnacleClassId, BiomeType.SeaTreaderPath_CaveCeiling, 2f, 1);
            LootDistributionHandler.EditLootDistributionData(breakableBarnacleClassId, BiomeType.SeaTreaderPath_CaveWall, 2f, 1);
            LootDistributionHandler.EditLootDistributionData(barnacleClusterId, BiomeType.SeaTreaderPath_CaveFloor, 1f, 1);
            LootDistributionHandler.EditLootDistributionData(barnacleClusterId, BiomeType.SeaTreaderPath_CaveCeiling, 1f, 1);
            LootDistributionHandler.EditLootDistributionData(barnacleClusterId, BiomeType.SeaTreaderPath_CaveWall, 1f, 1);
            LootDistributionHandler.EditLootDistributionData(mohawkClassId, BiomeType.SeaTreaderPath_CaveFloor, 1.2f, 1);
            LootDistributionHandler.EditLootDistributionData(whiteCaveCrawler, BiomeType.SeaTreaderPath_CaveCeiling, 2f, 1);
            //Drillable crystalline sulphur
            LootDistributionHandler.EditLootDistributionData(drillableSulphurId, BiomeType.GhostTree_Lake_Floor, 0.4f, 1);
            //Dunes
            LootDistributionHandler.EditLootDistributionData(breakableBarnacleClassId, BiomeType.Dunes_Rock, 1.3f, 1);
            LootDistributionHandler.EditLootDistributionData(breakableBarnacleClassId, BiomeType.Dunes_CaveCeiling, 1.3f, 1);
            LootDistributionHandler.EditLootDistributionData(breakableBarnacleClassId, BiomeType.Dunes_CaveFloor, 1.3f, 1);
            LootDistributionHandler.EditLootDistributionData(breakableBarnacleClassId, BiomeType.Dunes_CaveWall, 1.3f, 1);
            LootDistributionHandler.EditLootDistributionData(drillableRubyId, BiomeType.Dunes_SandDune, 0.1f, 1);
            LootDistributionHandler.EditLootDistributionData(drillableRubyId, BiomeType.Dunes_SandPlateau, 0.1f, 1);
            LootDistributionHandler.EditLootDistributionData(drillableDiamondId, BiomeType.Dunes_SandDune, 0.1f, 1);
            LootDistributionHandler.EditLootDistributionData(drillableDiamondId, BiomeType.Dunes_SandPlateau, 0.1f, 1);
            LootDistributionHandler.EditLootDistributionData(plantMiddle11ClassId, BiomeType.Dunes_SandPlateau, 0.4f, 1);
            LootDistributionHandler.EditLootDistributionData(plantMiddle11ClassId, BiomeType.Dunes_SandDune, 0.4f, 1);
            LootDistributionHandler.EditLootDistributionData(plantMiddle11ClassId, BiomeType.Dunes_Grass, 0.4f, 1);

            PatchPlantPDAEntry(TechType.Mohawk, "Mohawk", "Mohawk plant", "A similar, albeit more resilient relative to the Scaly Maw Anemone found on other sections of the planet. Thrives off of abundances of microorganisms found in the water. Lives in both shallow waters and deep cave systems."); 

            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        static void PatchPlantPDAEntry(TechType techType, string key, string name, string desc, float scanTime = 3f)
        {
            PDAEncyclopediaHandler.AddCustomEntry(new PDAEncyclopedia.EntryData()
            {
                path = "Lifeforms/Flora/Sea",
                key = key,
                nodes = new string[] { "Lifeforms", "Flora", "Sea" }
            });
            PDAHandler.AddCustomScannerEntry(new PDAScanner.EntryData()
            {
                key = techType,
                encyclopedia = key,
                scanTime = scanTime,
                isFragment = false
            });
            LanguageHandler.SetLanguageLine("Ency_" + key, name);
            LanguageHandler.SetLanguageLine("EncyDesc_" + key, desc);
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

        static void FixSpikeTrap()
        {
            GameObject obj = Resources.Load<GameObject>("WorldEntities/Doodads/Coral_reef_Light/coral_reef_spiky_trap");
            if (obj == null)
            {
                ECCLog.AddMessage("DE EXTINCTION: Spike trap found at path " + "WorldEntities/Doodads/Coral_reef_Light/coral_reef_spiky_trap");
                return;
            }
            obj.AddComponent<FreezeRigidbodyWhenFar>().freezeDist = 40f;
            obj.AddComponent<EcoTarget>().type = EcoTargetType.Trap;
            TechType spikeTrapTechType = TechTypeHandler.AddTechType("SpikyTrap", "Spikey trap", "A very spikey trap.");
            obj.AddComponent<TechTag>().type = spikeTrapTechType;
            PatchPlantPDAEntry(spikeTrapTechType, "SpikyTrap", "Spikey Trap", "A very large plant with obvious carnivorous adapations.\n\nBehavior:\nUnusually docile. The carnivorous parts may be vestigial, or this individual may have just eaten a large meal.");
        }
    }
}
