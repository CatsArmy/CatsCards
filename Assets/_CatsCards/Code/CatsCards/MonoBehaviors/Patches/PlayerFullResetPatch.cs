//using System;
//using HarmonyLib;
//using Lightsaber;
////using DoggoBonk.MonoBehaviors.Squish.Lanky;
//using UnityEngine;

//namespace CatsCards.MonoBehaviors.SquishPlayer
//{
//    [Serializable]
//    [HarmonyPatch(typeof(Player), "FullReset")]
//    internal class PlayerFullResetPatch
//    {
//        private static void Prefix(Player __instance)
//        {
//            foreach (WidePlayerEffect widePlayerEffect in __instance.gameObject.GetComponentsInChildren<WidePlayerEffect>())
//            {
//                if (widePlayerEffect != null)
//                {
//                    GameObject.Destroy(widePlayerEffect);
//                }
//            }
//            foreach (LankyPlayerEffect lankyPlayerEffect in __instance.gameObject.GetComponentsInChildren<LankyPlayerEffect>())
//            {
//                if (lankyPlayerEffect != null)
//                {
//                    GameObject.Destroy(lankyPlayerEffect);
//                }
//            }
//            __instance.gameObject.transform.GetChild(3).gameObject.GetComponentInChildren<LegRaycasters>().force = 1000f;

//            GameObject.Destroy(__instance.GetComponentInChildren<A_HoldableObject>());


//        }


//    }
//}
