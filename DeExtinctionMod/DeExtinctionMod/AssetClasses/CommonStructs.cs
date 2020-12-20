using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Assets;

namespace DeExtinctionMod.AssetClasses
{
    public struct EatableData
    {
        public bool CanBeEaten;
        public float FoodAmount;
        public float WaterAmount;
        public bool Decomposes;

        public EatableData(bool canBeEaten, float foodAmount, float waterAmount, bool decomposes)
        {
            CanBeEaten = canBeEaten;
            FoodAmount = foodAmount;
            WaterAmount = waterAmount;
            Decomposes = decomposes;
        }

        public void MakeItemEatable(GameObject go)
        {
            var eatable = go.EnsureComponent<Eatable>();
            eatable.allowOverfill = true;
            eatable.foodValue = FoodAmount;
            eatable.waterValue = WaterAmount;
            eatable.decomposes = Decomposes;
            eatable.kDecayRate = 0.03f;
        }
    }
    public struct ScannableItemData
    {
        public bool scannable;
        public float scanTime;
        public string encyPath;
        public string[] encyNodes;
        public Sprite popup;
        public Texture2D encyImage;

        public ScannableItemData(bool scannable, float scanTime, string encyPath, string[] encyNodes, Sprite popup, Texture2D encyImage)
        {
            this.scannable = scannable;
            this.scanTime = scanTime;
            this.encyPath = encyPath;
            this.encyNodes = encyNodes;
            this.popup = popup;
            this.encyImage = encyImage;
        }

        public void AttemptPatch(ModPrefab prefab, string encyTitle, string encyDesc)
        {
            PDAEncyclopediaHandler.AddCustomEntry(new PDAEncyclopedia.EntryData()
            {
                key = prefab.ClassID,
                nodes = encyNodes,
                path = encyPath,
                image = encyImage,
                popup = popup
            });
            PDAHandler.AddCustomScannerEntry(new PDAScanner.EntryData()
            {
                key = prefab.TechType,
                encyclopedia = prefab.ClassID,
                scanTime = scanTime,
                isFragment = false
            });
            LanguageHandler.SetLanguageLine("Ency_" + prefab.ClassID, encyTitle);
            LanguageHandler.SetLanguageLine("EncyDesc_" + prefab.ClassID, encyDesc);
        }
    }
}
