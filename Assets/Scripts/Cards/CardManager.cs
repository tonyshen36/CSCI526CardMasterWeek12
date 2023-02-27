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

    public int startingMoveRightCard = 10;

    public Transform cards;
    public Transform deckLocation;

    public int currentCardCount = 0;

    public List<GameObject> cardLocations;

    public int loseScreenIndex;

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
                int cardChoice = Random.Range(0, cardsInDeck.Count);
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

                handCards[0].GetComponent<ICard>().DisableDragging();
                break;
            case 2:
                handCards[0].transform.DOLocalMove(cardLocations[1].transform.localPosition, 1).OnComplete(() => handCards[0].GetComponent<ICard>().EnableDragging());
                handCards[1].transform.DOLocalMove(cardLocations[3].transform.localPosition, 1).OnComplete(() => handCards[1].GetComponent<ICard>().EnableDragging());

                handCards[0].GetComponent<ICard>().DisableDragging();
                handCards[1].GetComponent<ICard>().DisableDragging();
                break; 
            case 3:
                handCards[0].transform.DOLocalMove(cardLocations[1].transform.localPosition, 1).OnComplete(() => handCards[0].GetComponent<ICard>().EnableDragging());
                handCards[1].transform.DOLocalMove(cardLocations[2].transform.localPosition, 1).OnComplete(() => handCards[1].GetComponent<ICard>().EnableDragging());
                handCards[2].transform.DOLocalMove(cardLocations[3].transform.localPosition, 1).OnComplete(() => handCards[2].GetComponent<ICard>().EnableDragging());

                handCards[0].GetComponent<ICard>().DisableDragging();
                handCards[1].GetComponent<ICard>().DisableDragging();
                handCards[2].GetComponent<ICard>().DisableDragging();
                break;
            case 4:
                handCards[0].transform.DOLocalMove(cardLocations[0].transform.localPosition, 1).OnComplete(() => handCards[0].GetComponent<ICard>().EnableDragging());
                handCards[1].transform.DOLocalMove(cardLocations[1].transform.localPosition, 1).OnComplete(() => handCards[1].GetComponent<ICard>().EnableDragging());
                handCards[2].transform.DOLocalMove(cardLocations[3].transform.localPosition, 1).OnComplete(() => handCards[2].GetComponent<ICard>().EnableDragging());
                handCards[3].transform.DOLocalMove(cardLocations[4].transform.localPosition, 1).OnComplete(() => handCards[3].GetComponent<ICard>().EnableDragging());

                handCards[0].GetComponent<ICard>().DisableDragging();
                handCards[1].GetComponent<ICard>().DisableDragging();
                handCards[2].GetComponent<ICard>().DisableDragging();
                handCards[3].GetComponent<ICard>().DisableDragging();
                break;
            case 5:
                handCards[0].transform.DOLocalMove(cardLocations[0].transform.localPosition, 1).OnComplete(() => handCards[0].GetComponent<ICard>().EnableDragging());
                handCards[1].transform.DOLocalMove(cardLocations[1].transform.localPosition, 1).OnComplete(() => handCards[1].GetComponent<ICard>().EnableDragging());
                handCards[2].transform.DOLocalMove(cardLocations[2].transform.localPosition, 1).OnComplete(() => handCards[2].GetComponent<ICard>().EnableDragging());
                handCards[3].transform.DOLocalMove(cardLocations[3].transform.localPosition, 1).OnComplete(() => handCards[3].GetComponent<ICard>().EnableDragging());
                handCards[4].transform.DOLocalMove(cardLocations[4].transform.localPosition, 1).OnComplete(() => handCards[4].GetComponent<ICard>().EnableDragging());

                handCards[0].GetComponent<ICard>().DisableDragging();
                handCards[1].GetComponent<ICard>().DisableDragging();
                handCards[2].GetComponent<ICard>().DisableDragging();
                handCards[3].GetComponent<ICard>().DisableDragging();
                handCards[4].GetComponent<ICard>().DisableDragging();
                break;
            default: 
                break;
        }
    }
}
