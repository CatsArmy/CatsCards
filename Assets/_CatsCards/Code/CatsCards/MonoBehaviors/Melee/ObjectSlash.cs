using System.Collections;
using System.Collections.Generic;
using CatsCards.Lightsaber.Extensions;
using UnboundLib;
using UnboundLib.GameModes;
using UnboundLib.Networking;
using UnityEngine;

namespace CatsCards.Lightsaber
{
    public class ObjectSlash : MonoBehaviour
    {
        public static bool CanAttack = false;

        internal A_HoldableObject holdableObject;
        internal GameObject OnHitEffect;

        private const float slashAnimationDuration = 0.25f;
        private const float slashCooldown = slashAnimationDuration + 0.05f;
        private float sinceSlash = slashCooldown;

        private bool canDoDamage = false;
        private bool isSlashing = false;

        private Holdable Holdable;
        internal Player Player;
        internal ObjectCollider Collider;
        internal ObjectMirror Mirror;
        private GeneralInput Input;
        //TODO refactor animation
        private static readonly AnimationCurve[] SlashCurves = new AnimationCurve[] {
            new AnimationCurve(new Keyframe[] { // X-Axis, which is Y-Axis due to rotation
                new Keyframe(0,0,1,1,1,1),
                new Keyframe(1f/16f, 0.1f,1,1,1,1),
                new Keyframe(1f/4f, 0.35f,2,2,1,1),
                new Keyframe(2f/3f, 0f,1,1,1,1),
                new Keyframe(5f/6f, -0.2f,1,1,1,1),
                new Keyframe(7f/8f, -0.1f,1,1,1,1),
                new Keyframe(1, 0,1,1,1,1)
            }),
            new AnimationCurve(new Keyframe[] { // Y-Axis, which is X-Axis due to rotation
                new Keyframe(0,0,1,1,1,1),
                new Keyframe(1f/16f, -0.15f,1,1,1,1),
                new Keyframe(1f/4f, 0.2f,1,1,1,1),
                new Keyframe(2f/3f, 0.4f,1,1,1,1),
                new Keyframe(5f/6f, 0.25f,1,1,1,1),
                new Keyframe(7f/8f, -0.05f,1,1,1,1),
                new Keyframe(1, 0,1,1,1,1)
            }),
            new AnimationCurve(new Keyframe[] { // Rotation
                new Keyframe(0,0,1,1,1,1),
                new Keyframe(1f/16f, 5f,1,1,1,1),
                new Keyframe(1f/4f, 40f,1,1,1,1),
                new Keyframe(2f/3f, 0f,1,1,1,1),
                new Keyframe(5f/6f, -20f,1,1,1,1),
                new Keyframe(7f/8f, -10f,1,1,1,1),
                new Keyframe(1, 0f,1,1,1,1)
            })
        };
        private List<Collider2D> HitColliders = new List<Collider2D>();
        private List<Player> HitPlayers = new List<Player>();

        //public bool IsAttacking(Collider2D collider)
        //{
        //    //if (!isSlashing)
        //    //    return false;
        //    return Collider.collider.IsTouching(collider, contactFilter) || collider.IsTouchingLayers(LayerMask.NameToLayer(Constants.PlayerObjectCollider));
        //}

        internal ObjectSlash Initialize(Player player, GameObject OnHitEffect = null)
        {
            this.Holdable = this.transform.root.GetComponent<Holdable>();
            this.Player = player;
            this.Input = this.Player.data.input;
            this.isSlashing = false;
            this.OnHitEffect = OnHitEffect;
            return this;
        }

        private void DoSlash()
        {
            if (!this.holdableObject.IsOut)
            {
                return;
            }
            this.sinceSlash = 0f;
            this.canDoDamage = true;
            this.DoSlashAnimation();
            if (this.Player.data.view.IsMine)
                NetworkingManager.RPC_Others(typeof(ObjectSlash), nameof(RPCA_DoSlashAnimation), this.Player.playerID);
        }

