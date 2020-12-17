using DeExtinctionMod.AssetClasses;
using DeExtinctionMod.Mono;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Prefabs
{
    public class ThalassaceanPrefab : CreatureAsset
    {
        public ThalassaceanPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {

        }

        public override Vector2int SizeInInventory => new Vector2int(2, 2);

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Medium;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(20f, 0f, 20f), 2f, 2f, 0.5f);

        public override EcoTargetType EcoTargetType => EcoTargetType.Whale;

        public override TechType CreatureTraitsReference => TechType.SeaTreader;

        public override float Mass => 100f;

        public override float TurnSpeed => 0.25f;

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 600f;
            liveMixinData.knifeable = true;
        }

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(1f, false, 5f);

        public override RoarAbilityData RoarAbilitySettings => new RoarAbilityData(true, 2f, 10f, "ThalassaceanRoar", string.Empty, 0.51f, 10f, 25f);

        public override AnimationCurve SizeDistribution => new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(1f, 2.5f) });

        public override GameObject GetGameObject()
        {
            if(prefab == null)
            {
                SetupPrefab(out CreatureComponents<Thalassacean> components);

                CreateTrail(prefab.SearchChild("root"), new Transform[] { prefab.SearchChild("spine1").transform, prefab.SearchChild("spine2").transform, prefab.SearchChild("spine3").transform, prefab.SearchChild("spine4").transform }, components, 1f);

                var varySwimSpeeds = prefab.AddComponent<VaryingSwimSpeeds>();
                varySwimSpeeds.maxIncrease = 3f;
                varySwimSpeeds.variationRate = 0.2f;

                AnimateByVelocity animateByVelocity = prefab.AddComponent<AnimateByVelocity>();
                animateByVelocity.animator = components.creature.GetAnimator();
                animateByVelocity.animationMoveMaxSpeed = 5f;
                animateByVelocity.levelOfDetail = components.behaviourLOD;
                animateByVelocity.rootGameObject = prefab;

                AddMeleeAttack(prefab.SearchChild("Mouth"), 0.35f, 15f, "ThalassaceanBite", 35f, true, components);
                CompletePrefab(components);
            }
            return prefab;
        }

        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.02f, 0.3f, 0.5f, 1.25f, false);
    }
}
