using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMoveBack : MonoBehaviour, ICard
{
    public float acc = 0;
    public float waitTime = 1;
    public float timeLeft = 0;

    private bool isDragging = false;

    private Vector2 startPosition;

    public void ActiveCard()
    {
        PlayerController.instance.MoveRight();
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void StartDrag()
    {
        startPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;
        ActiveCard();
        Destroy(gameObject);
    }
}
