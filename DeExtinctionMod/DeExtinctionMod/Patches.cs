using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using SMLHelper.V2.Utility;
using System.IO;
using UnityEngine;
using ECCLibrary.Internal;
using ECCLibrary;
using System.Runtime.Remoting.Messaging;
using System;

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

    [HarmonyPatch(typeof(LargeWorldStreamer), "Initialize")]
    public static class LargeWorldStreamer_Initialize_Patch
    {
        [HarmonyPostfix()]
        public static void Postfix()
        {
            bool flag = false;
            string directory = SaveUtils.GetCurrentSaveDataDir();
            ECCLog.AddMessage("Directory: " + directory);
            if (!Directory.Exists(directory))
            {
                flag = true;
            }
            else
            {
                string deExtinctionFile = Path.Combine(directory, "DeExtinctionData");
                if (!File.Exists(deExtinctionFile))
                {
                    FileStream stream = File.Create(deExtinctionFile);
                    stream.Dispose();
                    flag = true;
                }
            }
            if (flag)
            {
                GameObject gulperPrefab = QPatch.gulper.GetGameObject();
                SpawnPrefabAtLocation(gulperPrefab, new Vector3(1000f, 0f, 1000f));
                SpawnPrefabAtLocation(gulperPrefab, Vector3.zero);
            }
        }

        static void SpawnPrefabAtLocation(GameObject prefab, Vector3 position)
        {
            GameObject obj = UWE.Utils.InstantiateDeactivated(prefab, position, Quaternion.identity);
            LargeWorldEntity lwe = obj.GetComponent<LargeWorldEntity>();
            LargeWorld.main.streamer.LoadBatch(LargeWorld.main.streamer.cellManager.GetGlobalCell(position, (int)lwe.cellLevel));
            LargeWorld.main.streamer.cellManager.RegisterEntity(lwe);
            //obj.SetActive(active);
            //ECCLog.AddMessage("Is active: " + active);
        }
    }
}
