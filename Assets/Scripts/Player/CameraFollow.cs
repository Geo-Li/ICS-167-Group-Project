using UnityEngine;


// Geo Li
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;

    // update camera to make it follow the player
    public void LateUpdate() {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = desiredPosition;
    }
}
