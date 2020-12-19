using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.AssetClasses
{
    public class EatableAsset : Craftable
    {
        TechType originalFish;
        GameObject model;
        EatableData eatableData;
        bool cured;
        GameObject prefab;
        Atlas.Sprite sprite;

        public EatableAsset(string classId, string friendlyName, string description, GameObject model, TechType originalFish, EatableData eatableData, bool cured, Texture2D sprite) : base(classId, friendlyName, description)
        {
            this.model = model;
            this.originalFish = originalFish;
            this.eatableData = eatableData;
            this.cured = cured;
            this.sprite = ImageUtils.LoadSpriteFromTexture(sprite);
        }

        protected override TechData GetBlueprintRecipe()
        {
            if (cured)
            {
                return new TechData() { Ingredients = new List<Ingredient>() { new Ingredient(originalFish, 1), new Ingredient(TechType.Salt, 1) }, craftAmount = 1 };
            }
            else
            {
                return new TechData() { Ingredients = new List<Ingredient>() { new Ingredient(originalFish, 1) }, craftAmount = 1 };
            }
        }

        public override GameObject GetGameObject()
        {
            if(prefab == null)
            {
                prefab = GameObject.Instantiate(model);
                prefab.SetActive(false);
                Helpers.ApplySNShaders(prefab);
                prefab.AddComponent<TechTag>().type = TechType;
                prefab.AddComponent<PrefabIdentifier>().ClassId = ClassID;
                prefab.AddComponent<Pickupable>();
                prefab.SearchChild("CraftModel").AddComponent<VFXFabricating>();
                eatableData.MakeItemEatable(prefab);
            }
            return prefab;
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return sprite;
        }

        public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;

        public override string[] StepsToFabricatorTab
        {
            get
            {
                if (cured)
                {
                    return new string[] { "Survival", "CuredFood" };
                }
                else
                {
                    return new string[] { "Survival", "CookedFood" };
                }
            }
        }
    }
}
