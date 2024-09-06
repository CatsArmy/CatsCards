using System.Runtime.CompilerServices;
using UnityEngine;

namespace Lightsaber.Extensions
{
    public class GunAdditionalData
    {
        public bool disabled = false;
    }

    public static class GunExtensions
    {
        private static readonly ConditionalWeakTable<Gun, GunAdditionalData> additionalData =
            new ConditionalWeakTable<Gun, GunAdditionalData>();

        public static GunAdditionalData GetData(this Gun instance)
        {
            return additionalData.GetOrCreateValue(instance);
        }

        public static void DisableGun(this Gun instance)
        {
            instance.GetData().disabled = true;
        }

        public static void EnableGun(this Gun instance)
        {
            instance.GetData().disabled = false;
        }

        public static GameObject GetSpring(this Gun gun)
        {
            return gun.transform.Find(Constants.Spring).gameObject;
        }

        public static RightLeftMirrorSpring GetSpringMirror(this Gun gun)
        {
            return GameObjectExtensions.GetSpringMirror(gun.GetSpring());
        }
    }
}