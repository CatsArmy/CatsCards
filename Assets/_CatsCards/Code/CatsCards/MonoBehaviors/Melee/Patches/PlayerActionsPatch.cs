using System;
using System.Reflection;
using CatsCards.Lightsaber.Extensions;
using HarmonyLib;
using InControl;

namespace CatsCards.Lightsaber.Patches
{
    [HarmonyPatch(typeof(PlayerActions))]
    public static class PlayerActionsPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, new Type[] { })]
        private static void CreatePlayerAction(PlayerActions __instance)
        {
            __instance.GetAdditionalData().SwitchHoldable = (PlayerAction)typeof(PlayerActions).InvokeMember(nameof(CreatePlayerAction),
                                    BindingFlags.Instance | BindingFlags.InvokeMethod |
                                    BindingFlags.NonPublic, null, __instance, new object[] { nameof(PlayerActionsAdditionalData.SwitchHoldable) });
        }


        [HarmonyPostfix]
        [HarmonyPatch(nameof(CreateWithKeyboardBindings))]
        private static void CreateWithKeyboardBindings(ref PlayerActions __result)
        {
            __result.GetAdditionalData().SwitchHoldable.AddDefaultBinding(new KeyBindingSource(Key.LeftShift));
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CreateWithControllerBindings))]
        private static void CreateWithControllerBindings(ref PlayerActions __result)
        {
            __result.GetAdditionalData().SwitchHoldable.AddDefaultBinding(new DeviceBindingSource(InputControlType.DPadDown));
        }
    }
}
