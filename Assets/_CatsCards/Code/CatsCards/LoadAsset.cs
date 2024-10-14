using UnityEngine;

namespace CatsCards
{
    public class LoadAsset : MonoBehaviour
    {
        public GameObject Object;
        public GameObject InstantiateObject(Transform transform)
        {
            this.Object = Instantiate(this.Object.name, transform);
            return this.Object;
        }
        private static GameObject Instantiate(string ObjectName, Transform transform)
        {
            GameObject Object = GameObject.Instantiate(CatsCards.assets.LoadAsset<GameObject>(ObjectName), transform);
            Object.name = ObjectName;
            return Object;
        }
    }
}
