using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using BepInEx;
using HarmonyLib;
using InControl;
using Lightsaber;
using UnityEngine;

namespace CatsCards
{
    [BepInDependency(Constants.CardChoiceSpawnUniqueCardPatch)]
    [BepInDependency(Constants.RarityBundle)]
    [BepInDependency(Constants.ModdingUtils)]
    [BepInDependency(Constants.ActionHelper)]
    [BepInDependency(Constants.UnboundLib)]
    [BepInDependency(Constants.CardThemeLib)]
    [BepInDependency(Constants.RarityLib)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess(Constants.ProcessName)]
    public class CatsCards : BaseUnityPlugin
    {
        internal static AssetBundle assets;
        internal static CatsCards instance;
        internal static readonly string modInitials = "Cats";
        private const string ModId = "com.CatsCards.Cats";
        private const string ModName = "Cats Cards";
        public const string Version = "1.0.2";

#pragma warning disable IDE0051 // Remove unused private members
        private void Awake()
        {
            Harmony harmony = new Harmony(ModId);
            harmony.PatchAll();
            instance = this;
            //PlayerActionManager.RegisterPlayerAction(new ActionInfo(Constants.SwitchHoldable, new KeyBindingSource(Key.LeftShift),
            //    new DeviceBindingSource(InputControlType.DPadDown)));

            assets = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources(Constants.AssetBundle, typeof(CatsCards).Assembly);
            assets.LoadAsset<GameObject>(Constants.ModCards).GetComponent<CardHolder>().RegisterCards();
        }
#pragma warning restore IDE0051 // Remove unused private members
    }
}

[Serializable]
public class PlayerActionsAdditionalData
{
    public PlayerAction SwitchHoldable;


    public PlayerActionsAdditionalData()
    {
        SwitchHoldable = null;
    }
}
public static class PlayerActionsExtension
{
    public static readonly ConditionalWeakTable<PlayerActions, PlayerActionsAdditionalData> data =
        new ConditionalWeakTable<PlayerActions, PlayerActionsAdditionalData>();

    public static PlayerActionsAdditionalData GetAdditionalData(this PlayerActions playerActions)
    {
        return data.GetOrCreateValue(playerActions);
    }

    public static void AddData(this PlayerActions playerActions, PlayerActionsAdditionalData value)
    {
        try
        {
            data.Add(playerActions, value);
        }
        catch (Exception) { }
    }
}


[HarmonyPatch(typeof(PlayerActions))]
[HarmonyPatch(MethodType.Constructor)]
[HarmonyPatch(new Type[] { })]
public static class PlayerActionsPatchPlayerActions
{
    private static void Postfix(PlayerActions __instance)
    {
        __instance.GetAdditionalData().SwitchHoldable = (PlayerAction)typeof(PlayerActions).InvokeMember("CreatePlayerAction",
                                BindingFlags.Instance | BindingFlags.InvokeMethod |
                                BindingFlags.NonPublic, null, __instance, new object[] { "SwitchHoldable" });

    }
}

[HarmonyPatch(typeof(PlayerActions), "CreateWithControllerBindings")]
public static class PlayerActionsPatchCreateWithControllerBindings
{
    private static void Postfix(ref PlayerActions __result)
    {
        __result.GetAdditionalData().SwitchHoldable.AddDefaultBinding(InputControlType.DPadDown);
    }
}

[HarmonyPatch(typeof(PlayerActions), "CreateWithKeyboardBindings")]
public static class PlayerActionsPatchCreateWithKeyboardBindings
{
    private static void Postfix(ref PlayerActions __result)
    {
        __result.GetAdditionalData().SwitchHoldable.AddDefaultBinding(Key.LeftShift);
    }
}
