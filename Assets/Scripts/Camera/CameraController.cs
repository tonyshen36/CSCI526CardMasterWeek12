using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private Vector3 CamaeraPos;
    [SerializeField] Vector3 offset;
    private float CamaeraSpeed = 1;

    private void Start()
    {
        player = PlayerController.instance.transform;
        CamaeraPos = player.position + offset;
        this.transform.position = CamaeraPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            CamaeraPos.y += CamaeraSpeed / 100;
        }
        if (Input.GetKey(KeyCode.S))
        {
            CamaeraPos.y -= CamaeraSpeed / 100;
        }
        if (Input.GetKey(KeyCode.A))
        {
            CamaeraPos.x += CamaeraSpeed / 100;
        }
        if (Input.GetKey(KeyCode.D))
        {
            CamaeraPos.x -= CamaeraSpeed / 100;
        }
        this.transform.position = CamaeraPos;
    }
}