        [UnboundRPC]
        private static void RPCA_DoSlashAnimation(int playerID)
        {
            Player slasher = PlayerManager.instance.GetPlayerWithID(playerID);
            if (slasher is null)
            {
                return;
            }
            ObjectSlash objSlash = slasher.GetGun().transform.GetChild(1).Find(Constants.Lightsaber).GetComponent<ObjectSlash>();
            objSlash.DoSlashAnimation();
        }

        internal void DoSlashAnimation()
        {
            this.StartCoroutine(this.IDoSlashAnimation());
        }

        private IEnumerator IDoSlashAnimation()
        {
            this.isSlashing = true;
            this.HitPlayers = new List<Player>();
            if (this.Holdable == null)
                this.Holdable = base.transform.root.GetComponent<Holdable>();

            float deltaTime = 0f;
            float time = Mathf.Clamp(deltaTime / slashAnimationDuration, 0, 1);
            this.Mirror.rotMod = SlashCurves[2].Evaluate(time);

            const float left = -1f;
            const float right = 1f;
            while (deltaTime < slashAnimationDuration)
            {
                yield return null;
                deltaTime += TimeHandler.deltaTime;
                time = Mathf.Clamp(deltaTime / slashAnimationDuration, 0, 1);
                bool ScaleDirection = this.transform.root.position.x - 0.1f < this.Holdable.holder.transform.position.x;
                this.transform.root.position = Player.data.hand.position;
                Vector3 heading = new Vector3(SlashCurves[1].Evaluate(time), SlashCurves[0].Evaluate(time) * (ScaleDirection ? left : right), 0);
                Vector2 normalizedAimDirection = this.Player.data.aimDirection.normalized;
                Vector2 normalizedHeading = heading.normalized;
                this.transform.root.position += Quaternion.Euler(0, 0, Vector2.Angle(normalizedAimDirection, normalizedHeading)) * heading;
                this.Mirror.rotMod = SlashCurves[2].Evaluate(time);
                //UnityEngine.Debug.Log($"Time: {c}\nPosition: {slashCurves[0].Evaluate(c)}, {slashCurves[1].Evaluate(c)}\nRotation: {slashCurves[2].Evaluate(c)}");
            }
            this.Mirror.rotMod = 0;
            this.isSlashing = false;
            yield break;
        }

        private void Update()
        {
            this.sinceSlash += TimeHandler.deltaTime;
            if (!this.Player.data.view.IsMine)
                return;

            if (!this.isSlashing)
            {
                this.transform.root.position = Player.data.hand.position;
            }

            if (!this.holdableObject.IsOut)
                return;
            if (!this.Input.shootWasPressed)
                return;
            if (this.holdableObject.SwitchTimer > 0f)
                return;
            if (this.sinceSlash < slashCooldown)
                return;

            this.DoSlash();
        }

        internal void TrySlash(Collider2D collider2D)
        {
            if (!this.Player.data.view.IsMine || !this.canDoDamage || collider2D == null)
                return;

            Player player = collider2D.GetComponent<Player>();
            if (player == null)
                return;

            if (player.data.dead || player.playerID == this.Player.playerID)
                return;

            this.canDoDamage = false;
            NetworkingManager.RPC(typeof(ObjectSlash), nameof(RPCA_SlashPlayer),
                this.Player.playerID, collider2D.GetComponent<Player>().playerID);
        }

        internal void TrySlash(Collision2D collision)
        {
            Collider2D collider2D = collision.collider;
            if (!this.Player.data.view.IsMine)
                return;
            if (collider2D == null || collision.contactCount <= 0 || this.HitColliders.Contains(collider2D))
                return;

            HitColliders.Add(collider2D);
            CatsCards.instance.ExecuteAfterSeconds(slashAnimationDuration, () => { HitColliders.Remove(collider2D); });
            Vector3 position = this.gameObject.transform.position;
            if (collider2D.GetComponentInParent<Player>())
            {
                if (collider2D.GetComponentInParent<Player>().playerID == this.Player.playerID)
                    return;
                Player target = collider2D.GetComponentInParent<Player>();
                if (HitPlayers.Contains(target))
                    return;
                HitPlayers.Add(target);

                HandlePlayer(target, collision);
            }
            else if (collider2D.GetComponentInParent<ProjectileHit>())
                HandleBullet(collider2D.GetComponentInParent<ProjectileHit>(), collision);
            else if (collider2D.attachedRigidbody != null)
                HandleBox(collider2D.attachedRigidbody, collision);
            else if (this.isSlashing && collider2D.transform.root.GetComponentInChildren<Damagable>())
                collider2D.transform.root.GetComponentInChildren<Damagable>().CallTakeDamage(Vector2.up * 55f, position, damagingPlayer: Player);
        }

