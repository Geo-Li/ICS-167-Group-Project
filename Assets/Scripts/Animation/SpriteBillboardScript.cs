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
    private bool m_FreezeXZAxis = false;

    // Main entity that holds the rotation
    [SerializeField]
    private GameObject m_RotationOwner;

    private static float Rotation_Min = 0f, Rotation_Max = 360f;
    private Vector3 ZeroAxis = Vector3.right;

    // Makes sure that the sprite is generally facing towards the camera
    void Update()
    {
        float flippedY = Camera.main.transform.rotation.eulerAngles.y;
        float flippedX = Camera.main.transform.rotation.eulerAngles.x;

        if (m_RotationOwner != null)
        {
            Vector3 m_MyCenterVector = m_RotationOwner.transform.position;
            Vector3 m_MyFirstVector = m_RotationOwner.transform.right + m_MyCenterVector;
            Vector3 m_MySecondVector = -Camera.main.transform.forward + m_MyCenterVector;

            bool facingForward = IsFacingForward(m_MyFirstVector, m_MySecondVector, m_MyCenterVector, ZeroAxis);

            Debug.DrawLine(m_MyCenterVector, m_MyFirstVector, Color.magenta);
            Debug.DrawLine(m_MyCenterVector, m_MySecondVector, Color.blue);

            if (!facingForward)
            {
                flippedY += 180;
                flippedX *= -1;
            }
        }

        if (m_FreezeXZAxis)
            transform.rotation = Quaternion.Euler(0f, flippedY, 0f);
        else
            transform.rotation = Quaternion.Euler(flippedX, flippedY, 0f);
    }

    private bool IsFacingForward(Vector3 vector1, Vector3 vector2, Vector3 vectorCenter, Vector3 zeroAxis)
    {
        return FacingAngle(vector1, vector2, vectorCenter, zeroAxis) > 0;
    }

    private float FacingAngle(Vector3 vector1, Vector3 vector2, Vector3 vectorCenter, Vector3 zeroAxis)
    {
        vector1.y = 0f;
        vector2.y = 0f;
        vectorCenter.y = 0f;

        Vector3 v1 = vector1 - vectorCenter;
        Vector3 v2 = vector2 - vectorCenter;

        float zDis1 = zeroAxis.z - v1.z - zeroAxis.z;
        float zDis2 = zeroAxis.z - v2.z;

        float sign1 = GetSign(zDis1);
        float sign2 = GetSign(zDis2);

        float angle1 = Vector3.Angle(zeroAxis, v1) * sign1;
        float angle2 = Vector3.Angle(zeroAxis, v2) * sign2;

        float finalAngle = angle2 - angle1;

        if (finalAngle < -180f)
            finalAngle += 360;
        else if (finalAngle > 180f)
            finalAngle -= 360;

        return finalAngle;
    }

    private float GetSign(float number)
    {
        float result = number / Mathf.Abs(number);

        if (float.IsNaN(result) || float.IsInfinity(result))
            result = 0;

        return result;
    }
}
