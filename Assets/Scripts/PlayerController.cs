using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 initialForceVector;
    [SerializeField] [Range(0, 1)] float ballSensitivity;
    [SerializeField] float forwardSpeed;

    Rigidbody playerRb;
    GameObject basket;
    Vector3 movementVector;

    public bool canMove = true;
    public bool gameOver = false;
    public bool scoredTriple = false;
    private float offset = 2f;

    // Events
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(initialForceVector);

        basket = GameObject.Find("Basket");
    }

    void Update()
    {
        CheckLoseConditions();
    }

    void FixedUpdate()
    {
        PlayerMovement();
        ForwardMovement(forwardSpeed);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Table"))
        {
            canMove = false;
            playerRb.AddForce(new Vector3(0f, 0f, 1f) * -2, ForceMode.Impulse);

            return;
        }

        // Lose if the player collides with some obstacle
        TriggerGameOverState();
    }

    private void OnTriggerEnter(Collider other) 
    {
        // Player scored a basket
        if(other.gameObject.CompareTag("Basket"))
        {
            TriggerScoredTripleState();
        }
    }

    // Methods

    // Moves the ball depending on the mouse position
    void PlayerMovement()
    {
        if(!canMove) { return; }

        movementVector = MouseDistanceFromBall();
        playerRb.AddForce(movementVector * ballSensitivity);
    }

    // Checks the fixed distance between the ball and the mouse
    Vector3 MouseDistanceFromBall()
    {
        Vector3 rayCoordinates = RaycastToBallPlane();
        //Debug.Log("Raycast pos: " + (transform.position - rayCoordinates));

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
        if(!canMove) { return; }

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    // Check almost every lose condition
    void CheckLoseConditions()
    {
        // Ball went past the basket
        if(transform.position.z > basket.transform.position.z + offset)
        {
            Debug.Log("PASA");
            TriggerGameOverState();
        }
    }

    // Toggle lose state flags
    void TriggerGameOverState()
    {
        if(gameOver || scoredTriple) { return; }

        gameOver = true;
        canMove = false;
        playerRb.useGravity = true;

        Debug.Log("Game Over");
    }

    void TriggerScoredTripleState()
    {
        scoredTriple = true;
        Debug.Log("Triple!");

        gameObject.SetActive(false);
    }
}
