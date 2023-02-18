using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public EntitySO entity;
    private float playerSpeed;
    // private float playerRotationSpeed = 100f;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = entity.speed;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // update player position based on keyboard input
        float translation = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;

        transform.Translate(rotation,0,translation);

        // check if user has fallen the table, if so make them go back
        // to the origin
        if (transform.position.y < 0) {
            transform.position = startPosition;
        }
    }
}
