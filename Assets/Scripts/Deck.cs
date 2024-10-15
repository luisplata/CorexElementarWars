using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour, IDeck
{
    [SerializeField] private List<CardPoint> cards;
    [SerializeField] private GameObject deckPoint;
    [SerializeField] private Card cardPrefab;
    private IMediatorDeck _mediatorDeck;


    public void Configure(IMediatorDeck mediatorDeck)
    {
        _mediatorDeck = mediatorDeck;
        FillCards();
    }

    public void FillCards()
    {
        var count = cards.Count(cardPoint => !cardPoint.IsFull());

        for (int i = 0; i < count; i++)
        {
            foreach (var cardPoint in cards)
            {
                if (!cardPoint.IsFull())
                {
                    var card = Instantiate(cardPrefab, deckPoint.transform.position, Quaternion.identity);
                    card.Configure(_mediatorDeck.GetInputHandler(), this, cardPoint);
                    cardPoint.SetCard();
                    
                    break;
                }
            }
        }
    }
}

public interface IDeck
{
}