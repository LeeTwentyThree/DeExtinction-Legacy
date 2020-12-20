using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;

namespace DeExtinctionMod.Mono
{
    public class GargantuanLeviathan : Creature
    {
        GameObject darkModel;
        GameObject glowingModel;
        LastTarget lastTarget;

        public override void Start()
        {
            base.Start();
            darkModel = gameObject.SearchChild("Model_Dark");
            glowingModel = gameObject.SearchChild("Model_Emissive");
            lastTarget = GetComponent<LastTarget>();
            StartCoroutine(UpdateGlowing());
        }

        IEnumerator UpdateGlowing()
        {
            bool targetExisted = false;
            for (; ; )
            {
                yield return new WaitForSeconds(1.5f);
                bool targetExists = lastTarget.target != null;
                if(targetExists != targetExisted)
                {
                    bool active = targetExisted;
                    for(int i = 0; i < 7; i++)
                    {
                        SetModelState(active);
                        active = !active;
                        yield return new WaitForSeconds(0.1f);
                    }
                    SetModelState(targetExists);
                }
                targetExisted = targetExists;
            }
        }
        private void SetModelState(bool glowing)
        {
            glowingModel.SetActive(glowing);
            darkModel.SetActive(!glowing);
        }
    }
}
