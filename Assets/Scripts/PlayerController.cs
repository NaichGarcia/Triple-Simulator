using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    Vector3 movementVector;
    bool canMove = true;
    [SerializeField] Vector3 initialForceVector;
    [SerializeField] float movementSpeedZ;
    [SerializeField] [Range(0, 1)] float ballSensitivity;

    // Events
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(initialForceVector);
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            movementVector = MouseDistanceFromBall();
            playerRb.AddForce(movementVector * ballSensitivity);

            ConstantMovementZ(movementSpeedZ);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            canMove = false;
            playerRb.AddForce(new Vector3(0f, 0f, 1f) * -2, ForceMode.Impulse);
            playerRb.useGravity = true;
        }
    }

    // Methods
    Vector3 MouseDistanceFromBall()
    {
        Vector3 rayCoordinates = RaycastToBallPlane();
        Debug.Log("Raycast pos: " + (transform.position - rayCoordinates));
        return(rayCoordinates - transform.position);
    }
    
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

    void ConstantMovementZ(float speed)
    {
        transform.Translate(new Vector3(0f, 0f, 1f) * Time.deltaTime * speed);
    }
}
