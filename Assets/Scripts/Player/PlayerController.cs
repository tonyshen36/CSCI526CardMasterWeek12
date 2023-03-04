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
    bool isMovingRight = false;
    bool isMovingLeft = false;

    public float acc;
    
    //fall and restart
    public Vector3 respawnPoint; //recall where palyer restart
    public Vector3 checkPoint;
    public GameObject fallDetector; //link the script to FallDetector

    // Variable to record previous frame player position
    private Vector3 previousPosition;
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
        // Initilize previousPosition to be start of player
        previousPosition = transform.position;
    }

    private void Update()
    {
        rb.velocity = new Vector2(acc * speed, rb.velocity.y);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

        //If previous frame x position is the same as current x position and player is stationary, record respawn point
        if (previousPosition.x == transform.position.x && rb.velocity == new Vector2(0, 0))
        {
            respawnPoint = transform.position;
        }
        else
        {
            previousPosition = transform.position;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colliObject = collision.gameObject;
        // Check if the collision is with an object tagged as "Player"
        if (colliObject.CompareTag("Rock"))
        {
            if (acc > 20)
            {
                colliObject.SetActive(false);
                rockpieces.SetActive(true);
            }

            // Print a message to the console
            Debug.Log("The player collided with this object!");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector" )
        {
            transform.position = respawnPoint;
            rb.velocity = new Vector2(0, 0);
        }
        else if (collision.tag == "Monster")
        {
            transform.position = respawnPoint;
            rb.velocity = new Vector2(0, 0);
        }
        else if(collision.tag == "Spike")
        {
            transform.position = checkPoint;
            rb.velocity = new Vector2(0, 0);
            moveTimeLeft = 0;
        }
        else if(collision.tag == "Checkpoint")
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