        internal void HandlePlayer(Player target, Collision2D collision)
        {
            if (!isSlashing)
                return;

            if (!CanAttack)
                return;

            NetworkingManager.RPC(typeof(ObjectSlash), nameof(RPCA_SlashPlayer), this.Player.playerID, target.playerID);
            Gun gun = Player.data.weaponHandler.gun;
            float mass = (float)target.data.playerVel.GetFieldValue(nameof(mass)) / 100f;
            float knockbackForce = Mathf.Pow(gun.damage, 2f) * gun.knockback * Mathf.Clamp(mass, 0f, 1f);
            target.data.healthHandler.CallTakeForce(knockbackForce * collision.contacts[0].point);
        }

        private const float slashForceMultiplier = 1500f;
        private const float nonSlashForceMultiplier = 500f;
        internal void HandleBox(Rigidbody2D box, Collision2D collision)
        {
            if (box.GetComponent<Damagable>() && this.isSlashing)
            {
                Gun gun = Player.data.weaponHandler.gun;
                Damagable obj = box.GetComponent<Damagable>();
                Vector2 point = collision.contacts[0].point;
                Vector2 normalizedPosition = this.transform.position.normalized;
                obj.CallTakeDamage(55f * gun.damage * gun.bulletDamageMultiplier * (point - normalizedPosition), point);
            }

            ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
            int count = collision.GetContacts(contacts);

            for (int i = 0; i < count; i++)
                box.AddForceAtPosition(this.Player.data.weaponHandler.gun.knockback * box.mass * -1 * contacts[i].normal * (isSlashing ?
                    slashForceMultiplier : nonSlashForceMultiplier) / (float)count, contacts[i].point);
            //UnityEngine.Debug.Log($"Adding {this.player.data.weaponHandler.gun.knockback * box.mass * -1 * contacts[i].normal * (slashing ? slashForceMult : nonslashForceMult) / (float)count} force to {box.gameObject}");
        }

        internal void HandleBullet(ProjectileHit bullet, Collision2D collision)
        {
            Player.data.block.blocked(bullet.gameObject, bullet.gameObject.transform.forward, collision.transform.position);
        }

        [UnboundRPC]
        private static void RPCA_SlashPlayer(int slashingPlayerID, int slashedPlayerID)
        {
            if (slashedPlayerID == -1)
                return;

            Player slashingPlayer = PlayerManager.instance.GetPlayerWithID(slashingPlayerID);
            Player slashedPlayer = PlayerManager.instance.GetPlayerWithID(slashedPlayerID);
            if (slashedPlayer == null || slashingPlayer == null)
                return;

            // 51 damage instead of 50 because players don't die until they are less than 0 hp
            Gun gun = slashingPlayer.GetGun();
            //slashedPlayer.data.stats.objectsAddedToPlayer.Add(OnHitEffect);
            //OnHitEffect.GetComponent<ReversibleOnSlashHitEffect>().DestroyEffect();

            float damage = gun.damage;
            float damageMultiplier = gun.bulletDamageMultiplier;
            slashedPlayer.data.healthHandler.DoDamage(damage * damageMultiplier * 55f * gun.shootPosition.forward,
                slashedPlayer.transform.position, Color.white,
                gun.transform?.GetChild(1)?.Find(Constants.Lightsaber)?.gameObject,
                slashingPlayer, false, true, true);
        }

        public static IEnumerator Enable(IGameModeHandler gm)
        {
            CanAttack = true;
            yield break;
        }

        public static IEnumerator Disable(IGameModeHandler gm)
        {
            CanAttack = false;
            yield break;
        }
    }
}


