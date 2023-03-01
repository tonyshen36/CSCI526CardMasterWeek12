using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5.0f;
    
    //public GameObject switcher;
    
    //public float closeness = 1.0f;
    
    // Update is called once per frame
    public Vector3 startPos;
    public GameObject spike;
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        if((PlayerController.instance.transform.position.x - transform.position.x)>=0 && ( PlayerController.instance.transform.position.x- transform.position.x) <=8 && PlayerController.instance.isUnderground)
        // Move the object upward based on the move speed
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime) ;
        else
        {
            transform.position = startPos;
        }
    }

}
