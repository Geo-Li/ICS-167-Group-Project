using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Makes a sprite object in a 3D world face towards the camera in a similar vein as Paper Mario or Cult of the Lamb
 */
public class SpriteBillboardScript : MonoBehaviour
{
    // Determines if the sprite will directly look towards the camera
    // or align with the ground while still facing towards the camera
    [SerializeField] 
    protected bool m_FreezeXZAxis = false;

    // Makes sure that the sprite is generally facing towards the camera
    void Update()
    {
        AdjustRotation();
    }

    // Method that does the sprite adjustment
    protected virtual void AdjustRotation()
    {
        Vector3 angle = Camera.main.transform.rotation.eulerAngles;

        if (m_FreezeXZAxis)
            transform.rotation = Quaternion.Euler(0f, angle.y, 0f);
        else
            transform.rotation = Quaternion.Euler(angle.x, angle.y, 0f);
    }
}
