using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Detects when a given target is visble to this object. A target is
// visible when it's both in range and in front of the target. Both the
// range and the angle of visibility are configurable.
public class EnemyVisibility : MonoBehaviour
{
    // The object we're looking for.
    public Transform target = null;

    // If the object is more than this distance away, we can't see it.
    public float maxDistance = 10f;

    // The angle of our arc of visibility.
    [Range(0f, 360f)]
    public float angle = 45f;

    // If true, visualize changes in visibility by changing material color
    [SerializeField] bool visualize = true;

    // A property that other classes can access to determine if we can
    // currently see our target.
    public bool targetIsVisible { get; private set; }

    // Check to see if we can see the target every frame.
    void Update()
    {
        targetIsVisible = CheckVisibility();

        if (visualize)
        {
            // Update our color: yellow if we can see the target, white if we can't
            var color = targetIsVisible ? Color.yellow : Color.white;

            GetComponent<Renderer>().material.color = color;
        }
    }

    // Returns true if this object can see the specified position.
    public bool CheckVisibilityToPoint(Vector3 worldPoint)
    {
        // Calculate the direction from our location to the point
        var directionToTarget = worldPoint - transform.position;

        // Calculate the number of degrees from the forward direction.
        float degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);

        // The target is within the arc if it's within half of the
        // specified angle. If it's not within the arc, it's not visible.
        if (degreesToTarget >= (angle / 2))
            return false;

        // Figure out the distance to the target
        var distanceToTarget = directionToTarget.magnitude;

        // Take into account our maximum distance
        var rayDistance = Mathf.Min(maxDistance, distanceToTarget);

        // Create a new ray that goes from our current location, in the specified direction
        var ray = new Ray(transform.position, directionToTarget);

        // Stores information about anything we hit
        RaycastHit hit;

        // Perform the raycast. Did it hit anything?
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // We hit something.
            if (hit.collider.transform == target)
            {
                // It was the target itself. We can see the target point.
                return true;
            }
            // It's something between us and the target. We cannot see the target point.
            return false;
        }
        else
        {
            // There's an unobstructed line of sight between
            // us and the target point, so we can see it.
            return true;
        }
    }

    // Returns true if a straight line can be drawn between this object and the target.
    // The target must be within range, and within the visible arc.
    public bool CheckVisibility()
    {
        // Calculate the direction from our location to the point
        var directionToTarget = target.position - transform.position;

        // Calculate the number of degrees from the forward direction.
        float degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);

        // The target is within the arc if it's within half of the
        // specified angle. If it's not within the arc, it's not visible.
        if (degreesToTarget >= (angle / 2))
            return false;

        // Figure out the distance to the target
        var distanceToTarget = directionToTarget.magnitude;

        // Take into account our maximum distance
        var rayDistance = Mathf.Min(maxDistance, distanceToTarget);

        // Create a new ray that goes from our current location, in the specified direction
        var ray = new Ray(transform.position, directionToTarget);

        // Stores information about anything we hit
        RaycastHit hit;

        // Records info about whether the target is in range and not occluded
        var canSee = false;

        // Perform the raycast. Did it hit anything?
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // We hit something.
            if (hit.collider.transform == target)
            {
                // It was the target itself. We can see the target point.
                canSee = true;
            }
            // Visualize the ray
            Debug.DrawRay(transform.position, hit.point);
        }
        else
        {
            // The ray didn't hit anything. This means that it reached
            // the maximum distance and stopped, which means we didn't
            // hit our target. It must be out of range.

            // Visualize the ray
            Debug.DrawRay(transform.position, directionToTarget.normalized * rayDistance);
        }

        return canSee;
    }
}

#if UNITY_EDITOR
// A custom editor for the EnemyVisibility class.
// Visuallizes and allows for editing the visible range.
[CustomEditor(typeof(EnemyVisibility))]
public class EnemyVisibilityEditor: Editor
{
    // Called when Unity needs to fraw the Scene view.
    private void OnSceneGUI()
    {
        // Get a reference to the EnemeyVisibility script we're looking at
        var visibility = target as EnemyVisibility;

        // Start drawing at 10% opacity
        Handles.color = new Color(1, 1, 1, .1f);

        var forwardPointMinusHalfAngle = Quaternion.Euler(0, -visibility.angle / 2, 0) * visibility.transform.forward;

        Vector3 arcStart = forwardPointMinusHalfAngle * visibility.maxDistance;

        Handles.DrawSolidArc(
            visibility.transform.position,  // Center of the arc
            Vector3.up,                     // Up direction of the arc
            arcStart,                       // Point where it begins
            visibility.angle,               // Angle of the arc
            visibility.maxDistance          // Radius of the arc
        );

        // Draw a scale handle at the edge of the arc;
        // if the user drags it, update the arc size.

        Handles.color = Color.white;

        // Position of handle
        Vector3 handlePosition = visibility.transform.position + visibility.transform.forward * visibility.maxDistance;

        // Draw the handle, and store its result.
        visibility.maxDistance = Handles.ScaleValueHandle(
            visibility.maxDistance,         // cureent value
            handlePosition,                 // handle position
            visibility.transform.rotation,  // orientation
            1,                              // size
            Handles.ConeHandleCap,          // cap to draw
            .25f);                          // snap to multiples of this
                                            // if the snapping key is held down
    }
}
#endif