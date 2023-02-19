using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * 
 */
public static class Detections
{
    public static bool HasBeenHit(GameObject owner)
    {
        return false;
    }

    public static bool HasHitSomething(GameObject owner)
    {
        return false;
    }

    // Returns the distance between the enemy and its target
    public static float DistanceFromTarget(GameObject target, GameObject owner)
    {
        if (target == null)
            return -1;
        else
            return Vector3.Distance(owner.transform.position, target.transform.position);
    }
}
