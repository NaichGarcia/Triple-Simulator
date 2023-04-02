using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 cameraOffset;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + cameraOffset;
    }
}