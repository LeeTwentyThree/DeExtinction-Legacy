using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using SMLHelper.V2.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;

namespace DeExtinctionMod.Asset_Classes
{
    public class CreatureEggAsset : Spawnable
    {
        private GameObject model;
        protected GameObject prefab;
        private TechType hatchingCreature;
        private Atlas.Sprite sprite;
        Texture2D spriteTexture;
        static LiveMixinData eggLiveMixinData;

        public CreatureEggAsset(string classId, string friendlyName, string description, GameObject model, TechType hatchingCreature, Texture2D spriteTexture) : base(classId, friendlyName, description)
        {
            this.model = model;
            this.hatchingCreature = hatchingCreature;
            this.spriteTexture = spriteTexture;
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            slotType = EntitySlot.Type.Small,
            cellLevel = LargeWorldEntity.CellLevel.Near,
            classId = ClassID,
            techType = TechType
        };

        new public void Patch()
        {
            sprite = ImageUtils.LoadSpriteFromTexture(spriteTexture);
            if(eggLiveMixinData == null)
            {
                eggLiveMixinData = Helpers.CreateNewLiveMixinData();
                eggLiveMixinData.destroyOnDeath = true;
                eggLiveMixinData.explodeOnDestroy = true;
                eggLiveMixinData.maxHealth = GetMaxHealth;
                eggLiveMixinData.knifeable = true;
            }
            base.Patch();
        }

        public override GameObject GetGameObject()
        {
            if(prefab == null)
            {
                prefab = model;
                prefab.AddComponent<PrefabIdentifier>().ClassId = ClassID;
                prefab.AddComponent<TechTag>().type = TechType;
                prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
                SkyApplier skyApplier = prefab.AddComponent<SkyApplier>();
                skyApplier.renderers = prefab.GetComponentsInChildren<Renderer>();

                Pickupable pickupable = prefab.AddComponent<Pickupable>();

                LiveMixin lm = prefab.AddComponent<LiveMixin>();
                lm.data = eggLiveMixinData;
                lm.health = GetMaxHealth;

                VFXSurface surface = prefab.AddComponent<VFXSurface>();
                surface.surfaceType = VFXSurfaceTypes.organic;

                WaterParkItem waterParkItem = prefab.AddComponent<WaterParkItem>();
                waterParkItem.pickupable = pickupable;

                Rigidbody rb = prefab.EnsureComponent<Rigidbody>();
                rb.mass = 10f;

                WorldForces worldForces = prefab.EnsureComponent<WorldForces>();
                worldForces.useRigidbody = rb;

                CreatureEgg egg = prefab.AddComponent<CreatureEgg>();
                egg.animator = prefab.GetComponentInChildren<Animator>();
                egg.hatchingCreature = hatchingCreature;
                egg.overrideEggType = TechType;

                EntityTag entityTag = prefab.AddComponent<EntityTag>();
                entityTag.slotType = EntitySlot.Type.Small;
                Helpers.ApplySNShaders(prefab);
            }
            return prefab;
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return sprite;
        }

        public virtual float GetMaxHealth
        {
            get
            {
                return 20f;
            }
        }
    }
}
