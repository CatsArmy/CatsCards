using Lightsaber.Extensions;
using UnboundLib;
using UnboundLib.Networking;
using UnityEngine;
using UnityEngine.Events;

namespace Lightsaber
{

    public class A_HoldableObject : MonoBehaviour
    {
        public UnityEvent TriggerVFX;
        public LoadAsset asset;

        internal A_HoldableHandler Handler;
        internal GameObject Weapon;
        private Player Player;

        private const float Volume = 1f;
        internal const float SwitchDelay = 0.25f;
        internal const float ResetSwitchTimer = 0f;
        internal float SwitchTimer
        {
            get;
            private set;
        } = 0f;
        private float StabTimer = 0f;
        internal bool IsOut
        {
            get;
            private set;
        } = false;
        private bool wasOut = false;

        private void Start()
        {
            this.IsOut = false;
            this.StabTimer = 0f;
            this.SwitchTimer = 0f;
            this.Player = this.GetComponentInParent<Player>();
            this.Weapon = this.asset.InstantiateObject(this.Player.GetSpring().transform);
            this.Handler = this.gameObject.GetOrAddComponent<A_HoldableHandler>().Initialize(this,
                this.Player, Constants.Lightsaber);
        }

        private void OnDisable()
        {
            if (this.Player is null || !this.Player.data.view.IsMine)
                return;

            if (this.IsOut && !this.wasOut)
            {
                this.IsOut = false;
                this.wasOut = true;
                NetworkingManager.RPC(typeof(A_HoldableObject), nameof(RPCA_Switch_To_Holdable),
                    this.Player.playerID, false, false);
            }
        }

        private void OnEnable()
        {
            if (this.Player is null || !this.Player.data.view.IsMine || this.IsOut || !this.wasOut)
            {
                return;
            }

            this.IsOut = true;
            this.wasOut = false;
            NetworkingManager.RPC(typeof(A_HoldableObject), nameof(RPCA_Switch_To_Holdable),
                this.Player.playerID, true, false);
        }

        private void Update()
        {
            if (this.Player is null)
                return;

            bool IsClient = this.Player.data.view.IsMine;
            if (!IsClient)
                return;

            if (this.Player.data.dead)
                return;

            this.StabTimer -= TimeHandler.deltaTime;
            this.SwitchTimer -= TimeHandler.deltaTime;
            if (this.SwitchTimer > ResetSwitchTimer)
                return;

            PlayerActions playerActions = this.Player.data.playerActions;
            var value = playerActions.GetAdditionalData().SwitchHoldable.WasPressed;

            if (value)
                return;

            var holdableObject = this;
            holdableObject.SwitchTimer = holdableObject.IsOut ? ResetSwitchTimer : SwitchDelay;
            holdableObject.IsOut = !holdableObject.IsOut;
            if (holdableObject.IsOut)
            {
                holdableObject.TriggerVFX.Invoke();
            }
            NetworkingManager.RPC(typeof(A_HoldableObject), nameof(RPCA_Switch_To_Holdable),
                this.Player.playerID, this.IsOut, true);
        }

        [UnboundRPC]
        internal static void RPCA_Switch_To_Holdable(int stabbingPlayerID, bool holdableObj, bool playSFX)
        {
            MakeGunHoldable(stabbingPlayerID, holdableObj, playSFX);
        }

        private static void MoveToHide(Transform transform, bool hide)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, hide ? -10000f : 0f);
        }

        internal static void MakeGunHoldable(int playerID, bool holdableObj, bool playSFX = false)
        {
            Player player = PlayerManager.instance.GetPlayerWithID(playerID);
            if (player is null)
            {
                return;
            }

            A_HoldableObject holdableObject = player.GetComponentInChildren<A_HoldableObject>();
            //holdableObject.SwitchTimer = holdableObject.IsOut ? ResetSwitchTimer : SwitchDelay;
            //holdableObject.IsOut = !holdableObject.IsOut;
            //if (holdableObject.IsOut)
            //{
            //    holdableObject.TriggerVFX.Invoke();
            //}
            A_HoldableHandler Handler = holdableObject.gameObject.GetComponent<A_HoldableHandler>();
            Handler.Weapon.SetActive(holdableObj);
            GameObject spring = player.GetSpring();
            MoveToHide(spring.transform.Find(Constants.AmmoCanvas), holdableObj);
            MoveToHide(spring.transform.GetChild(2), holdableObj);
            MoveToHide(spring.transform.GetChild(3), holdableObj);
            spring.transform.GetChild(2).GetComponent<RightLeftMirrorSpring>().enabled = !holdableObj;
            spring.transform.GetChild(3).GetComponent<RightLeftMirrorSpring>().enabled = !holdableObj;
            player.GetGun().GetData().disabled = holdableObj;
        }

        private void OnDestroy()
        {
            if (this.Player is null)
            {
                return;
            }
            NetworkingManager.RPC(typeof(A_HoldableObject), nameof(RPCA_Switch_To_Holdable),
                this.Player.playerID, false, false);
            GameObject.Destroy(Handler);
            //GameObject Weapon = this.Player.GetSpring().transform.Find(Constants.Lightsaber)?.gameObject;
            //GameObject.Destroy(Weapon);
            //GameObject.Destroy(this.Weapon);
        }
    }
}
