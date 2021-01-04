using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECCLibrary;
using UnityEngine;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class TwisteelPrefab : CreatureAsset
    {
        public TwisteelPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override TechType CreatureTraitsReference => TechType.BoneShark;

        public override BehaviourType BehaviourType => BehaviourType.Shark;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Medium;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(20f, 5f, 20f), 5f, 1f, 0.1f);

        public override EcoTargetType EcoTargetType => EcoTargetType.Shark;

        public override SmallVehicleAggressivenessSettings AggressivenessToSmallVehicles => new SmallVehicleAggressivenessSettings(0.3f, 15f);

        public override bool EnableAggression => true;

        public override float MaxVelocityForSpeedParameter => 10f;

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.3f, 8f, 3f, 6f, 5f, 15f);

        public override float BioReactorCharge => 630f;

        public override Vector2int SizeInInventory => new Vector2int(3, 3);

        public override float Mass => 200f;

        public override float TurnSpeed => 0.6f;

        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.01f, 0.4f, 0.6f, 1.5f, false);

        public override string GetEncyDesc => "A large eel-like predator found within a deep canyon.\n\n1. Body:\nA long and flexible body allows the Twisteel to snake around the environment with a low profile while hunting for prey.\n\n2. Jaws:\nDistantly related to other lifeforms on the planet possessing a quad-jaw arrangement, the lateral pair of jaws have been reduced to a vestigial point.The remaining jaws reach lengths of up to 3m, and are filled with rows of large teeth to trap prey items.\n\nAssessment: Avoid";

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 7f, "Lifeforms/Fauna/Carnivores", new string[] { "Lifeforms", "Fauna", "Carnivores" }, QPatch.assetBundle.LoadAsset<Sprite>("Twisteel_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("Twisteel_Ency"));

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            AddMeleeAttack(prefab.SearchChild("Mouth", ECCStringComparison.Contains), 1f, 30f, "TwisteelBite", 30f, false, components);
            GameObject trailParent = prefab.SearchChild("Spine3_phys");
            Transform[] trails = new Transform[] { prefab.SearchChild("Spine4_phys").transform, prefab.SearchChild("Spine5_phys").transform, prefab.SearchChild("Spine6_phys").transform, prefab.SearchChild("Spine7_phys").transform, prefab.SearchChild("Spine8_phys").transform, prefab.SearchChild("Spine9_phys").transform, prefab.SearchChild("Spine10_phys").transform, prefab.SearchChild("Spine11_phys").transform, prefab.SearchChild("Spine12_phys").transform, prefab.SearchChild("Spine13_phys").transform, prefab.SearchChild("Spine14_phys").transform, prefab.SearchChild("Spine15_phys").transform, prefab.SearchChild("Spine16_phys").transform, prefab.SearchChild("Spine17_phys").transform, prefab.SearchChild("Spine18_phys").transform };
            CreateTrail(trailParent, trails, components, 3f);
            MakeAggressiveTo(15f, 1, EcoTargetType.Shark, 0f, 0.5f);
            MakeAggressiveTo(25f, 1, EcoTargetType.SmallFish, 0.1f, 0.4f);
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 250f;
        }
    }
}
