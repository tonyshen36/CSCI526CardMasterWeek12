using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    private float orizoomsize = 5.0f;
    public float zoomsize = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("M");
            var camera = Camera.main;
            var brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
            var vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;
            if (vcam.m_Lens.OrthographicSize == orizoomsize)
            {
                vcam.m_Lens.OrthographicSize = zoomsize;
            } else
            {
                vcam.m_Lens.OrthographicSize = orizoomsize;
            }
            
        }
    }
}
