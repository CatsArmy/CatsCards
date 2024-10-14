using HarmonyLib;
using CatsCards.Lightsaber.Extensions;

namespace CatsCards.Lightsaber.Patches
{
    [HarmonyPatch(typeof(Gun))]
    internal class AttackPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Gun.Attack))]
        private static bool ToggleGun(Gun __instance)
        {
            return (!__instance.GetData().disabled);
        }
    }

}
