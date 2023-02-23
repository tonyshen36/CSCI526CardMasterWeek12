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
    public GameObject fallDetector; //link the script to FallDetector

    // Variable to record previous frame player position
    private Vector3 previousPosition;


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

        // If previous frame x position is the same as current x position and player is stationary, record respawn point
        if (previousPosition.x == transform.position.x && rb.velocity == new Vector2(0, 0))
        {
            respawnPoint = transform.position;
        }
        else
        {
            previousPosition = transform.position;
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
            transform.position = respawnPoint;
            rb.velocity = new Vector2(0, 0);
            moveTimeLeft = 0;

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
