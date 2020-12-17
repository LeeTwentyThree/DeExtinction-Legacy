using DeExtinctionMod.Mono;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using SMLHelper.V2.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;
using HarmonyLib;

namespace DeExtinctionMod.AssetClasses
{
    public abstract class CreatureAsset : Spawnable
    {
        private GameObject model;
        private Atlas.Sprite sprite;

        protected GameObject prefab;
        protected List<CreatureAction> creatureActions;

        public CreatureAsset(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description)
        {
            this.model = model;
            if (spriteTexture != null)
            {
                sprite = ImageUtils.LoadSpriteFromTexture(spriteTexture);
            }
        }

        protected void SetupPrefab<CreatureType>(out CreatureComponents<CreatureType> creatureComponents) where CreatureType : Creature
        {
            prefab = model;
            creatureActions = new List<CreatureAction>();
            creatureComponents = SetupNecessaryComponents<CreatureType>();
            Helpers.ApplySNShaders(prefab);
        }
        protected void CompletePrefab<CreatureType>(CreatureComponents<CreatureType> components) where CreatureType : Creature
        {
            PropertyInfo prop = typeof(Creature).GetProperty("actions", BindingFlags.Default);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(components.creature, creatureActions, null);
            }
        }
        protected override Atlas.Sprite GetItemSprite()
        {
            return sprite;
        }
        protected virtual void PostPatch()
        {

        }
        public MeleeAttack_New AddMeleeAttack<CreatureType>(GameObject mouth, float biteInterval, float damage, string biteSoundPrefix, float consumeWholeHealthThreshold, bool regurgitateLater, CreatureComponents<CreatureType> components) where CreatureType: Creature
        {
            OnTouch onTouch = mouth.EnsureComponent<OnTouch>();
            onTouch.gameObject.EnsureComponent<Rigidbody>().isKinematic = true;
            MeleeAttack_New meleeAttack = prefab.AddComponent<MeleeAttack_New>();
            meleeAttack.mouth = mouth;
            meleeAttack.creature = components.creature;
            meleeAttack.liveMixin = components.liveMixin;
            meleeAttack.animator = components.creature.GetAnimator();
            meleeAttack.biteInterval = biteInterval;
            meleeAttack.lastTarget = components.lastTarget;
            meleeAttack.biteDamage = damage;
            meleeAttack.eatHappyIncrement = 0f;
            meleeAttack.eatHungerDecrement = 0.4f;
            meleeAttack.biteAggressionThreshold = 0f;
            meleeAttack.biteAggressionDecrement = 0.2f;
            meleeAttack.onTouch = onTouch;
            meleeAttack.biteSoundPrefix = biteSoundPrefix;
            meleeAttack.consumeWholeHealthThreshold = consumeWholeHealthThreshold;
            meleeAttack.regurgitate = regurgitateLater;
            return meleeAttack;
        }
        /// <summary>
        /// Messy component that does most of the work.
        /// </summary>
        /// <typeparam name="CreatureType"></typeparam>
        /// <returns></returns>
        private CreatureComponents<CreatureType> SetupNecessaryComponents<CreatureType>() where CreatureType : Creature
        {
            CreatureComponents<CreatureType> components = new CreatureComponents<CreatureType>();
            components.prefabIdentifier = prefab.EnsureComponent<PrefabIdentifier>();
            components.prefabIdentifier.ClassId = ClassID;

            components.techTag = prefab.EnsureComponent<TechTag>();
            components.techTag.type = TechType;

            components.largeWorldEntity = prefab.EnsureComponent<LargeWorldEntity>();
            components.largeWorldEntity.cellLevel = CellLevel;

            components.entityTag = prefab.EnsureComponent<EntityTag>();
            components.entityTag.slotType = EntitySlot.Type.Creature;

            components.skyApplier = prefab.AddComponent<SkyApplier>();
            components.skyApplier.renderers = prefab.GetComponentsInChildren<Renderer>();

            components.ecoTarget = prefab.AddComponent<EcoTarget>();
            components.ecoTarget.type = EcoTargetType;

            components.vfxSurface = prefab.EnsureComponent<VFXSurface>();
            components.vfxSurface.surfaceType = SurfaceType;

            components.behaviourLOD = prefab.EnsureComponent<BehaviourLOD>();
            components.behaviourLOD.veryCloseThreshold = BehaviourLODSettings.Close;
            components.behaviourLOD.closeThreshold = BehaviourLODSettings.Close;
            components.behaviourLOD.farThreshold = BehaviourLODSettings.Far;

            components.rigidbody = prefab.EnsureComponent<Rigidbody>();
            components.rigidbody.useGravity = false;
            components.rigidbody.mass = Mass;

            components.locomotion = prefab.AddComponent<Locomotion>();
            components.locomotion.useRigidbody = components.rigidbody;
            components.splineFollowing = prefab.AddComponent<SplineFollowing>();
            components.splineFollowing.respectLOD = false;
            components.splineFollowing.locomotion = components.locomotion;
            components.swimBehaviour = prefab.AddComponent<SwimBehaviour>();
            components.swimBehaviour.splineFollowing = components.splineFollowing;
            components.swimBehaviour.turnSpeed = TurnSpeed;

            components.lastScarePosition = prefab.AddComponent<LastScarePosition>();

            components.worldForces = prefab.EnsureComponent<WorldForces>();
            components.worldForces.useRigidbody = components.rigidbody;
            components.worldForces.handleGravity = true;
            components.worldForces.underwaterGravity = UnderwaterGravity;
            components.worldForces.aboveWaterGravity = AboveWaterGravity;

            components.liveMixin = prefab.EnsureComponent<LiveMixin>();
            components.liveMixin.data = Helpers.CreateNewLiveMixinData();
            SetLiveMixinData(ref components.liveMixin.data);
            if (components.liveMixin.data.maxHealth <= 0f)
            {
                ErrorMessage.AddMessage("A creature should not have a max health of zero or below.");
            }

            Creature reference = CraftData.GetPrefabForTechType(CreatureTraitsReference).GetComponent<Creature>();
            components.creature = prefab.AddComponent<CreatureType>();
            reference.CopyFields(components.creature);
            components.creature.liveMixin = components.liveMixin;
            Helpers.SetPrivateField(typeof(Creature), components.creature, "traitsAnimator", components.creature.GetComponentInChildren<Animator>());
            components.creature.sizeDistribution = SizeDistribution;

            RoarAbility roar = null;
            if (!string.IsNullOrEmpty(RoarAbilitySettings.AudioClipPrefix))
            {
                roar = prefab.AddComponent<RoarAbility>();
                roar.minRoarDistance = RoarAbilitySettings.MinRoarDistance;
                roar.maxRoarDistance = RoarAbilitySettings.MaxRoarDistance;
                roar.animationName = RoarAbilitySettings.AnimationName;
                roar.clipPrefix = RoarAbilitySettings.AudioClipPrefix;
                if (RoarAbilitySettings.RoarActionPriority > 0f)
                {
                    RoarRandomAction roarAction = prefab.AddComponent<RoarRandomAction>();
                    roarAction.roarIntervalMin = RoarAbilitySettings.MinTimeBetweenRoars;
                    roarAction.roarIntervalMax = RoarAbilitySettings.MaxTimeBetweenRoars;
                    roarAction.evaluatePriority = RoarAbilitySettings.RoarActionPriority;
                    creatureActions.Add(roarAction);
                }
            }
            components.lastTarget = prefab.AddComponent<LastTarget_New>();
            components.lastTarget.roar = roar;
            if (EnableAggression)
            {
                if(AggressivenessToSmallVehicles.Aggression > 0f)
                {
                    AggressiveToPilotingVehicle atpv = prefab.AddComponent<AggressiveToPilotingVehicle>();
                    atpv.aggressionPerSecond = AggressivenessToSmallVehicles.Aggression;
                    atpv.range = AggressivenessToSmallVehicles.MaxRange;
                    atpv.creature = components.creature;
                    atpv.lastTarget = components.lastTarget;
                }
                if(AttackSettings.EvaluatePriority > 0f)
                {
                    AttackLastTarget actionAtkLastTarget = prefab.AddComponent<AttackLastTarget>();
                    actionAtkLastTarget.evaluatePriority = AttackSettings.EvaluatePriority;
                    actionAtkLastTarget.swimVelocity = AttackSettings.ChargeVelocity;
                    actionAtkLastTarget.aggressionThreshold = 0.02f;
                    actionAtkLastTarget.minAttackDuration = AttackSettings.MinAttackLength;
                    actionAtkLastTarget.maxAttackDuration = AttackSettings.MaxAttackLength;
                    actionAtkLastTarget.pauseInterval = AttackSettings.AttackInterval;
                    actionAtkLastTarget.rememberTargetTime = AttackSettings.RememberTargetTime;
                    actionAtkLastTarget.priorityMultiplier = Helpers.Curve_Flat();
                    actionAtkLastTarget.lastTarget = components.lastTarget;
                    creatureActions.Add(actionAtkLastTarget);
                }
            }
            components.swimRandom = prefab.AddComponent<SwimRandom>();
            components.swimRandom.swimRadius = SwimRandomSettings.SwimRadius;
            components.swimRandom.swimVelocity = SwimRandomSettings.SwimVelocity;
            components.swimRandom.swimInterval = SwimRandomSettings.SwimInterval;
            components.swimRandom.evaluatePriority = SwimRandomSettings.EvaluatePriority;
            components.swimRandom.priorityMultiplier = Helpers.Curve_Flat();
            if (AvoidObstaclesSettings.evaluatePriority > 0f)
            {
                AvoidObstacles avoidObstacles = prefab.AddComponent<AvoidObstacles>();
                avoidObstacles.avoidTerrainOnly = AvoidObstaclesSettings.terrainOnly;
                avoidObstacles.avoidanceDistance = AvoidObstaclesSettings.avoidDistance;
                avoidObstacles.scanDistance = AvoidObstaclesSettings.avoidDistance;
                avoidObstacles.priorityMultiplier = Helpers.Curve_Flat();
                avoidObstacles.evaluatePriority = AvoidObstaclesSettings.evaluatePriority;
            }
            creatureActions.Add(components.swimRandom);
            if (CanBeInfected)
            {
                components.infectedMixin = prefab.AddComponent<InfectedMixin>();
                components.infectedMixin.renderers = prefab.GetComponentsInChildren<Renderer>();
            }

            return components;
        }
        new public void Patch()
        {
            base.Patch();
            WaterParkCreature.waterParkCreatureParameters.Add(TechType, WaterParkParameters);
            if (ScannableSettings.scannable)
            {
                PDAEncyclopediaHandler.AddCustomEntry(new PDAEncyclopedia.EntryData()
                {
                    key = ClassID,
                    nodes = ScannableSettings.encyNodes,
                    path = ScannableSettings.encyPath,
                    image = ScannableSettings.encyImage,
                    popup = ScannableSettings.popup
                });
                PDAHandler.AddCustomScannerEntry(new PDAScanner.EntryData()
                {
                    key = TechType,
                    encyclopedia = ClassID,
                    scanTime = ScannableSettings.scanTime,
                    isFragment = false
                });
                LanguageHandler.SetLanguageLine("Ency_" + ClassID, GetEncyTitle);
                LanguageHandler.SetLanguageLine("EncyDesc_" + ClassID, GetEncyDesc);
            }
            PostPatch();
        }
        protected void CreateTrail<CreatureType>(GameObject trailParent, CreatureComponents<CreatureType> components, float segmentSnapSpeed, float maxSegmentOffset = -1f) where CreatureType : Creature
        {
            TrailManager_New trail = trailParent.AddComponent<TrailManager_New>();
            trail.trails = trailParent.transform.GetChild(0).GetComponentsInChildren<Transform>();
            trail.rootTransform = prefab.transform;
            trail.rootSegment = trail.transform;
            trail.levelOfDetail = components.behaviourLOD;
            trail.segmentSnapSpeed = segmentSnapSpeed;
            trail.maxSegmentOffset = maxSegmentOffset;
            trail.allowDisableOnScreen = false;
            AnimationCurve decreasing = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.25f), new Keyframe(1f, 0.75f) });
            trail.pitchMultiplier = decreasing;
            trail.rollMultiplier = decreasing;
            trail.yawMultiplier = decreasing;
        }
        protected void CreateTrail<CreatureType>(GameObject trailRoot, Transform[] trails, CreatureComponents<CreatureType> components, float segmentSnapSpeed, float maxSegmentOffset = -1f) where CreatureType : Creature
        {
            TrailManager_New trail = trailRoot.AddComponent<TrailManager_New>();
            trail.trails = trails;
            trail.rootTransform = prefab.transform;
            trail.rootSegment = trail.transform;
            trail.levelOfDetail = components.behaviourLOD;
            trail.segmentSnapSpeed = segmentSnapSpeed;
            trail.maxSegmentOffset = maxSegmentOffset;
            trail.allowDisableOnScreen = false;
            AnimationCurve decreasing = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.25f), new Keyframe(1f, 0.75f) });
            trail.pitchMultiplier = decreasing;
            trail.rollMultiplier = decreasing;
            trail.yawMultiplier = decreasing;
        }

        public abstract LargeWorldEntity.CellLevel CellLevel
        {
            get;
        }

        public virtual AnimationCurve SizeDistribution
        {
            get
            {
                return Helpers.Curve_Flat();
            }
        }

        public virtual string GetEncyTitle
        {
            get
            {
                return "no title";
            }
        }

        public virtual bool CanBeInfected
        {
            get
            {
                return true;
            }
        }

        public virtual string GetEncyDesc
        {
            get
            {
                return "no description";
            }
        }

        public abstract SwimRandomData SwimRandomSettings { get; }

        /// <summary>
        /// By default, the creature does not roar.
        /// </summary>
        public virtual RoarAbilityData RoarAbilitySettings { get; }

        /// <summary>
        /// How aggressive the creature is to small vehicles. If set to zero (default value), it will not be aggressive to small vehicles.
        /// </summary>
        public virtual SmallVehicleAggressivenessSettings AggressivenessToSmallVehicles
        {
            get
            {
                return new SmallVehicleAggressivenessSettings(0f, 0f);
            }
        }
        public virtual AttackLastTargetSettings AttackSettings
        {
            get
            {
                return new AttackLastTargetSettings(0f, 0f, 0f, 0f, 0f, 0f);
            }
        }
        public abstract EcoTargetType EcoTargetType
        {
            get;
        }

        public virtual bool EnableAggression
        {
            get
            {
                return false;
            }
        }

        public virtual WaterParkCreatureParameters WaterParkParameters
        {
            get
            {
                return default;
            }
        }

        public virtual float Mass
        {
            get
            {
                return 1f;
            }
        }

        public virtual float AboveWaterGravity
        {
            get
            {
                return 9.81f;
            }
        }

        public virtual float UnderwaterGravity
        {
            get
            {
                return 0f;
            }
        }

        public virtual VFXSurfaceTypes SurfaceType
        {
            get
            {
                return VFXSurfaceTypes.organic;
            }
        }

        public abstract void SetLiveMixinData(ref LiveMixinData liveMixinData);

        public virtual BehaviourLODLevelsStruct BehaviourLODSettings
        {
            get
            {
                return new BehaviourLODLevelsStruct(30f, 60f, 100f);
            }
        }

        /// <summary>
        /// The creature used for reference. Easier than declaring all the stats manually.
        /// </summary>
        public abstract TechType CreatureTraitsReference { get; }

        /// <summary>
        /// Normalized value. Determines how fast the creature turns while swimming.
        /// </summary>
        public virtual float TurnSpeed
        {
            get
            {
                return 1f;
            }
        }

        /// <summary>
        /// The FOV used for detecting things, such as targets, on a scale from 0f to 1f. -1f means it can see everything.
        /// </summary>
        public virtual float EyeFov
        {
            get
            {
                return 0.25f;
            }
        }

        public virtual ScannableCreatureData ScannableSettings
        {
            get
            {
                return new ScannableCreatureData();
            }
        }

        public virtual AvoidObstaclesData AvoidObstaclesSettings
        {
            get
            {
                return new AvoidObstaclesData(0f, false, 0f);
            }
        }

        public struct BehaviourLODLevelsStruct
        {
            public float VeryClose;
            public float Close;
            public float Far;

            public BehaviourLODLevelsStruct(float veryClose, float close, float far)
            {
                VeryClose = veryClose;
                Close = close;
                Far = far;
            }
        }

        public struct AttackLastTargetSettings
        {
            public float EvaluatePriority;
            public float ChargeVelocity;
            public float MinAttackLength;
            public float MaxAttackLength;
            public float AttackInterval;
            public float RememberTargetTime;

            public AttackLastTargetSettings(float actionPriority, float chargeVelocity, float minAttackLength, float maxAttackLength, float attackInterval, float rememberTargetTime)
            {
                EvaluatePriority = actionPriority;
                ChargeVelocity = chargeVelocity;
                MinAttackLength = minAttackLength;
                MaxAttackLength = maxAttackLength;
                AttackInterval = attackInterval;
                RememberTargetTime = rememberTargetTime;
            }
        }
        
        public struct AvoidObstaclesData
        {
            public bool terrainOnly;
            public float avoidDistance;
            public float evaluatePriority;

            public AvoidObstaclesData(float actionPriority, bool terrainOnly, float avoidDistance)
            {
                this.evaluatePriority = actionPriority;
                this.terrainOnly = terrainOnly;
                this.avoidDistance = avoidDistance;
            }
        }

        public struct SmallVehicleAggressivenessSettings
        {
            public float Aggression;
            public float MaxRange;

            public SmallVehicleAggressivenessSettings(float aggression, float maxRange)
            {
                Aggression = aggression;
                MaxRange = maxRange;
            }
        }
        public struct RoarAbilityData
        {
            public bool CanRoar;
            public float MinRoarDistance;
            public float MaxRoarDistance;
            /// <summary>
            /// All sounds starting with AudioClipPrefix in the asset bundle have a possibility to be played.
            /// </summary>
            public string AudioClipPrefix;
            public string AnimationName;
            public float MinTimeBetweenRoars;
            public float MaxTimeBetweenRoars;
            public float RoarActionPriority;

            public RoarAbilityData(bool canRoar, float roarSoundFalloffStart, float roarSoundMaxDistance, string audioClipPrefix, string animationName, float roarActionPriority = 0.5f, float minTimeBetweenRoars = 4f, float maxTimeBetweenRoars = 8f)
            {
                CanRoar = canRoar;
                MinRoarDistance = roarSoundFalloffStart;
                MaxRoarDistance = roarSoundMaxDistance;
                AudioClipPrefix = audioClipPrefix;
                AnimationName = animationName;
                MinTimeBetweenRoars = minTimeBetweenRoars;
                MaxTimeBetweenRoars = maxTimeBetweenRoars;
                RoarActionPriority = roarActionPriority;
            }
        }

        public struct SwimRandomData
        {
            public bool SwimRandomly;
            public Vector3 SwimRadius;
            public float SwimVelocity;
            public float SwimInterval;
            public float EvaluatePriority;

            public SwimRandomData(bool swimRandomly, Vector3 swimRadius, float swimVelocity, float swimInterval, float priority)
            {
                SwimRandomly = swimRandomly;
                SwimRadius = swimRadius;
                SwimVelocity = swimVelocity;
                SwimInterval = swimInterval;
                EvaluatePriority = priority;
            }
        }
        public struct CreatureComponents<CreatureType> where CreatureType : Creature
        {
            public PrefabIdentifier prefabIdentifier;
            public TechTag techTag;
            public LargeWorldEntity largeWorldEntity;
            public EntityTag entityTag;
            public SkyApplier skyApplier;
            public Renderer renderer;
            public EcoTarget ecoTarget;
            public VFXSurface vfxSurface;
            public BehaviourLOD behaviourLOD;
            public Rigidbody rigidbody;
            public LastScarePosition lastScarePosition;
            public WorldForces worldForces;
            public CreatureType creature;
            public LiveMixin liveMixin;
            public LastTarget_New lastTarget;
            public SwimBehaviour swimBehaviour;
            public Locomotion locomotion;
            public SplineFollowing splineFollowing;
            public SwimRandom swimRandom;
            public InfectedMixin infectedMixin;
        }
        public struct ScannableCreatureData
        {
            public bool scannable;
            public float scanTime;
            public string encyPath;
            public string[] encyNodes;
            public Sprite popup;
            public Texture2D encyImage;

            public ScannableCreatureData(bool scannable, float scanTime, string encyPath, string[] encyNodes, Sprite popup, Texture2D encyImage)
            {
                this.scannable = scannable;
                this.scanTime = scanTime;
                this.encyPath = encyPath;
                this.encyNodes = encyNodes;
                this.popup = popup;
                this.encyImage = encyImage;
            }
        }
    }
}
