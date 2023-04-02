using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 initialForceVector;
    [SerializeField] [Range(0, 1)] float ballSensitivity;
    [SerializeField] float forwardSpeed;

    Rigidbody playerRb;
    Vector3 movementVector;

    bool canMove = true;

    // Events
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(initialForceVector);
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    private void OnTriggerEnter(Collider other) 
    {
        // Bounce back if the player collides with an "obstacle"
        if(other.gameObject.CompareTag("Obstacle"))
        {
            canMove = false;
            playerRb.AddForce(new Vector3(0f, 0f, 1f) * -2, ForceMode.Impulse);
            playerRb.useGravity = true;
        }
    }

    // Methods

    // Moves the ball depending on the mouse position
    void PlayerMovement()
    {
        if(!canMove) { return; }

        movementVector = MouseDistanceFromBall();
        playerRb.AddForce(movementVector * ballSensitivity);

        ForwardMovement(forwardSpeed);
    }

    // Checks the fixed distance between the ball and the mouse
    Vector3 MouseDistanceFromBall()
    {
        Vector3 rayCoordinates = RaycastToBallPlane();
        Debug.Log("Raycast pos: " + (transform.position - rayCoordinates));
        return(rayCoordinates - transform.position);
    }
    
    // Creates a xy Plane at the ball's position and checks mouse position within the Plane
    Vector3 RaycastToBallPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane xyPlane = new Plane(
            new Vector3(0f, 0f, -1f), 
            transform.position
        );

        float distance = 0;

        if(xyPlane.Raycast(ray, out distance))
        {
            return(ray.GetPoint(distance));
        }

        return Vector3.zero;
    }

    // Constant +Z movement
    void ForwardMovement(float speed)
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
