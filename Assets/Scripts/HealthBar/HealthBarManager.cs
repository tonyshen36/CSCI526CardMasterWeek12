using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Image playerHealthBar;
    public Image bossHealthBar;

    public float playerHealthPercentage;

    public float bossHealthPercentage;
    // private Player player;
    // private Boss boss;

    private int playerMaxHealth;
    private int bossMaxHealth;

    void Start()
    {
        // player = FindObjectOfType<Player>();
        // boss = FindObjectOfType<Boss>();

        playerMaxHealth = PlayerController.instance.health;
        bossMaxHealth = BossController.instance.health;
    }

    void Update()
    {
         playerHealthPercentage = (float) PlayerController.instance.health / playerMaxHealth; 
         bossHealthPercentage = (float)BossController.instance.health / bossMaxHealth;

        playerHealthBar.fillAmount = playerHealthPercentage;
        bossHealthBar.fillAmount = bossHealthPercentage;
    }
}

