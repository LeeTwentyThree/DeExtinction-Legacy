using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;

namespace DeExtinctionMod.Prefabs.Creatures
{
    public class ClownPincherSapphire : ClownPincherPrefab
    {
        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 2f, "Lifeforms/Fauna/Scavengers", new string[] { "Lifeforms", "Fauna", "Scavengers" }, QPatch.assetBundle.LoadAsset<Sprite>("SCP_Popup"), QPatch.assetBundle.LoadAsset<Texture2D>("SCP_Ency"));

        public override string GetEncyDesc => "A small colorful scavenger found in deep waters.\n\nColoration:\nColoration appears to mimic the surrounding ocean, with white stripes to break up the pattern.\n\nBehavior:\nA social species, the Sapphire Clown Pincher can be found forming loose shoals while foraging.\n\nSpecimen shows high genetic diversity suggesting many extant, closely related species that frequently mate.\n\nAssessment: Edible";

        public ClownPincherSapphire(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }
    }
}
