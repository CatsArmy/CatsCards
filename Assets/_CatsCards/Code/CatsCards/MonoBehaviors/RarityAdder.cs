using UnityEngine;

namespace CatsCards
{
    internal class RarityAdder : MonoBehaviour
    {
        public Rarity rarity = Rarity.Common;
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
