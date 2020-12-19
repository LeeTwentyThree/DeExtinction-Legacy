using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.AssetClasses
{
    public struct EatableData
    {
        public bool CanBeEaten;
        public float FoodAmount;
        public float WaterAmount;
        public bool Decomposes;

        public EatableData(bool canBeEaten, float foodAmount, float waterAmount, bool decomposes)
        {
            CanBeEaten = canBeEaten;
            FoodAmount = foodAmount;
            WaterAmount = waterAmount;
            Decomposes = decomposes;
        }

        public void MakeItemEatable(GameObject go)
        {
            var eatable = go.EnsureComponent<Eatable>();
            eatable.allowOverfill = true;
            eatable.foodValue = FoodAmount;
            eatable.waterValue = WaterAmount;
            eatable.decomposes = Decomposes;
            eatable.kDecayRate = 0.03f;
        }
    }
}
