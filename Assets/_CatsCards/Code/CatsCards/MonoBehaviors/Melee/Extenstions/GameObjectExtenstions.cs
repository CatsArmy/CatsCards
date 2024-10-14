using UnityEngine;

namespace CatsCards.Lightsaber.Extensions
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponentInChildren<T>(this GameObject go) where T : Component
        {
            T component = go.GetComponentInChildren<T>();
            if (component == null)
            {
                component = go.AddComponent<T>();
            }
            return component;
        }

        public static RightLeftMirrorSpring GetSpringMirror(this GameObject spring)
        {
            return spring.transform.GetChild(2).GetComponent<RightLeftMirrorSpring>();
        }
    }
}
