//using System;
//using CardChoiceSpawnUniqueCardPatch.CustomCategories;
//using UnboundLib;
//using UnboundLib.Cards;
//using UnityEngine;

//namespace JokeCards.MonoBehaviors.BonkSquishPlayer
//{
//    public abstract class OnFlipEffect : MonoBehaviour
//    {
//        public abstract void Selected();

//        public abstract void Unselected();
//        public static CardCategory onFlipEffectCategory = CustomCardCategories.instance.CardCategory("__OnFlipEffect__");
//    }


//    public class LankyCard : CustomCard
//    {
//        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
//        {
//            cardInfo.categories = new CardCategory[]
//            {
//                OnFlipEffect.onFlipEffectCategory
//            };
//            cardInfo.allowMultiple = false;
//        }
//        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
//        {
//            characterStats.jump *= 2f;
//            characterStats.movementSpeed *= 1.75f;
//            LankyPlayerEffect orAddComponent = player.gameObject.transform.GetChild(0).gameObject.GetOrAddComponent<LankyPlayerEffect>(false);
//            orAddComponent.ratio *= 0.5f;
//            orAddComponent.yOffset += 0.1f;
//            orAddComponent.ResetScale();
//            orAddComponent.Lanken();
//            player.gameObject.transform.GetChild(3).gameObject.GetComponentInChildren<LegRaycasters>().force *= 2f;
//        }
//        public override void OnRemoveCard()
//        {
//        }
//        protected override string GetTitle()
//        {
//            return "lanky";
//        }
//        protected override string GetDescription()
//        {
//            return "lanky";
//        }
//        protected override GameObject GetCardArt()
//        {
//            //not importing art just for showing off the code...
//            return null;//JokeCards.ArtAssets.LoadAsset<GameObject>("C_Lanky");
//        }
//        protected override CardInfo.Rarity GetRarity()
//        {
//            return 0;
//        }
//        protected override CardInfoStat[] GetStats()
//        {
//            return new CardInfoStat[]
//            {
//                new CardInfoStat
//                {
//                    positive = true,
//                    stat = "Jump Height",
//                    amount = "+100%",//im too lazy to not use parsing and thigure out what stat it was
//                    simepleAmount = (CardInfoStat.SimpleAmount)Enum.Parse(typeof(CardInfoStat.SimpleAmount),"4")
//                },
//                new CardInfoStat
//                {
//                    positive = true,
//                    stat = "Movement Speed",
//                    amount = "+75%",
//                    simepleAmount = (CardInfoStat.SimpleAmount)Enum.Parse(typeof(CardInfoStat.SimpleAmount),"3")
//                },
//                new CardInfoStat
//                {
//                    positive = true,
//                    stat = "lanky",
//                    amount = "Become",
//                    simepleAmount = 0//OH BUT OF CORSE THIS ONE DOESNT ERROR OUT GOD DAMIT
//                }
//            };
//        }
//        protected override CardThemeColor.CardThemeColorType GetTheme()
//        {
//            return 0;
//        }
//        public override string GetModName()
//        {
//            return "YACCM";
//        }
//        public override void Callback()
//        {
//            //werid method he had ExtensionMethods.GetOrAddComponent<LankyCard.LankyVisualEffect>(base.gameObject, false);
//            base.gameObject.GetOrAddComponent<LankyCard.LankyVisualEffect>(false);
//        }

//        internal class LankyPlayerEffect : MonoBehaviour
//        {
//            private void Start()
//            {
//                this.orig_scale = base.gameObject.transform.localScale;
//                foreach (object obj in base.gameObject.transform.parent.GetChild(4))
//                {
//                    ((Transform)obj).localPosition += new Vector3(0f, this.yOffset, 0f);
//                }
//                this.Lanken();
//                this.ResetTimer();
//            }
//            private void Update()
//            {
//                if (Time.time >= this.startTime + 0.5f)
//                {
//                    this.ResetTimer();
//                    if (base.gameObject.transform.localScale.x == base.gameObject.transform.localScale.y)
//                    {
//                        this.orig_scale = base.gameObject.transform.localScale;
//                        this.Lanken();
//                    }
//                }
//            }
//            internal void Lanken()
//            {
//                if (Mathf.Abs(base.gameObject.transform.localScale.x / base.gameObject.transform.localScale.y - this.ratio) >= 0.0001f)
//                {
//                    base.gameObject.transform.localScale = 1.25f * new Vector3(base.gameObject.transform.localScale.x * this.ratio, base.gameObject.transform.localScale.x, base.gameObject.transform.localScale.z);
//                    base.gameObject.transform.localPosition = new Vector3(0f, this.yOffset, 0f);
//                }
//            }
//            internal void ResetScale()
//            {
//                if (this.orig_scale != Vector3.zero)
//                {
//                    base.gameObject.transform.localScale = this.orig_scale;
//                }
//            }
//            private void ResetTimer()
//            {
//                this.startTime = Time.time;
//            }
//            private void OnDestroy()
//            {
//                this.ResetScale();
//                base.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
//                foreach (object obj in base.gameObject.transform.parent.GetChild(4))
//                {
//                    ((Transform)obj).localPosition -= new Vector3(0f, this.yOffset, 0f);
//                }
//            }
//            private const float updateDelay = 0.5f;
//            private float startTime = -1f;
//            internal float ratio = 1f;
//            internal float yOffset;
//            private const float flatScale = 1.25f;
//            internal Vector3 orig_scale = Vector3.zero;
//        }
//        internal class LankyVisualEffect : OnFlipEffect
//        {
//            private void Start() { }
//            public override void Selected()
//            {
//                if (this.lanky)
//                {
//                    return;
//                }
//                this.lanky = true;
//                this.orig_scale = base.gameObject.transform.localScale;
//                base.gameObject.transform.localScale = new Vector3(base.gameObject.transform.localScale.x * 0.6f, base.gameObject.transform.localScale.y * 1.1f, base.gameObject.transform.localScale.z);
//            }
//            public override void Unselected()
//            {
//                this.lanky = false;
//                base.gameObject.transform.localScale = this.orig_scale;
//            }
//            private void Update()
//            {
//                if (this.lanky)
//                {
//                    if (Mathf.Abs(this.orig_scale.y * 1.1f - base.gameObject.transform.localScale.y) >= 0.1f)
//                    {
//                        this.signy *= -1f;
//                    }
//                    base.gameObject.transform.localScale = base.gameObject.transform.localScale + new Vector3(0f, this.signy * 0.02f, 0f);
//                }
//            }
//            private void OnDisable()
//            {
//                this.lanky = false;
//            }
//            private void OnDestroy()
//            {
//                this.lanky = false;
//            }
//            private bool lanky;
//            private Vector3 orig_scale;
//            private const float xmult = 0.6f;
//            private const float ymult = 1.1f;
//            private const float dy = 0.02f;
//            private const float maxDeltaY = 0.1f;
//            private float signy = 1f;
//        }
//    }
//}