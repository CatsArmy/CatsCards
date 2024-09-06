//using UnityEngine;

//namespace CatsCards.MonoBehaviors.SquishPlayer
//{

//internal class WideVisualEffect : OnFlipEffect
//{
//    private bool wide;

//    private Vector3 restore_scale;

//    private float signy = 1f;

//    public override void Selected()
//    {
//        if (this.wide)
//            return;
//        this.wide = true;
//        this.restore_scale = base.gameObject.transform.localScale;
//        base.gameObject.transform.localScale = new Vector3(base.gameObject.transform.localScale.x * 1.35f,
//            base.gameObject.transform.localScale.y * 0.45f, base.gameObject.transform.localScale.z);
//    }

//    public override void Unselected()
//    {
//        this.wide = false;
//        base.gameObject.transform.localScale = this.restore_scale;
//    }

//    private void Update()
//    {
//        if (this.wide)
//        {
//            if (Mathf.Abs(this.restore_scale.y * 0.45f - base.gameObject.transform.localScale.y) >= 0.1f)
//                this.signy *= -1f;
//            base.gameObject.transform.localScale = base.gameObject.transform.localScale + new Vector3(0f, this.signy * 0.02f, 0f);
//        }
//    }
//    private void OnDisable() => this.wide = false;
//    private void OnDestroy() => this.wide = false;

//}

//}