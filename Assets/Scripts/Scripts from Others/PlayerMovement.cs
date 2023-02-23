
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private EntitySO entity;
    [HideInInspector] private float playerSpeed, currentSpeed;
    [HideInInspector] private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = entity.speed;
        startPosition = transform.position;
    }

    // Get player's current velocity
    public float GetVelocity() 
    {
        //return gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        return currentSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // check if user has fallen the table, if so make them go back
        // to the start position
        if (transform.position.y < 0) {
            transform.position = startPosition;
        }
    }

    void FixedUpdate() 
    {
        // TODO:
        // Make the player position normalized in y-axis
        // update player position based on keyboard input
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(inputX, 0f, inputZ);

        if (movementVector.magnitude < playerSpeed)
            movementVector = movementVector.normalized;

        movementVector *= playerSpeed * Time.deltaTime;

        transform.position += movementVector;

        currentSpeed = movementVector.magnitude;
        transform.rotation = Quaternion.LookRotation(movementVector);
    }
}
