using BepInEx;
using CatsCards.Lightsaber;
using HarmonyLib;
using UnboundLib.GameModes;
using UnityEngine;

namespace CatsCards
{
    [BepInDependency(Constants.CardChoiceSpawnUniqueCardPatch)]
    [BepInDependency(Constants.RarityBundle)]
    [BepInDependency(Constants.CardThemeLib)]
    [BepInDependency(Constants.ModdingUtils)]
    [BepInDependency(Constants.UnboundLib)]
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
        public const string Version = "1.0.4";

#pragma warning disable IDE0051 // Remove unused private members
        private void Awake()
        {
            Harmony harmony = new Harmony(ModId);
            harmony.PatchAll();
            instance = this;
            GameModeManager.AddHook(GameModeHooks.HookBattleStart, ObjectSlash.Enable);
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, ObjectSlash.Disable);
            assets = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources(Constants.AssetBundle, typeof(CatsCards).Assembly);
            assets.LoadAsset<GameObject>(Constants.ModCards).GetComponent<CardHolder>().RegisterCards();

            //this.ExecuteAfterFrames(20, () =>
            //{
            //    StartCoroutine(nameof(SteamIdCheck));
            //});
        }
#pragma warning restore IDE0051 // Remove unused private members
        //private IEnumerator SteamIdCheck()
        //{
        //    while (!SteamManager.Initialized)
        //    {
        //        yield return null;
        //    }
        //    //if ($"{SteamUser.GetSteamID().m_SteamID}" == "76561199140062399")
        //    //Import Steamworks
        //}

        public void OnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookPointEnd, ObjectSlash.Disable);
            GameModeManager.RemoveHook(GameModeHooks.HookPointStart, ObjectSlash.Enable);
        }
    }
}
