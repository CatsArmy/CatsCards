using System;
using System.Linq;

namespace Lightsaber.Extensions
{
    public static class PlayerManagerExtensions
    {
        public static Player GetPlayerWithID(this PlayerManager instance, int playerID)
        {
            return instance.players.FirstOrDefault(p => p.playerID == playerID);
        }

        public static void ForEachPlayer(this PlayerManager instance, Action<Player> action)
        {
            foreach (Player player in instance.players)
            {
                action(player);
            }
        }

        public static RightLeftMirrorSpring GetSpringMirror(this Player player)
        {
            return GunExtensions.GetSpringMirror(player.GetGun());
        }
    }
}