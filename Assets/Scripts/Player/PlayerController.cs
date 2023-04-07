using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField]
    private float speed = 1.0f;

    public int health = 1000;
    public int boss_index;
    private Rigidbody2D rb;
    private float input;
    private BoxCollider2D boxCollider;

    public float moveWaitTime = 0.2f;
    public float moveTimeLeft = 0;
    public bool isMovingRight = false;
    public bool isMovingLeft = false;
    
    public float acc;

    private int move_counter;
    private int back_counter;
    private int jump_counter;
    private int dash_counter;
    //private int slash_counter;
    
    // Bool to reset spike in scene 2
    public bool resetSpike = false;
    
    //fall and restart
    public Vector3 respawnPoint; //recall where palyer restart
    public Vector3 checkPoint;
    public GameObject fallDetector; //link the script to FallDetector
    

    //check if player is undeground
    public bool isUnderground=false;

    //public GameObject rockpieces;
    private void Awake()
    {
        if (PlayerController.instance == null) { PlayerController.instance = this; }
        else { Destroy(this); }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        
        respawnPoint = transform.position;
        move_counter = 0;
        jump_counter = 0;
        back_counter = 0;
        dash_counter = 0;
        //slash_counter = 0;
        }

    private void Update()
    {
        rb.velocity = new Vector2(acc * speed, rb.velocity.y);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

        if (health <= 0)
        {
            transform.position = checkPoint;
            rb.velocity = new Vector2(0, 0);
            moveTimeLeft = 0;
        }
    }
    
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     GameObject colliObject = collision.gameObject;
    //     // Check if the collision is with an object tagged as "Player"
    //     if (colliObject.CompareTag("Rock"))
    //     {
    //         if (acc > 20)
    //         {
    //             colliObject.SetActive(false);
    //             rockpieces.SetActive(true);
    //         }
    //
    //         // Print a message to the console
    //         Debug.Log("The player collided with this object!");
    //     }
    // }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector" )
        {
            float current_x = transform.position.x;
            float current_y = transform.position.y;
            transform.position = checkPoint;
            rb.velocity = new Vector2(0, 0);
            Analyzer.instance.sendDeathData(current_x, current_y, "FallDetector");
        }
        else if (collision.tag == "Spike")
        {
            float spike_current_x = transform.position.x;
            float spike_current_y = transform.position.y;
            Analyzer.instance.sendDeathData(spike_current_x, spike_current_y, "Spike");
            transform.position = checkPoint;
            rb.velocity = new Vector2(0, 0);
            moveTimeLeft = 0;
            Debug.Log("Spike");
        }
        else if (collision.tag == "Checkpoint")
        {
            checkPoint = collision.transform.position;
            rb.velocity = new Vector2(0, 0);
        }
        else if (collision.tag == "Underground")
        {
            isUnderground = true;
        }
        // else if (collision.tag == "Rock")
        // {
        //     GameObject colliObject = collision.gameObject;
        //     if (acc > 20)
        //     {
        //         colliObject.SetActive(false);
        //     }
        // }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            float spike_current_x = transform.position.x;
            float spike_current_y = transform.position.y;
            Analyzer.instance.sendDeathData(spike_current_x, spike_current_y, "Monster");
            transform.position = checkPoint;
            rb.velocity = new Vector2(0, 0);
            Debug.Log("Monster");
        }

        else if (collision.gameObject.tag == "Rock")
        {
            GameObject colliObject = collision.gameObject;
            if (acc > 20)
            {
                colliObject.SetActive(false);
            }
        }
        else if (collision.gameObject.tag == "key")
        {
            GameObject doorObject = GameObject.FindWithTag("door");
            GameObject keyObject = GameObject.FindWithTag("key");
            Destroy(doorObject);
            Destroy(keyObject);
            Debug.Log("Key get");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Underground")
        {
            isUnderground = false;
        }
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 10 * speed);
    }

    public void MoveRight()
    {
        if(isMovingRight) { moveTimeLeft += moveWaitTime; }
        else if (isMovingLeft) { moveTimeLeft = moveWaitTime; }
        else { StartCoroutine(Move(15)); }
    }
    public void MoveBack()
    {
        if (isMovingLeft) { moveTimeLeft += moveWaitTime; }
        else if (isMovingRight) { moveTimeLeft = moveWaitTime; }
        else { StartCoroutine(Move(-15)); }
    }

    public void Dash()
    {
        if(isMovingRight) { moveTimeLeft += moveWaitTime; }
        else if (isMovingLeft) { moveTimeLeft = moveWaitTime; }
        else { StartCoroutine(Move(30)); }
    }

    public void DashBack()
    {
        if (isMovingLeft) { moveTimeLeft += moveWaitTime; }
        else if (isMovingRight) { moveTimeLeft = moveWaitTime; }
        else { StartCoroutine(Move(-30)); }
    }
    
    //Slash card operation
    public void Slash()
    {
        //StartCoroutine(SlashCoroutine());
        StartCoroutine(ElectricShockCoroutine());
    }

    public float shockDuration = 1f;
    public float shockRange = 10f;
    public int shockDamage = 5;
    private Collider2D[] hitEnemies;
    
    public GameObject lightningObject;
    
    private IEnumerator ElectricShockCoroutine()
{
    float startTime = Time.time;

    // Use a BoxCollider2D for the wider attack range
    BoxCollider2D shockCollider = gameObject.AddComponent<BoxCollider2D>();
    shockCollider.isTrigger = true;
    shockCollider.size = new Vector2(shockRange, shockRange);
    
    while (Time.time < startTime + shockDuration)
    {
        hitEnemies = Physics2D.OverlapBoxAll(transform.position, new Vector2(shockRange, shockRange), 0);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            // Apply damage and instantiate lightning effect
            if (enemyCollider.CompareTag("Boss"))
            {
                GameObject enemyGameObject = enemyCollider.gameObject;
                BossController enemyScript = enemyGameObject.GetComponent<BossController>();
                enemyScript.health -= shockDamage;
                if (enemyScript.health <= 0)
                {
                    enemyGameObject.SetActive(false);
                    // Handle boss death
                }

                UpdateLightningPositionAndScale(enemyGameObject);

                // Set the lightning trigger to play the animation
                Animator lightningAnimator = lightningObject.GetComponent<Animator>();
                lightningAnimator.SetTrigger("PlayLightning");

                lightningObject.SetActive(true);
            }
            else if (enemyCollider.CompareTag("Root"))
            {
                GameObject enemyGameObject = enemyCollider.gameObject;
                RootController enemyScript = enemyGameObject.GetComponent<RootController>();
                enemyScript.health -= shockDamage;
                
                UpdateLightningPositionAndScale(enemyGameObject);

                // Set the lightning trigger to play the animation
                Animator lightningAnimator = lightningObject.GetComponent<Animator>();
                lightningAnimator.SetTrigger("PlayLightning");

                lightningObject.SetActive(true);
            }
        }

        yield return null;
    }

    lightningObject.SetActive(false);

    // Remove the shock collider and lightning effect after the electric shock is complete
    Destroy(shockCollider);
}

