using System;
using System.Runtime.CompilerServices;
using InControl;

namespace CatsCards.Lightsaber.Extensions
{
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
}
