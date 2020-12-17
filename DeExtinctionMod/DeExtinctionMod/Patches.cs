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
    [HarmonyPatch(typeof(LargeWorldStreamer), "Initialize")]
    public static class LargeWorldStreamer_Initialize_Patch
    {
        [HarmonyPostfix()]
        public static void Postfix()
        {
            /*bool newSave = !Directory.Exists(SaveUtils.GetCurrentSaveDataDir());
            string saveDataPath = Path.Combine(SaveUtils.GetCurrentSaveDataDir(), "deextinction.json");
            if (!File.Exists(saveDataPath))
            {
                FileStream fs = File.Create(saveDataPath);
                using(StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(JsonUtility.ToJson(new SaveData()));
                }
                fs.Dispose();
            }
            string json = File.ReadAllText(saveDataPath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            if (newSave || !saveData.GargantuanHasSpawned)
            {
                GameObject prefab = CraftData.GetPrefabForTechType(QPatch.gargantuanPrefab.TechType);
                GameObject.Instantiate(prefab, new Vector3(1250f, -120f, 600f), Quaternion.identity);
                GameObject.Instantiate(prefab, new Vector3(-880f, -200f, -1260f), Quaternion.identity);
            }
            File.WriteAllText(saveDataPath, JsonUtility.ToJson(new SaveData(true)));*/
        }
    }
}
