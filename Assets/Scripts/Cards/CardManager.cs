using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    public List<GameObject> deck;
    public List<GameObject> cardsInDeck;

    public GameObject JumpCardPrefab;
    public GameObject MoveCardPrefab;
    public GameObject MoveBackCardPrefab;

    public int startingMoveRightCard = 10;

    public Transform cards;
    public Transform deckLocation;

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
            newCard.SetActive(false);
        }
        cardsInDeck = new List<GameObject>(deck);
    }

    public void AddMove()
    {
        GameObject newCard = Instantiate(MoveCardPrefab, deckLocation);
        deck.Add(newCard);
        cardsInDeck.Add(newCard);
        newCard.SetActive(false);
    }

    public void AddMoveBack()
    {
        GameObject newCard = Instantiate(MoveBackCardPrefab, deckLocation);
        deck.Add(newCard);
        cardsInDeck.Add(newCard);
        newCard.SetActive(false);
    }

    public void AddJump()
    {
        GameObject newCard = Instantiate(JumpCardPrefab, deckLocation);
        deck.Add(newCard);
        cardsInDeck.Add(newCard);
        newCard.SetActive(false);
    }

    void Update()
    {
        if (cards.childCount == 0 && cardsInDeck.Count >= 3)
        {
            int cardChoice = Random.Range(0, cardsInDeck.Count);
            cardsInDeck[cardChoice].SetActive(true);
            cardsInDeck[cardChoice].transform.SetParent(transform, cards);
            cardsInDeck.RemoveAt(cardChoice);
            cardChoice = Random.Range(0, cardsInDeck.Count);
            cardsInDeck[cardChoice].SetActive(true);
            cardsInDeck[cardChoice].transform.SetParent(transform, cards);
            cardsInDeck.RemoveAt(cardChoice);
            cardChoice = Random.Range(0, cardsInDeck.Count);
            cardsInDeck[cardChoice].SetActive(true);
            cardsInDeck[cardChoice].transform.SetParent(transform, cards);
            cardsInDeck.RemoveAt(cardChoice);
        }
        else if(cards.childCount == 0 && cardsInDeck.Count > 0)
        {
            while(cardsInDeck.Count > 0)
            {
                cardsInDeck[0].SetActive(true);
                cardsInDeck[0].transform.SetParent(transform, cards);
                cardsInDeck.RemoveAt(0);
            }
        }
        else if (cards.childCount == 0 && cardsInDeck.Count == 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
