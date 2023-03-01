using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardJump : MonoBehaviour, ICard, IPointerEnterHandler, IPointerExitHandler
{
    private bool isDragging = false;

    private Vector2 startPosition;

    private bool enableDragging = true;

    private Tween tween;

    private int sibilingIndex;
    public CardEnum cardType = CardEnum.Jump;

    public void ActiveCard()
    {
        PlayerController.instance.Jump();
        CardManager.instance.currentCardCount--;
        CardManager.instance.handCards.Remove(this.gameObject);
        CardManager.instance.jumpCardsInHand--;
    }

    // Update is called once per frame
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
        }
    }

    public void EndDrag()
    {
        if(enableDragging) 
        { 
            isDragging = false;
            ActiveCard();
            tween.Kill();
            Destroy(gameObject);
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

    public void OnPointerEnter(PointerEventData eventData)//当鼠标进入UI后执行的事件执行的
    {
        tween = this.GetComponent<Outline>().DOFade(1, .5f).SetLoops(-1, LoopType.Yoyo);
        sibilingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)//当鼠标离开UI后执行的事件执行的
    {
        tween.Kill();
        this.GetComponent<Outline>().DOFade(0, .01f);
        transform.SetSiblingIndex(sibilingIndex);
    }
    public CardEnum GetCardType()
    {
        return cardType;
    }
}
