using ECCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DeExtinctionMod.Mono;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class FiltorbPrefab : CreatureAsset
    {
        public FiltorbPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override TechType CreatureTraitsReference => TechType.SeaTreader;

        public override BehaviourType BehaviourType => BehaviourType.SmallFish;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Near;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData();

        public override EcoTargetType EcoTargetType => EcoTargetType.SmallFish;

        public override bool Pickupable => true;

        public override EatableData EatableSettings => new EatableData(true, 2f, 0f, false);

        public override HeldFishData ViewModelSettings => new HeldFishData(TechType.Peeper, "WorldModel", "ViewModel");

        public override float Mass => 5f;

        public override float BioReactorCharge => 300f;

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 3f, "Lifeforms/Fauna/SmallHerbivores", new string[] { "Lifeforms", "Fauna", "SmallHerbivores" }, QPatch.assetBundle.LoadAsset<Sprite>("Filtorb_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("Filtorb_Ency"));
        public override string GetEncyDesc => "Small, free-floating filter feeder with a tough shell.\n\nBody Plan:\nSix appendages used for filter feeding branch off from a soft central chamber.A calcified shell encases the body, and can seal shut if necessary.\n\nBehavior: While generally stationary within the water column, Filtorbs will close up when other lifeforms come near for protection.\n\nAssessment: Edible, though mostly water.";

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            var hide = prefab.AddComponent<FiltorbHide>();
            hide.actionLength = 0.5f;
            hide.maxReactDistance = 14f;
            hide.evaluatePriority = 0.2f;
            var freeFloat = prefab.AddComponent<FreeFloating>();
            freeFloat.evaluatePriority = 0.1f;
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 130f;
        }
    }
}
