using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardJump : MonoBehaviour, ICard
{
    private bool isDragging = false;

    private Vector2 startPosition;

    public void ActiveCard()
    {
        PlayerController.instance.Jump();
    }

    // Update is called once per frame
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
        print("startdrag");
    }

    public void EndDrag()
    {
        isDragging = false;
        ActiveCard();
        print("enddrag");
        Destroy(gameObject);
    }
}
