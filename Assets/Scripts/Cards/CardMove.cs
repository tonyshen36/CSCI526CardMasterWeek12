using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMove : MonoBehaviour, ICard
{
    public float acc = 0;
    public float waitTime = 1;
    public float timeLeft = 0;

    private bool isDragging = false;

    private Vector2 startPosition;

    public void ActiveCard()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        timeLeft = waitTime;
        while (timeLeft > 0)
        {
            acc = Mathf.Lerp(0, 10, (waitTime - timeLeft) / waitTime);
            PlayerController.instance.acc = acc;
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        acc = 0;
        PlayerController.instance.acc = acc;
        Destroy(gameObject);
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
        print("startdrag");
    }

    public void EndDrag()
    {
        isDragging = false;
        ActiveCard();
        print("enddrag");
        GetComponent<Image>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
