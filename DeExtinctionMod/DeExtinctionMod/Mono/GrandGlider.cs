using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class GrandGlider : Creature
    {
        public override void Start()
        {
            base.Start();
            ErrorMessage.AddMessage(transform.localScale.ToString());
            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            for(; ; )
            {
                yield return new WaitForSeconds(1f);
                ErrorMessage.AddMessage(GetBestAction().GetType().ToString());
            }
        }
    }
}
