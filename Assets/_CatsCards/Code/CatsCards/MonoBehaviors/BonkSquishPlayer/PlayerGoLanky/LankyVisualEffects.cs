//using UnityEngine;

//namespace CatsCards.MonoBehaviors.SquishPlayer
//{
//    internal class LankyVisualEffect : OnFlipEffect
//    {
//        private bool lanky;

//        private Vector3 restore_scale;

//        private float signy = 1f;

//        public override void Selected()
//        {
//            if (this.lanky)
//            {
//                return;
//            }
//            this.lanky = true;
//            this.restore_scale = base.gameObject.transform.localScale;
//            base.gameObject.transform.localScale = new Vector3(base.gameObject.transform.localScale.x * 0.6f,
//                base.gameObject.transform.localScale.y * 1.1f,
//                base.gameObject.transform.localScale.z);
//        }

//        public override void Unselected()
//        {
//            this.lanky = false;
//            base.gameObject.transform.localScale = this.restore_scale;
//        }

//        private void Update()
//        {
//            if (!this.lanky)
//            {
//                return;
//            }
//            if (Mathf.Abs(this.restore_scale.y * 1.1f - base.gameObject.transform.localScale.y) >= 0.1f)
//            {
//                this.signy *= -1f;
//            }

//            base.gameObject.transform.localScale += new Vector3(0f,
//            this.signy * 0.02f, 0f);
//        }

//        private void OnDisable() => this.lanky = false;

//        private void OnDestroy() => this.lanky = false;
//    }
//}
