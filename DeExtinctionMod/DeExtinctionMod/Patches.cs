using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using SMLHelper.V2.Utility;
using System.IO;
using UnityEngine;

namespace DeExtinctionMod
{
    [HarmonyPatch(typeof(SwimInSchool), "IsValidLeader")]
    public static class SwimInSchool_IsValidLeader_Patch
    {
        [HarmonyPostfix()]
        public static void Postfix(ref bool __result, SwimInSchool __instance, IEcoTarget target)
        {
            if (__instance.gameObject == target.GetGameObject())
            {
                __result = false;
            }
        }
    }
}
