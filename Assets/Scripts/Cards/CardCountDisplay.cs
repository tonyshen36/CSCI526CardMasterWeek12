using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardCountDisplay : MonoBehaviour
{
    public TMP_Text move;
    public TMP_Text jump;
    public TMP_Text dash;
    public TMP_Text moveBack;
    // Update is called once per frame
    void Update()
    {
        move.text = "Move: " + CardManager.instance.remainingMoveCards;
        jump.text = "Jump: " + CardManager.instance.remainingJumpCards;
        dash.text = "Dash: " + CardManager.instance.remainingDashCards;
        moveBack.text = "Move Back: " + CardManager.instance.remainingMoveBackCards;
    }
}
