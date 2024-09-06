using UnityEngine;

namespace Lightsaber.Extensions
{
    public static class PlayerExtensions
    {
        public static Gun GetGun(this Player player)
        {
            return player?.GetComponent<Holding>().holdable.GetComponent<Gun>();
        }
        public static GameObject GetSpring(this Player player)
        {
            return GunExtensions.GetSpring(player.GetGun());
        }
    }
}
