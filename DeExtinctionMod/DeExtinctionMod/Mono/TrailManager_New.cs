using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;

namespace DeExtinctionMod.Mono
{
    public class TrailManager_New : TrailManager
    {
		public void LateUpdate()
		{
			float deltaTime = Time.deltaTime;
			CallPrivateMethod("UpdateTrails", new object[] { deltaTime });
			Helpers.SetPrivateField(typeof(TrailManager), this, "lastUpdateTime", Time.time);
		}

		void CallPrivateMethod(string name, object[] methodParams)
		{
			MethodInfo dynMethod = typeof(TrailManager).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
			dynMethod.Invoke(this, methodParams);
		}
	}
}
