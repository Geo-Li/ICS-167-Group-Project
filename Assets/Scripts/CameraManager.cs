using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothingSpeed;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Quaternion cameraRotation;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        //Move camera to the targets position while keeping the offset
        Vector3 endPosition = target.position + cameraOffset;
        transform.rotation = cameraRotation;
        //Smooth the camera so it can follow the player without snapping into place based on their velocity and frame rate (Time.deltaTime)
        transform.position =  Vector3.SmoothDamp(transform.position, endPosition, ref velocity, smoothingSpeed * Time.deltaTime);
    }//end LateUpdate
}//end CameraManager
