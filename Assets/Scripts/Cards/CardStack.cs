using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour
{
    public static CardStack instance;

    public List<ICard> cards;

    private void Awake()
    {
        if (CardStack.instance == null)
        {
            CardStack.instance = this;
        }
        else
        {
            Destroy(this);
        }
        cards = new List<ICard>();
    }

    public bool executing = false;

    public void executeCards()
    {
        if(!executing) { executing = true; StartCoroutine(wait()); }
    }

    public IEnumerator wait()
    {
        while (cards.Count > 0)
        {
            // if (cards.Count == 2 && cards[0].GetCardType() == CardEnum.Dash && cards[1].GetCardType() == CardEnum.Dash)
            // {
            //     ICard card1 = cards[0];
            //     ICard card2 = cards[1];
            //     cards.RemoveAt(0);
            //     cards.RemoveAt(0);
            //     PlayerController.instance.SuperDash();
            //     card1.RemoveCard();
            //     card2.RemoveCard();
            // }
            // else
            // {
                ICard card = cards[0];
                cards.RemoveAt(0);
                card.ActiveCard();
                yield return new WaitForSeconds(1f);
             //}
        }
        executing = false;
    }

}
