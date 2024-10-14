using CatsCards.Lightsaber.Extensions;
using UnboundLib;
using UnboundLib.Extensions;
using UnboundLib.Networking;
using UnboundLib.Utils;
using UnityEngine;

namespace CatsCards.Lightsaber
{
    internal class BladeColor : MonoBehaviour
    {
        internal A_HoldableObject holdableObject;
        private static SpriteRenderer render;
        private static PlayerSkin skin;
        private static Player player;
        private static Color color;

        ///TODO add config Cursed color which enables the player based color.
        internal BladeColor Initialize(int playerID)
        {
            player = PlayerManager.instance.GetPlayerWithID(playerID);
            skin = ExtraPlayerSkins.GetPlayerSkinColors(player.colorID());
            color = skin.color;
            render = this.gameObject.GetComponent<SpriteRenderer>();
            Unbound.Instance.ExecuteAfterFrames(10, UpdateBladeColor);
            return this;
        }

        public static void UpdateBladeColor()

        {
            RPCA_UpdateBladeColor(color);
        }

        [UnboundRPC]
        private static void RPCA_UpdateBladeColor(Color color)
        {
            render.color = color;
        }
    }
}
