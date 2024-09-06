using RarityLib.Utils;
using UnityEngine;

namespace CatsCards
{
    internal class RarityAdder : MonoBehaviour
    {
        public Rarity rarity = Rarity.Common;
        private void Start()
        {
            base.GetComponent<CardInfo>().rarity = RarityUtils.GetRarity($"{rarity}");
        }
    }
    public enum Rarity
    {
        Trinket,
        Common,
        Scarce,
        Uncommon,
        Exotic,
        Rare,
        Epic,
        Legendary,
        Mythical,
        Divine
    }
}
