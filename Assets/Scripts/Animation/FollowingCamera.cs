using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_LocalPosition, m_Rotation;

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = transform.position + m_LocalPosition;
        Camera.main.transform.rotation = Quaternion.Euler(m_Rotation);
    }
}
