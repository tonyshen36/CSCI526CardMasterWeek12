using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    public List<GameObject> deck;
    public List<GameObject> cardsInDeck;
    public List<GameObject> handCards;

    public GameObject JumpCardPrefab;
    public GameObject MoveCardPrefab;
    public GameObject MoveBackCardPrefab;

    public int startingMoveRightCard = 30;

    public Transform cards;
    public Transform deckLocation;

    public int currentCardCount = 0;

    public List<GameObject> cardLocations;

    public int loseScreenIndex;

    public int remainingMoveCards = 0;
    public int remainingJumpCards = 0;
    public int remainingMoveBackCards = 0;

    public int moveCardsInHand = 0;
    public int jumpCardsInHand = 0;
    public int moveBackCardsInHand = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if(CardManager.instance == null)
        {
            CardManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
        InitalizedDeck();
    }

    void InitalizedDeck() 
    {
        deck = new List<GameObject>();
        for(int i = 0; i < startingMoveRightCard; i++)
        {
            GameObject newCard = Instantiate(MoveCardPrefab, deckLocation);
            deck.Add(newCard);
            remainingMoveCards++;
            newCard.SetActive(false);
        }
        cardsInDeck = new List<GameObject>(deck);
    }

    public void AddMove()
    {
        GameObject newCard = Instantiate(MoveCardPrefab, deckLocation);
        deck.Add(newCard);
        cardsInDeck.Add(newCard);
        remainingMoveCards++;
        newCard.SetActive(false);
    }

    public void AddMoveBack()
    {
        GameObject newCard = Instantiate(MoveBackCardPrefab, deckLocation);
        deck.Add(newCard);
        cardsInDeck.Add(newCard);
        remainingMoveBackCards++;
        newCard.SetActive(false);
    }

    public void AddJump()
    {
        GameObject newCard = Instantiate(JumpCardPrefab, deckLocation);
        deck.Add(newCard);
        cardsInDeck.Add(newCard);
        remainingJumpCards++;
        newCard.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(UpdateHand());
    }

    void Update()
    {
        /*if (currentCardCount < 5 && cardsInDeck.Count >= 1)
        {
            int cardChoice = Random.Range(0, cardsInDeck.Count);
            cardsInDeck[cardChoice].SetActive(true);
            cardsInDeck[cardChoice].transform.SetParent(cards);
            handCards.Add(cardsInDeck[cardChoice]);
            cardsInDeck.RemoveAt(cardChoice);
            currentCardCount++;
            RearrangeHand();
        }
        else if (currentCardCount == 0 && cardsInDeck.Count == 0)
        {
            SceneManager.LoadScene("LoseScreen 1");
        }*/
    }

    IEnumerator UpdateHand()
    {
        while(currentCardCount != 0 || cardsInDeck.Count != 0)
        {
            if (currentCardCount < 5 && cardsInDeck.Count >= 1)
            {
                int cardChoice = -1;
                if (moveCardsInHand == 4 && (remainingJumpCards > 0 || remainingMoveBackCards > 0))
                {
                    if (remainingJumpCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Jump;
                        else targetEnum = CardEnum.MoveBack;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() != CardEnum.Move)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    
                }
                else if (moveBackCardsInHand == 4 && (remainingJumpCards > 0 || remainingMoveCards > 0))
                {
                    if (remainingJumpCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Jump;
                        else targetEnum = CardEnum.Move;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() != CardEnum.MoveBack)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }           
                }
                else if (jumpCardsInHand == 4 && (remainingMoveBackCards > 0 || remainingMoveCards > 0))
                {
                    if (remainingJumpCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.Move;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() != CardEnum.Jump)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    cardChoice = Random.Range(0, cardsInDeck.Count);
                }
                 
                CardEnum cardType = cardsInDeck[cardChoice].GetComponent<ICard>().GetCardType();
                switch (cardType)
                {
                    case CardEnum.Move:
                        remainingMoveCards--;
                        moveCardsInHand++;
                        break;
                    case CardEnum.MoveBack:
                        remainingMoveBackCards--;
                        moveBackCardsInHand++;
                        break;
                    case CardEnum.Jump:
                        remainingJumpCards--;
                        jumpCardsInHand++;
                        break;
                }
                cardsInDeck[cardChoice].SetActive(true);
                cardsInDeck[cardChoice].transform.SetParent(cards);
                
                handCards.Add(cardsInDeck[cardChoice]);
                cardsInDeck.RemoveAt(cardChoice);
                currentCardCount++;
                RearrangeHand();
            }
            yield return new WaitForSeconds(0.2f);
        }
        SceneManager.LoadScene(loseScreenIndex);
    }

    void RearrangeHand()
    {
        switch (handCards.Count)
        {
            case 0:
                break; 
            case 1:
                handCards[0].transform.DOLocalMove(cardLocations[2].transform.localPosition, 1).OnComplete(() => handCards[0].GetComponent<ICard>().EnableDragging());

                //handCards[0].GetComponent<ICard>().DisableDragging();
                break;
            case 2:
                handCards[0].transform.DOLocalMove(cardLocations[1].transform.localPosition, 1).OnComplete(() => handCards[0].GetComponent<ICard>().EnableDragging());
                handCards[1].transform.DOLocalMove(cardLocations[3].transform.localPosition, 1).OnComplete(() => handCards[1].GetComponent<ICard>().EnableDragging());

                //handCards[0].GetComponent<ICard>().DisableDragging();
                //handCards[1].GetComponent<ICard>().DisableDragging();
                break; 
            case 3:
                handCards[0].transform.DOLocalMove(cardLocations[1].transform.localPosition, 1).OnComplete(() => handCards[0].GetComponent<ICard>().EnableDragging());
                handCards[1].transform.DOLocalMove(cardLocations[2].transform.localPosition, 1).OnComplete(() => handCards[1].GetComponent<ICard>().EnableDragging());
                handCards[2].transform.DOLocalMove(cardLocations[3].transform.localPosition, 1).OnComplete(() => handCards[2].GetComponent<ICard>().EnableDragging());

                //handCards[0].GetComponent<ICard>().DisableDragging();
                //handCards[1].GetComponent<ICard>().DisableDragging();
                //handCards[2].GetComponent<ICard>().DisableDragging();
                break;
            case 4:
                handCards[0].transform.DOLocalMove(cardLocations[0].transform.localPosition, 1).OnComplete(() => handCards[0].GetComponent<ICard>().EnableDragging());
                handCards[1].transform.DOLocalMove(cardLocations[1].transform.localPosition, 1).OnComplete(() => handCards[1].GetComponent<ICard>().EnableDragging());
                handCards[2].transform.DOLocalMove(cardLocations[3].transform.localPosition, 1).OnComplete(() => handCards[2].GetComponent<ICard>().EnableDragging());
                handCards[3].transform.DOLocalMove(cardLocations[4].transform.localPosition, 1).OnComplete(() => handCards[3].GetComponent<ICard>().EnableDragging());

                //handCards[0].GetComponent<ICard>().DisableDragging();
                //handCards[1].GetComponent<ICard>().DisableDragging();
                //handCards[2].GetComponent<ICard>().DisableDragging();
                //handCards[3].GetComponent<ICard>().DisableDragging();
                break;
            case 5:
                handCards[0].transform.DOLocalMove(cardLocations[0].transform.localPosition, 1).OnComplete(() => handCards[0].GetComponent<ICard>().EnableDragging());
                handCards[1].transform.DOLocalMove(cardLocations[1].transform.localPosition, 1).OnComplete(() => handCards[1].GetComponent<ICard>().EnableDragging());
                handCards[2].transform.DOLocalMove(cardLocations[2].transform.localPosition, 1).OnComplete(() => handCards[2].GetComponent<ICard>().EnableDragging());
                handCards[3].transform.DOLocalMove(cardLocations[3].transform.localPosition, 1).OnComplete(() => handCards[3].GetComponent<ICard>().EnableDragging());
                handCards[4].transform.DOLocalMove(cardLocations[4].transform.localPosition, 1).OnComplete(() => handCards[4].GetComponent<ICard>().EnableDragging());

                //handCards[0].GetComponent<ICard>().DisableDragging();
                //handCards[1].GetComponent<ICard>().DisableDragging();
                //handCards[2].GetComponent<ICard>().DisableDragging();
                //handCards[3].GetComponent<ICard>().DisableDragging();
                //handCards[4].GetComponent<ICard>().DisableDragging();
                break;
            default: 
                break;
        }
    }

    public void ShuffleCard()
    {
        foreach(GameObject card in handCards)
        {
            card.SetActive(false);
            card.transform.SetParent(deckLocation);
            card.transform.localPosition = new Vector3(0, 0);
            cardsInDeck.Add(card);
            currentCardCount--;
            switch (card.GetComponent<ICard>().GetCardType())
            {
                case CardEnum.Move:
                    remainingMoveCards++;
                    moveCardsInHand--;
                    break;
                case CardEnum.MoveBack:
                    remainingMoveBackCards++;
                    moveBackCardsInHand--;
                    break;
                case CardEnum.Jump:
                    remainingJumpCards++;
                    jumpCardsInHand--;
                    break;
            }
        }
        handCards.Clear();
    }
}
