using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed = 5.0f; // the speed at which the object moves
    private int direction = 1; // the direction the object is currently moving (-1 = left, 1 = right)

    void Update()
    {
        // move the object left or right based on the current direction and speed
        transform.Translate(new Vector3(direction * speed * Time.deltaTime, 0, 0));

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the object has collided with the left or right wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // reverse the direction of movement
            direction *= -1;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
