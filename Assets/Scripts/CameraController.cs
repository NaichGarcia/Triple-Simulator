using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 cameraOffset;

    GameManager gameManager;

    Vector3 movingAwayOffset = new Vector3(0f, 3f, -5f);

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

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
        if(!gameManager.isGameActive || gameManager.scoredTriple) { return; }

        transform.position = player.transform.position + cameraOffset;
    }

    public void StartMovingAway()
    {
        Vector3 finalPoint = transform.position + movingAwayOffset;
        
        StartCoroutine(LerpPosition(finalPoint, 5));
    }

    // Lerps smoothly the camera position to the specified point
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while(time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, curve.Evaluate(time / duration));
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
