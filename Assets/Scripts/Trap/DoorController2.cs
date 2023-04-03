using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController2 : MonoBehaviour
{
    public Transform door;
    public GameObject switcher;
    public bool isOpen = false;
    
    public float compressionFactor = 0.0f;
    public float compressionTime = 2.0f;
    void Update()
    {
        if (!isOpen && switcher.GetComponent<SpriteRenderer>().color == Color.green)
        {
            StartCoroutine(CompressOverTime());
        }
        
    }
    
    private IEnumerator CompressOverTime()
    {
        // Get the initial scale and position of the GameObject
        Vector3 initialScale = door.localScale;
        Vector3 initialPosition = door.position;

        // Calculate the final scale and position based on the compression factor
        Vector3 finalScale = new Vector3(initialScale.x, 0.0f, initialScale.z);
        Vector3 finalPosition = new Vector3(initialPosition.x, initialPosition.y + (initialScale.y - finalScale.y) / 2.0f, initialPosition.z);

        // Calculate the compression speed
        float compressionSpeed = (initialScale.y - finalScale.y) / compressionTime;

        // Gradually compress the object over time
        while (door.localScale.y > finalScale.y)
        {
            Vector3 newScale = door.localScale;
            newScale.y -= compressionSpeed * Time.deltaTime;
            door.localScale = newScale;

            Vector3 newPosition = door.position;
            newPosition.y += compressionSpeed * Time.deltaTime / 2.0f;
            door.position = newPosition;

            yield return null;
        }

        // Set the final scale and position to ensure accuracy
        door.localScale = finalScale;
        door.position = finalPosition;

        isOpen = true;
    }
   

}