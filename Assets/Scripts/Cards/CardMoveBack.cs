using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMoveBack : MonoBehaviour, ICard, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public float acc = 0;
    public float waitTime = 1;
    public float timeLeft = 0;

    private bool isDragging = false;

    private Vector2 startPosition;

    private bool enableDragging = true;

    private Tween tween;

    private int sibilingIndex;
    public CardEnum cardType = CardEnum.MoveBack;
    public void ActiveCard()
    {
        PlayerController.instance.MoveBack();
        CardManager.instance.currentCardCount--;
        CardManager.instance.handCards.Remove(this.gameObject);
        CardManager.instance.moveBackCardsInHand--;
        Destroy(gameObject);
    }

    void Update()
    {
        if (isDragging && enableDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void StartDrag()
    {
        if (enableDragging)
        {
            startPosition = transform.position;
            isDragging = true;
            Color temp = this.GetComponent<Outline>().effectColor;
            this.GetComponent<Outline>().effectColor = new Color(temp.r, temp.g, temp.b, 0);
        }
    }

    public void EndDrag()
    {
        if (enableDragging)
        {
            isDragging = false;
            if (CardStack.instance.cards.Contains(this))
            {
                CardStack.instance.cards.Remove(this);
                tween.Kill();
                ActiveCard();
            }
            else
            {
                CardStack.instance.cards.Add(this);
                transform.position = startPosition;
                Color temp = this.GetComponent<Outline>().effectColor;
                this.GetComponent<Outline>().effectColor = new Color(temp.r, temp.g, temp.b, 1);
            }
        }
    }

    public void EnableDragging()
    {
        enableDragging = true;
    }

    public void DisableDragging()
    {
        enableDragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CardStack.instance.executing) return;
        if (CardStack.instance.cards.Contains(this))
        {
            CardStack.instance.cards.Remove(this);
            Color temp = this.GetComponent<Outline>().effectColor;
            tween.Kill();
            this.GetComponent<Outline>().effectColor = new Color(temp.r, temp.g, temp.b, 0);
        }
        else
        {
            CardStack.instance.cards.Add(this);
            Color temp = this.GetComponent<Outline>().effectColor;
            tween.Kill();
            this.GetComponent<Outline>().effectColor = new Color(temp.r, temp.g, temp.b, 1);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)//当鼠标进入UI后执行的事件执行的
    {
        if (!CardStack.instance.cards.Contains(this)) tween = this.GetComponent<Outline>().DOFade(1, .5f).SetLoops(-1, LoopType.Yoyo);
        sibilingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)//当鼠标离开UI后执行的事件执行的
    {
        tween.Kill();
        if (!CardStack.instance.cards.Contains(this)) this.GetComponent<Outline>().DOFade(0, .01f);
        transform.SetSiblingIndex(sibilingIndex);
    }

    public CardEnum GetCardType()
    {
        return cardType;
    }
}