private void UpdateLightningPositionAndScale(GameObject enemyGameObject)
{
    // Update the position and rotation of the lightning
    Vector3 direction = enemyGameObject.transform.position - transform.position;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    // Calculate the distance between the player and the enemy
    float distance = Vector3.Distance(transform.position, enemyGameObject.transform.position);

    // Set the lightning's scale based on the distance
    lightningObject.transform.localScale = new Vector3(distance, lightningObject.transform.localScale.y, lightningObject.transform.localScale.z);

    // Set the lightning's position to be in the middle of the player and the enemy
    lightningObject.transform.position = (transform.position + enemyGameObject.transform.position) / 2;

    lightningObject.transform.rotation = rotation;
}

    public void sendCardStatToAnalyzer(bool result)
    {
        Analyzer.instance.sendCardData(result, move_counter, back_counter, jump_counter, dash_counter);
    }

    private IEnumerator Move(float speed)
    {
        isMovingRight = true;
        moveTimeLeft = moveWaitTime;
        acc = speed;
        while (moveTimeLeft > 0)
        {
            //acc = Mathf.Lerp(0, 10, (waitTime - timeLeft) / waitTime);
            //PlayerController.instance.acc = acc;
            moveTimeLeft -= Time.deltaTime;
            yield return null;
        }
        acc = 0;
        isMovingRight = false;
    }
}
