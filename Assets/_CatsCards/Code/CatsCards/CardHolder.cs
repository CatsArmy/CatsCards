using System.Collections.Generic;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CatsCards
{
    public class CardHolder : MonoBehaviour
    {
        public List<GameObject> Cards;
        public List<GameObject> HiddenCards;
        internal void RegisterCards()
        {
            foreach (var Card in Cards)
            {
                if (Card.GetComponent<RarityAdder>() is RarityAdder rarityAdder)
                    this.ExecuteAfterFrames(5, () => Card.GetComponent<CardInfo>().rarity = RarityUtils.GetRarity($"{rarityAdder.rarity}"));

                CustomCard.RegisterUnityCard(Card,
                    CatsCards.modInitials, Card.GetComponent<CardInfo>().cardName, true, null);
            }
            foreach (var Card in HiddenCards)
            {
                if (Card.GetComponent<RarityAdder>() is RarityAdder rarityAdder)
                    this.ExecuteAfterFrames(5, () => Card.GetComponent<CardInfo>().rarity = RarityUtils.GetRarity($"{rarityAdder.rarity}"));

                CustomCard.RegisterUnityCard(Card, CatsCards.modInitials, Card.GetComponent<CardInfo>().cardName, false, null);
                ModdingUtils.Utils.Cards.instance.AddHiddenCard(Card.GetComponent<CardInfo>());
            }
        }
    }
}