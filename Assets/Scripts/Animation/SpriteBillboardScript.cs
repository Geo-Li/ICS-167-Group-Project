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

    private float m_AngleZ;

    // Sets up the script
    void Start()
    {
        m_AngleZ = transform.rotation.eulerAngles.z;
    }

    // Makes sure that the sprite is generally facing towards the camera
    void Update()
    {
        AdjustRotation();
    }

    // Method that does the sprite adjustment
    protected virtual void AdjustRotation()
    {
        Vector3 angle = Camera.main.transform.rotation.eulerAngles;

        SetRotation(angle.x, angle.y);
    }

    // Method that enables direct rotation of the sprite
    public void SetRotation(float angleX, float angleY)
    {
        if (m_FreezeXZAxis)
            transform.rotation = Quaternion.Euler(0f, angleY, m_AngleZ);
        else
            transform.rotation = Quaternion.Euler(angleX, angleY, m_AngleZ);
    }
}
