//using UnityEngine;

//namespace CatsCards.MonoBehaviors.SquishPlayer
//{
//    internal class WidePlayerEffect : MonoBehaviour
//    {
//        private float startTime = -1f;

//        internal float ratio = 0.5f;

//        public bool isMult;

//        public float ratioOffset;

//        public float yOffset;

//        internal Vector3 restore_scale = Vector3.zero;

//        private void Start()
//        {
//            this.ratio = this.isMult ? this.ratio * this.ratioOffset : this.ratio + this.ratioOffset;

//            this.restore_scale = base.gameObject.transform.localScale;
//            foreach (object obj in base.gameObject.transform.parent.GetChild(4))
//            {
//                ((Transform)obj).localPosition += new Vector3(0f, this.yOffset, 0f);
//            }
//            this.ResetScale();
//            this.Widen();
//            this.ResetTimer();
//        }

//        private void Update()
//        {
//            if (Time.time < this.startTime + 0.5f)
//            {
//                return;
//            }
//            this.ResetTimer();
//            if (base.gameObject.transform.localScale.x != base.gameObject.transform.localScale.y)
//            {
//                return;
//            }
//            this.restore_scale = base.gameObject.transform.localScale;
//            this.Widen();
//        }

//        internal void Widen()
//        {
//            if (Mathf.Abs(base.gameObject.transform.localScale.y / base.gameObject.transform.localScale.x - this.ratio) < 0.0001f)
//            {
//                return;
//            }
//            base.gameObject.transform.localScale = 1.25f * new Vector3(base.gameObject.transform.localScale.x,
//                base.gameObject.transform.localScale.x * this.ratio, base.gameObject.transform.localScale.z);
//            base.gameObject.transform.localPosition = new Vector3(0f, this.yOffset, 0f);
//        }
//        internal void ResetScale()
//        {
//            if (this.restore_scale == Vector3.zero)
//            {
//                return;
//            }

//            base.gameObject.transform.localScale = this.restore_scale;
//        }

//        private void ResetTimer() => this.startTime = Time.time;

//        private void OnDestroy()
//        {
//            this.ResetScale();
//            base.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
//            foreach (object obj in base.gameObject.transform.parent.GetChild(4))
//            {
//                ((Transform)obj).localPosition -= new Vector3(0f, this.yOffset, 0f);
//            }
//        }

//    }
//}