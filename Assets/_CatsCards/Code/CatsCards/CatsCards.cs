using BepInEx;
using HarmonyLib;
using InControl;
using Lightsaber;
using PlayerActionsHelper;
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
        public const string Version = "1.0.1";

        private void Awake()
        {
            Harmony harmony = new Harmony(ModId);
            harmony.PatchAll();
            instance = this;
            PlayerActionManager.RegisterPlayerAction(new ActionInfo(Constants.SwitchHoldable, new KeyBindingSource(Key.LeftShift),
                new DeviceBindingSource(InputControlType.DPadDown)));

            assets = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources(Constants.AssetBundle, typeof(CatsCards).Assembly);
            assets.LoadAsset<GameObject>(Constants.ModCards).GetComponent<CardHolder>().RegisterCards();
        }
    }
}