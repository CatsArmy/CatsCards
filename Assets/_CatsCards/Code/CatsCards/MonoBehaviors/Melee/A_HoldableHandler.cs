using Lightsaber.Extensions;
using UnboundLib;
using UnityEngine;

namespace Lightsaber
{

    public class A_HoldableHandler : MonoBehaviour
    {
        internal Player Player;
        internal ObjectSlash ObjectSlash;
        internal ObjectMirror ObjectMirror;
        internal ObjectCollider ObjectCollider;
        internal BladeColor BladeColor;
        internal GameObject Weapon;
        internal GameObject Blade;
        internal GameObject Collider;

        /// <param name="player">the player who got the weapon</param>
        /// <param name="WeaponName">a backup string of the unity object of the weapon to Init the weapon incase 
        /// something goes wrong</param>        
        public A_HoldableHandler Initialize(A_HoldableObject HoldableObject, Player player, string WeaponName = "BackupInit")
        {
            //if weapon failed to load reload it
            if (HoldableObject.Weapon is null || HoldableObject.Weapon == null)
            {
                HoldableObject.Weapon = Instantiate(CatsCards.CatsCards.assets.LoadAsset<GameObject>(WeaponName),
                    player.GetSpring().transform);
                //Remove the (Clone) part of the string as otherwise you would fail to find weapon
                HoldableObject.Weapon.gameObject.name = WeaponName;
            }
            this.Player = player;
            //Object Tree Structure as objects for quick access
            this.Weapon = HoldableObject.Weapon;
            this.Blade = this.Weapon.transform.Find(nameof(this.Blade)).gameObject;
            this.Collider = this.Blade.transform.Find(nameof(this.Collider)).gameObject;
            //Do not change order unless you know what your doing...
            this.ObjectMirror = this.Weapon.GetOrAddComponent<ObjectMirror>();
            this.ObjectSlash = this.Weapon.GetOrAddComponent<ObjectSlash>().Initialize(this.Player);
            this.ObjectCollider = this.Collider.gameObject.GetOrAddComponent<ObjectCollider>()
                .IgnoreCollisions(this.ObjectSlash);
            this.ObjectSlash.Collider = this.ObjectCollider;
            this.ObjectSlash.Mirror = this.ObjectMirror;
            this.BladeColor = this.Blade.gameObject.GetOrAddComponent<BladeColor>().Initialize(player.playerID);

            this.ObjectSlash.holdableObject = HoldableObject;
            this.ObjectMirror.holdableObject = HoldableObject;
            this.ObjectCollider.holdableObject = HoldableObject;
            this.BladeColor.holdableObject = HoldableObject;
            return this;
        }
    }
}
