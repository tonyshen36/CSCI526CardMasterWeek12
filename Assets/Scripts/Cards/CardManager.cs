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
    public GameObject DashCardPrefab;
    public GameObject DashBackCardPrefab;
    public GameObject SlashCardPrefab;

    public int startingMoveRightCard = 0;
    public int startingDashCard = 10;
    public int startingDashBackCard = 10;
    public int startingJumpCard = 10;
    public int startingMoveBackCard = 10;
    public int startingSlashCard = 20;

    public Transform cards;
    public Transform deckLocation;

    public int currentCardCount = 0;

    public List<GameObject> cardLocations;

    public int loseScreenIndex;

    public int remainingMoveCards = 0;
    public int remainingJumpCards = 0;
    public int remainingMoveBackCards = 0;
    public int remainingDashCards = 0;
    public int remainingDashBackCards = 0;
    public int remainingSlashCards = 0;
    public int moveCardsInHand = 0;
    public int jumpCardsInHand = 0;
    public int moveBackCardsInHand = 0;
    public int dashCardsInHand = 0;
    public int dashBackCardsInHand = 0;
    public int slashCardsInHand = 10;

    public int jumpRewardsCounter;
    public int moveRewardsCounter;
    public int backRewardsCounter;

    public int RedrawTimes = 0;
    public int RedrawLimit = 5;

    // Start is called before the first frame update
    void Awake()
    {
        if (CardManager.instance == null)
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
        for (int i = 0; i < startingSlashCard; i++)
        {
            GameObject newCard = Instantiate(SlashCardPrefab, deckLocation);
            deck.Add(newCard);
            remainingSlashCards++;
            newCard.SetActive(false);
        }
        for (int i = 0; i < startingDashBackCard; i++)
        {
            GameObject newCard = Instantiate(DashBackCardPrefab, deckLocation);
            deck.Add(newCard);
            remainingDashBackCards++;
            newCard.SetActive(false);
        }
        for (int i = 0; i < startingDashCard; i++)
        {
            GameObject newCard = Instantiate(DashCardPrefab, deckLocation);
            deck.Add(newCard);
            remainingDashCards++;
            newCard.SetActive(false);
        }
        for (int i = 0; i < startingMoveRightCard; i++)
        {
            GameObject newCard = Instantiate(MoveCardPrefab, deckLocation);
            deck.Add(newCard);
            remainingMoveCards++;
            newCard.SetActive(false);
        }
        for (int i = 0; i < startingJumpCard; i++)
        {
            GameObject newCard = Instantiate(JumpCardPrefab, deckLocation);
            deck.Add(newCard);
            remainingJumpCards++;
            newCard.SetActive(false);
        }
        for (int i = 0; i < startingMoveBackCard; i++)
        {
            GameObject newCard = Instantiate(MoveBackCardPrefab, deckLocation);
            deck.Add(newCard);
            remainingMoveBackCards++;
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

    public void AddDash()
    {
        GameObject newCard = Instantiate(DashCardPrefab, deckLocation);
        deck.Add(newCard);
        cardsInDeck.Add(newCard);
        remainingDashCards++;
        newCard.SetActive(false);
    }

    public void AddDashBack()
    {
        GameObject newCard = Instantiate(DashBackCardPrefab, deckLocation);
        deck.Add(newCard);
        cardsInDeck.Add(newCard);
        remainingDashBackCards++;
        newCard.SetActive(false);
    }
    public void AddSlash()
    {
        GameObject newCard = Instantiate(SlashCardPrefab, deckLocation);
        deck.Add(newCard);
        cardsInDeck.Add(newCard);
        remainingSlashCards++;
        newCard.SetActive(false);
    }

    public void setBackCounter()
    {
        backRewardsCounter++;
    }
    public void setJumpCounter()
    {
        jumpRewardsCounter++;
    }
    public void setMoveCounter()
    {
        moveRewardsCounter++;
    }

    private void Start()
    {
        jumpRewardsCounter = 0;
        backRewardsCounter = 0;
        moveRewardsCounter = 0;
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
        while (currentCardCount != 0 || cardsInDeck.Count != 0)
        {
            if (currentCardCount < 5 && cardsInDeck.Count >= 1)
            {
                int cardChoice = -1;
                if (moveCardsInHand == 4 && (remainingJumpCards > 0 || remainingMoveBackCards > 0 || remainingDashCards > 0 || remainingDashBackCards > 0))
                {
                    if (remainingJumpCards > 0 && remainingDashBackCards > 0 && remainingDashCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 4);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else if (randomTarget == 1) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 2) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.DashBack;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingJumpCards > 0 && remainingDashBackCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.DashBack;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashCards > 0 && remainingDashBackCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.DashBack;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashCards > 0 && remainingJumpCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashCards > 0 && remainingJumpCards > 0 && remainingDashBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.DashBack;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingJumpCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 2);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashBackCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.DashBack;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveBackCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingJumpCards > 0 && remainingMoveBackCards > 0)
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
                    else if (remainingDashBackCards > 0 && remainingJumpCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.DashBack;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashBackCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.DashBack;
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
                else if (moveBackCardsInHand == 4 && (remainingJumpCards > 0 || remainingMoveCards > 0 || remainingDashCards > 0 || remainingDashBackCards > 0))
                {
                    if (remainingJumpCards > 0 && remainingMoveCards > 0 && remainingDashCards > 0 && remainingDashBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 4);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else if (randomTarget == 1) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 2) targetEnum = CardEnum.DashBack;
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
                    else if (remainingJumpCards > 0 && remainingMoveCards > 0 && remainingDashBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else if (randomTarget == 1) targetEnum = CardEnum.DashBack;
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
                    else if (remainingDashCards > 0 && remainingMoveCards > 0 && remainingDashBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.DashBack;
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
                    else if (remainingDashCards > 0 && remainingJumpCards > 0 && remainingDashBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.DashBack;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashCards > 0 && remainingJumpCards > 0 && remainingMoveCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingJumpCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 2);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashBackCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.DashBack;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingJumpCards > 0 && remainingDashBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Jump;
                        else targetEnum = CardEnum.DashBack;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveCards > 0 && remainingJumpCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveCards > 0 && remainingDashBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.DashBack;
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
                else if (jumpCardsInHand == 4 && (remainingMoveBackCards > 0 || remainingMoveCards > 0 || remainingDashCards > 0 || remainingDashBackCards > 0))
                {
                    if (remainingDashBackCards > 0 && remainingMoveCards > 0 && remainingDashCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 4);
                        if (randomTarget == 0) targetEnum = CardEnum.DashBack;
                        else if (randomTarget == 1) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 2) targetEnum = CardEnum.MoveBack;
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
                    else if (remainingDashBackCards > 0 && remainingMoveCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.DashBack;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
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
                    else if (remainingDashCards > 0 && remainingMoveCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
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
                    else if (remainingDashCards > 0 && remainingDashBackCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.DashBack;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashCards > 0 && remainingDashBackCards > 0 && remainingMoveCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.DashBack;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashBackCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 2);
                        if (randomTarget == 0) targetEnum = CardEnum.DashBack;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveBackCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashBackCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.DashBack;
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
                    else if (remainingMoveCards > 0 && remainingDashBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.DashBack;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
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
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() != CardEnum.Jump)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }

                }
                else if (dashCardsInHand == 4 && (remainingMoveBackCards > 0 || remainingMoveCards > 0 || remainingJumpCards > 0 || remainingDashBackCards > 0))
                {
                    if (remainingJumpCards > 0 && remainingMoveCards > 0 && remainingDashBackCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 4);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else if (randomTarget == 1) targetEnum = CardEnum.DashBack;
                        else if (randomTarget == 2) targetEnum = CardEnum.MoveBack;
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
                    else if (remainingJumpCards > 0 && remainingMoveCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
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
                    else if (remainingDashBackCards > 0 && remainingMoveCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.DashBack;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
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
                    else if (remainingDashBackCards > 0 && remainingJumpCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.DashBack;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashBackCards > 0 && remainingJumpCards > 0 && remainingMoveCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.DashBack;
                        else if (randomTarget == 1) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingJumpCards > 0 && remainingDashBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 2);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else targetEnum = CardEnum.DashBack;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveCards > 0 && remainingDashBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.DashBack;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveBackCards > 0 && remainingDashBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.DashBack;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingJumpCards > 0 && remainingMoveBackCards > 0)
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
                    else if (remainingMoveCards > 0 && remainingJumpCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
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
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() != CardEnum.Dash)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }

                }
                else if (dashBackCardsInHand == 4 && (remainingDashCards > 0 || remainingJumpCards > 0 || remainingMoveCards > 0 || remainingMoveBackCards > 0))
                {
                    if (remainingJumpCards > 0 && remainingMoveCards > 0 && remainingDashCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 4);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else if (randomTarget == 1) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 2) targetEnum = CardEnum.MoveBack;
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
                    else if (remainingJumpCards > 0 && remainingMoveCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
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
                    else if (remainingDashCards > 0 && remainingMoveCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
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
                    else if (remainingDashCards > 0 && remainingJumpCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingDashCards > 0 && remainingJumpCards > 0 && remainingMoveCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 3);
                        if (randomTarget == 0) targetEnum = CardEnum.Dash;
                        else if (randomTarget == 1) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingJumpCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        int randomTarget = Random.Range(0, 2);
                        if (randomTarget == 0) targetEnum = CardEnum.Jump;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveBackCards > 0 && remainingDashCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.MoveBack;
                        else targetEnum = CardEnum.Dash;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingJumpCards > 0 && remainingMoveBackCards > 0)
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
                    else if (remainingMoveCards > 0 && remainingJumpCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
                        else targetEnum = CardEnum.Jump;
                        for (int i = 0; i < cardsInDeck.Count; i++)
                        {
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() == targetEnum)
                            {
                                cardChoice = i;
                                break;
                            }
                        }
                    }
                    else if (remainingMoveCards > 0 && remainingMoveBackCards > 0)
                    {
                        CardEnum targetEnum;
                        if (Random.Range(0, 2) == 0) targetEnum = CardEnum.Move;
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
                            if (cardsInDeck[i].GetComponent<ICard>().GetCardType() != CardEnum.DashBack)
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
                    case CardEnum.Dash:
                        remainingDashCards--;
                        dashCardsInHand++;
                        break;
                    case CardEnum.DashBack:
                        remainingDashBackCards--;
                        dashBackCardsInHand++;
                        break;
                    case CardEnum.Slash:
                        remainingSlashCards--;
                        slashCardsInHand++;
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
        Analyzer.instance.reach_end_point(false);
        PlayerController.instance.sendCardStatToAnalyzer(false);
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
        if (RedrawTimes < RedrawLimit)
        {
            foreach (GameObject card in handCards)
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
                    case CardEnum.Dash:
                        remainingDashCards++;
                        dashCardsInHand--;
                        break;
                    case CardEnum.DashBack:
                        remainingDashBackCards++;
                        dashBackCardsInHand--;
                        break;
                }
            }
            handCards.Clear();
            RedrawTimes += 1;
        }
    }

    public int getJumpRewardsCounter()
    {
        return jumpRewardsCounter;
    }
    public int getBackRewardsCounter()
    {
        return backRewardsCounter;
    }
    public int getMoveRewardsCounter()
    {
        return moveRewardsCounter;
    }
}