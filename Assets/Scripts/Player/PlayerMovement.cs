using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private string whichKeyboardController;
    [SerializeField] private float playerSpeed = 500f;
    [HideInInspector] private Vector3 startPosition;
    [HideInInspector] private Rigidbody playerRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        // playerSpeed = entity.speed;
        startPosition = transform.position;
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
    }


    // Get player's current velocity
    public float GetVelocity() {
        return playerRigidbody.velocity.magnitude;
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

    void FixedUpdate() {
        Vector3 move = new Vector3(0, 0, 0);
        if (whichKeyboardController == "Arrows") {
            move = new Vector3(Input.GetAxis("Horizontal_Arrows"), 0, Input.GetAxis("Vertical_Arrows"));
        } else if (whichKeyboardController == "WASD") {
            move = new Vector3(Input.GetAxis("Horizontal_WASD"), 0, Input.GetAxis("Vertical_WASD"));
        }
        playerRigidbody.velocity = move * playerSpeed * Time.deltaTime;
    }
}
