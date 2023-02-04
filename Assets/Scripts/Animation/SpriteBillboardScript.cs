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
    private bool m_FreezeXZAxis = true;

    // Main entity that holds the rotation
    [SerializeField]
    private GameObject m_RotationOwner;

    private static float Rotation_Min = 0f, Rotation_Max = 360f;

    // Makes sure that the sprite is generally facing towards the camera
    void Update()
    {
        float flippedY = Camera.main.transform.rotation.eulerAngles.y + 180;
        float flippedX = Camera.main.transform.rotation.eulerAngles.x * -1;

        if (m_RotationOwner != null)
        {
            float characterRotation = BoundedRotation(m_RotationOwner.transform.rotation.eulerAngles.y);
            float cameraRotation = BoundedRotation(Camera.main.transform.rotation.eulerAngles.y);

            //Debug.Log(characterRotation + ", " + cameraRotation);
            Debug.Log(Vector3.Angle(Camera.main.transform.position, m_RotationOwner.transform.position));
        }

        if (m_FreezeXZAxis)
            transform.rotation = Quaternion.Euler(0f, flippedY, 0f);
        else
            transform.rotation = Quaternion.Euler(flippedX, flippedY, 0f);
    }

    private float BoundedRotation(float rotation)
    {
        float rawRotation = rotation;
        float sign = rawRotation / Mathf.Abs(rawRotation);

        float newRotation = rawRotation;
        if (rawRotation > Rotation_Max)
            newRotation = rawRotation % Rotation_Max;

        if (sign < 0)
            newRotation = Rotation_Max - newRotation;

        return newRotation;
    }
}
