using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using SMLHelper.V2.Assets;

namespace DeExtinctionMod
{
    public static class Helpers
    {
        public static void ApplySNShaders(GameObject prefab)
        {
            var renderers = prefab.GetComponentsInChildren<Renderer>();
            var newShader = Shader.Find("MarmosetUBER");
            for (int i = 0; i < renderers.Length; i++)
            {
                for (int j = 0; j < renderers[i].materials.Length; j++)
                {
                    Material material = renderers[i].materials[j];
                    material.shader = newShader;

                    Texture specularTexture = material.GetTexture("_SpecGlossMap");
                    if (specularTexture != null)
                    {
                        material.SetTexture("_SpecTex", specularTexture);
                        material.SetFloat("_SpecInt", 2f);
                        material.SetFloat("_Shininess", material.GetFloat("_Glossiness") * 2f);
                        material.EnableKeyword("MARMO_SPECMAP");
                        material.SetColor("_SpecColor", new Color(0.796875f, 0.796875f, 0.796875f, 0.796875f));
                        material.SetFloat("_Fresnel", 0f);
                        material.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
                    }
                    Texture emissionTexture = material.GetTexture("_EmissionMap");
                    if (emissionTexture || material.name.Contains("illum"))
                    {
                        material.EnableKeyword("MARMO_EMISSION");
                        material.SetFloat("_EnableGlow", 1f);
                        material.SetTexture("_Illum", emissionTexture);
                    }

                    if (material.GetTexture("_BumpMap"))
                    {
                        material.EnableKeyword("_NORMALMAP");
                    }
                }
            }
        }
        public static SwimBehaviour EssentialComponentSystem_Swimming(GameObject prefab, float turnSpeed, Rigidbody rb)
        {
            Locomotion locomotion = prefab.AddComponent<Locomotion>();
            locomotion.useRigidbody = rb;
            SplineFollowing splineFollow = prefab.AddComponent<SplineFollowing>();
            splineFollow.respectLOD = false;
            splineFollow.locomotion = locomotion;
            SwimBehaviour swim = prefab.AddComponent<SwimBehaviour>();
            swim.splineFollowing = splineFollow;
            swim.turnSpeed = turnSpeed;
            return swim;
        }
        public static BehaviourLOD EssentialComponent_BehaviourLOD(GameObject prefab, float near, float medium, float far)
        {
            BehaviourLOD bLod = prefab.AddComponent<BehaviourLOD>();
            bLod.veryCloseThreshold = near;
            bLod.closeThreshold = medium;
            bLod.farThreshold = far;
            return bLod;
        }
        public static AnimationCurve Curve_Trail()
        {
            return new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.25f), new Keyframe(1f, 0.75f) });
        }
        public static AnimationCurve Curve_Flat(float value = 1f)
        {
            return new AnimationCurve(new Keyframe[] { new Keyframe(0f, value), new Keyframe(1f, value) });
        }

        public static LiveMixinData CreateNewLiveMixinData()
        {
            return ScriptableObject.CreateInstance<LiveMixinData>();
        }

        public static void SetPrivateField<T>(Type type, T instance, string name, object value)
        {
            var prop = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            prop.SetValue(instance, value);
        }
        public static OutputT GetPrivateField<OutputT>(Type type, object target, string name)
        {
            var prop = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            return (OutputT)prop.GetValue(target);
        }
        public static AggressiveWhenSeeTarget MakeAggressiveTo(GameObject obj, float maxRange, EcoTargetType ecoTarget, float hungerThreshold, float aggressionSpeed)
        {
            AggressiveWhenSeeTarget aggressiveWhenSeeTarget = obj.AddComponent<AggressiveWhenSeeTarget>();
            aggressiveWhenSeeTarget.maxRangeMultiplier = Helpers.Curve_Flat();
            aggressiveWhenSeeTarget.distanceAggressionMultiplier = Helpers.Curve_Flat();
            aggressiveWhenSeeTarget.maxRangeScalar = maxRange;
            aggressiveWhenSeeTarget.maxSearchRings = 3;
            aggressiveWhenSeeTarget.lastScarePosition = obj.GetComponent<LastScarePosition>();
            aggressiveWhenSeeTarget.lastTarget = obj.GetComponent<LastTarget>();
            aggressiveWhenSeeTarget.targetType = ecoTarget;
            aggressiveWhenSeeTarget.hungerThreshold = hungerThreshold;
            aggressiveWhenSeeTarget.aggressionPerSecond = aggressionSpeed;
            return aggressiveWhenSeeTarget;
        }
    }
    public static class GameObjectExtensions
    {
        public static GameObject SearchChild(this GameObject gameObject, string byName)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.name == byName)
                {
                    return child.gameObject;
                }
                GameObject recursive = SearchChild(child.gameObject, byName);
                if (recursive)
                {
                    return recursive;
                }
            }
            return null;
        }
    }
}
