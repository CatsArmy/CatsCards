//using HarmonyLib;
//using CatsCards.Lightsaber;
//using UnityEngine;

//namespace CatsCards.MonoBehaviors.Melee
//{
//    [HarmonyPatch(typeof(ApplyCardStats))]
//    public static class ApplyCardStatsPatch
//    {
//        private static Vector2 damage = new Vector2(100f, 0f);

//        [HarmonyPrefix]
//        [HarmonyPatch(nameof(Update))]
//        public static void Update(ApplyCardStats __instance)
//        {
//            if (__instance == null || !__instance.shootToPick)
//                return;
//            GameObject cardObject = __instance.gameObject;
//            GameObject cardBase = cardObject.transform.Find(Constants.CardBase).gameObject;
//            if (cardBase == null)
//                return;
//            GameObject damageable = cardBase.transform.Find(Constants.Damagable).gameObject;
//            if (damageable == null)
//                return;
//            DamagableEvent damageableEvent = damageable.GetComponent<DamagableEvent>();
//            BoxCollider2D collider = damageable.GetComponent<BoxCollider2D>();

//            var weapons = GameObject.FindObjectsOfType<A_HoldableHandler>();
//            if (weapons is null)
//                return;

//            foreach (var weapon in weapons)
//            {
//                if (weapon is null)
//                    continue;
//                Player player = weapon.Player;
//                if (player is null)
//                    continue;
//                ObjectSlash slash = weapon.ObjectSlash;
//                if (!slash.IsAttacking(collider))
//                    continue;
//                damageableEvent.TakeDamage(damage, damage, weapon.Weapon, player);
//                //damageableEvent.lastPlayer = player;
//                //damageableEvent.dead = true;
//            }
//        }
//    }
//}