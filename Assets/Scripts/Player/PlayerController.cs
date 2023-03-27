using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField]
    private float speed = 1.0f;

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
    // Bool to reset spike in scene 2
    public bool resetSpike = false;
    
    //fall and restart
    public Vector3 respawnPoint; //recall where palyer restart
    public Vector3 checkPoint;
    public GameObject fallDetector; //link the script to FallDetector

    //check if player is undeground
    public bool isUnderground=false;

    public GameObject rockpieces;
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
    }

    private void Update()
    {
        rb.velocity = new Vector2(acc * speed, rb.velocity.y);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
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
                rockpieces.SetActive(true);

            }
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
