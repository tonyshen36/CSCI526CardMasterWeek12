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
            ICard card = cards[0];
            cards.RemoveAt(0);
            card.ActiveCard();
            yield return new WaitForSeconds(1f);
        }
        executing = false;
    }

}
