using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    void ActiveCard();

    public void EnableDragging();

    public void DisableDragging();

    public CardEnum GetCardType();

}
