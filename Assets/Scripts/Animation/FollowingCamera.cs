using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    // Local position of the camera from the player
    [SerializeField]
    private Vector3 m_LocalPosition;

    // Rotation of the camera 
    [SerializeField]
    private Vector3 m_Rotation;

    // Update is called once per frame
    void Update()
    {
        PlayerMovement p = GetComponent<PlayerMovement>();

        if (p != null && p.CanAct())
        {
            Camera.main.transform.position = transform.position + m_LocalPosition;
            Camera.main.transform.rotation = Quaternion.Euler(m_Rotation);
        }
    }
}
