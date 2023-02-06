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

    public float acc;

    private void Awake()
    {
        if (PlayerController.instance == null) { PlayerController.instance = this; }
        else { Destroy(this); }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(acc * speed, rb.velocity.y);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 10 * speed);
    }
}
