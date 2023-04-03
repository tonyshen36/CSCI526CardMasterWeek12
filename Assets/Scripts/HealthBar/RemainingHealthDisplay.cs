using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemainingHealthDisplay : MonoBehaviour
{
    
    public TMP_Text remainingPlayer;
    public TMP_Text remainingBoss;
    // Update is called once per frame
    void start()
    {
        
    }
    void Update()
    {
        remainingPlayer.text = PlayerController.instance.health + "/100";
        remainingBoss.text = BossController.instance.health + "/2000";
    }
    
}
