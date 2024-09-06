using UnityEngine;

namespace Lightsaber
{
    public class ObjectCollider : MonoBehaviour
    {
        internal A_HoldableObject holdableObject;

        internal ObjectSlash ObjectSlash;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        internal Collider2D collider;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        internal ObjectCollider IgnoreCollisions(ObjectSlash objectSlash)
        {
            // object must be on this layer to avoid prop-flying and shadow casting
            this.gameObject.layer = LayerMask.NameToLayer(Constants.PlayerObjectCollider);
            this.collider = base.GetComponent<Collider2D>();
            this.collider.attachedRigidbody.mass = 500;
            this.collider.isTrigger = false;
            this.ObjectSlash = objectSlash;

            foreach (Collider2D collider in this.ObjectSlash.Player.GetComponentsInChildren<Collider2D>())
            {
                Physics2D.IgnoreCollision(this.collider, collider);
            }
            IgnoreLayerCollision();

            return this;
        }

        /// <summary>
        /// <list type="bullet">
        /// <item><see langword="pros"/>: + Removes players jumping on the object to fly.</item>
        /// <item><see langword="cons"/>: - Removes the option to collide with cards.</item>
        /// </list>
        /// </summary>
        public static void IgnoreLayerCollision()
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(Constants.PlayerObjectCollider), LayerMask.NameToLayer(nameof(Player)));
        }

#pragma warning disable IDE0051 // Remove unused private members

        private void OnCollider(Collider2D collider2D)
        {
            this.ObjectSlash?.TrySlash(collider2D);
        }

        private void OnCollision(Collision2D collision2D)
        {
            this.ObjectSlash?.TrySlash(collision2D);
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            OnCollider(collider2D);
        }

        private void OnTriggerStay2D(Collider2D collider2D)
        {
            OnCollider(collider2D);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollision(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            OnCollision(collision);
        }

#pragma warning restore IDE0051 // Remove unused private members
    }
}
