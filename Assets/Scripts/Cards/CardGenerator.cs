using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    public static CardGenerator instance;

    public GameObject JumpCardPrefab;
    public GameObject MoveCardPrefab;

    public bool hasJump;

    public Transform cards;

    private void Awake()
    {
        if(CardGenerator.instance == null) { CardGenerator.instance = this; }
        else { Destroy(this); }
    }

    // Update is called once per frame
    void Update()
    {
        if (cards.childCount == 0 && hasJump)
        {
            Instantiate(Random.Range(0, 2) == 0 ? JumpCardPrefab : MoveCardPrefab, cards);
            Instantiate(Random.Range(0, 2) == 0 ? JumpCardPrefab : MoveCardPrefab, cards);
            Instantiate(Random.Range(0, 2) == 0 ? JumpCardPrefab : MoveCardPrefab, cards);
        }
        else if(cards.childCount == 0)
        {
            Instantiate(MoveCardPrefab, cards);
            Instantiate(MoveCardPrefab, cards);
            Instantiate(MoveCardPrefab, cards);
        }
    }
}
