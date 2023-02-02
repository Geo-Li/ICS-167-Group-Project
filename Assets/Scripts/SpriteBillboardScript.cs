using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Paper Mouse Games at 2D Sprites in 3D World - Unity Sprite Billboarding Tutorial 
// link : https://www.youtube.com/watch?v=FjJJ_I9zqJo

/*
 * Makes a sprite object in a 3D world face towards the camera in a similar vein as Paper Mario or Cult of the Lamb
 */
public class SpriteBillboardScript : MonoBehaviour
{
    // Determines if the sprite will directly look towards the camera
    // or align with the ground while still facing towards the camera
    [SerializeField] 
    private bool freezeXZAxis = true;

    // Makes sure that the sprite is generally facing towards the camera
    void Update()
    {
        if (freezeXZAxis)
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        else
            transform.rotation = Camera.main.transform.rotation;
    }
}
