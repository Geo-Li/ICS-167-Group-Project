using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private EntitySO entity;
    [HideInInspector] private float playerSpeed;
    [HideInInspector] private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = entity.speed;
        startPosition = transform.position;
    }


    // Get player's current velocity
    public float GetVelocity() {
        return gameObject.GetComponent<Rigidbody>().velocity.magnitude;
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

        // TODO:
        // Make the player position normalized in y-axis
        // update player position based on keyboard input
        float translation = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;

        transform.Translate(rotation,0,translation);
    }
}
