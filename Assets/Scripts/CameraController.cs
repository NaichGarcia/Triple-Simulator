using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 cameraOffset;

    void LateUpdate()
    {
        FollowPlayer();
    }

    // Methods

    // Follow player's movement
    void FollowPlayer()
    {
        // Checks if can/should follow the player
        if(!player) { return; }
        if(player.GetComponent<PlayerController>().gameOver) { return; }

        transform.position = player.transform.position + cameraOffset;
    }
}
