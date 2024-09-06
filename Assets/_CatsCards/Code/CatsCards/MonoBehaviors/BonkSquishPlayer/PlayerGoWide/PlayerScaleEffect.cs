//using UnityEngine;

//namespace CatsCards.MonoBehaviors.SquishPlayer
//{
//    public class PlayerScaleEffect : MonoBehaviour
//    {
//        public bool Wide;
//        public bool Lank;
//        public bool IsMult;
//        public float RatioOffset;
//        public float yOffset;

//        internal float Ratio = 0.5f;
//        internal Vector3 RestoreScale = Vector3.zero;
//        private readonly float UpdateTimeDelay = 0.5f;
//        private float StartTime;

//        private void Start()
//        {
//            this.Ratio = this.IsMult ? this.Ratio * this.RatioOffset : this.Ratio + this.RatioOffset;

//            this.RestoreScale = base.gameObject.transform.localScale;
//            foreach (object obj in base.gameObject.transform.parent.GetChild(4))
//            {
//                ((Transform)obj).localPosition += new Vector3(0f, this.yOffset, 0f);
//            }
//            this.ResetScale();
//            this.Effect();
//            this.StartTime = Time.time;
//        }

//        private void Update()
//        {
//            if (this.StartTime + this.UpdateTimeDelay > Time.time)
//            {
//                return;
//            }
//            this.StartTime = Time.time;
//            Vector3 LocalScale = base.gameObject.transform.localScale;
//            if (LocalScale.x != LocalScale.y)
//            {
//                return;
//            }
//            this.RestoreScale = base.gameObject.transform.localScale;
//            this.Effect();
//        }

//        private void Effect()
//        {
//            this.Lanken();
//            this.Widen();
//        }

//        internal void Lanken()
//        {
//            if (!this.Lank)
//            {
//                return;
//            }
//            Vector3 LocalScale = base.gameObject.transform.localScale;
//            if (Mathf.Abs(LocalScale.x / LocalScale.y - this.Ratio) < 0.0001f)
//            {
//                return;
//            }
//            base.gameObject.transform.localScale = 1.25f * new Vector3(LocalScale.x * this.Ratio,
//                LocalScale.x, LocalScale.z);
//            base.gameObject.transform.localPosition = new Vector3(0f, this.yOffset, 0f);
//        }

//        internal void Widen()
//        {
//            if (!this.Wide)
//            {
//                return;
//            }
//            Vector3 LocalScale = base.gameObject.transform.localScale;
//            if (Mathf.Abs(LocalScale.y / LocalScale.x - this.Ratio) < 0.0001f)
//            {
//                return;
//            }
//            base.gameObject.transform.localScale = 1.25f * new Vector3(LocalScale.x,
//                LocalScale.x * this.Ratio, LocalScale.z);
//            base.gameObject.transform.localPosition = new Vector3(0f, this.yOffset, 0f);
//        }
//        internal void ResetScale()
//        {
//            if (this.RestoreScale == Vector3.zero)
//            {
//                return;
//            }

//            base.gameObject.transform.localScale = this.RestoreScale;
//        }
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