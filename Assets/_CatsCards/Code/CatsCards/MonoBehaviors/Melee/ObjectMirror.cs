using UnityEngine;

namespace CatsCards.Lightsaber
{
    public class ObjectMirror : MonoBehaviour
    {
        internal A_HoldableObject holdableObject;
        private Holdable holdable;

        private static readonly Vector3 LeftPos = new Vector3(0.1f, 1.95f, 0f);
        private static readonly Vector3 RightPos = new Vector3(0.4f, 1.95f, 0f);
        private static readonly Vector3 LeftScale = new Vector3(-1f, 1f, 1f);
        private static readonly Vector3 RightScale = new Vector3(-1f, -1f, 1f);
        internal Vector3 positionMod = Vector3.zero;
        private const float LeftRot = 300f;
        private const float RightRot = 225f;
        internal float rotMod = 0f;

        private void Start()
        {
            this.holdable = base.transform.root.GetComponent<Holdable>();
        }

        private void Update()
        {
            if (this.holdable is null)
                return;
            if (this.holdable.holder is null)
                return;

            bool ScaleDirection = this.transform.root.position.x - 0.1f < this.holdable.holder.transform.position.x;
            this.transform.localScale = (ScaleDirection ? LeftScale : RightScale);
            float rot = (ScaleDirection ? LeftRot : RightRot) + (rotMod * (ScaleDirection ? -1f : 1f));
            this.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rot));
            Vector3 pos = (ScaleDirection ? LeftPos : RightPos) + (new Vector3(positionMod.x * (ScaleDirection ? -1f : 1f), positionMod.y, positionMod.z));
            this.transform.localPosition = pos;
        }
    }
}
