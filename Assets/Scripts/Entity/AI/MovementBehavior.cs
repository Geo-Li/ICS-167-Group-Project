using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Methods for initiating enemy movement behaviors
 */
public static class MovementBehavior
{
    private const float LOOK_AHEAD_MULTIPLIER = 5, TO_TARGET_ANGLE = 90, RELATIVE_ANGLE = 20;

    // Have the enemy agent seek a certain location
    public static void Seek(NavMeshAgent agent, Vector3 location)
    {
        agent.SetDestination(location);
    }

    // Have the enemy agent flee from a certain location
    public static void Flee(NavMeshAgent agent, Vector3 location, Vector3 ownerLocation)
    {
        Vector3 fleeVector = location - ownerLocation;
        agent.SetDestination(ownerLocation - fleeVector);
    }

    // Have the enemy agent intelligently cut you off during a chase
    public static void Pursue(NavMeshAgent agent, GameObject target, float targetSpeed, GameObject owner)
    {
        Vector3 targetLocation = target.transform.position;
        Vector3 ownerLocation = owner.transform.position;

        Vector3 targetDir = targetLocation - ownerLocation;

        float relativeHeading = Vector3.Angle(owner.transform.forward, owner.transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(owner.transform.forward, owner.transform.TransformVector(targetDir));

        if (toTarget > TO_TARGET_ANGLE && relativeHeading < RELATIVE_ANGLE || targetSpeed < 0.01f)
        {
            Seek(agent, targetLocation);
            return;
        }

        float lookAhead = targetDir.magnitude / (agent.speed + targetSpeed);
        Seek(agent, targetLocation + target.transform.forward * lookAhead * LOOK_AHEAD_MULTIPLIER);
    }

    // Have the enemy agent intelligently avoid your attempts at cutting them off
    public static void Evade(NavMeshAgent agent, GameObject target, float targetSpeed, GameObject owner)
    {
        Vector3 targetLocation = target.transform.position;
        Vector3 ownerLocation = owner.transform.position;

        Vector3 targetDir = targetLocation - ownerLocation;
        float lookAhead = targetDir.magnitude / (agent.speed + targetSpeed);

        Flee(agent, targetLocation + target.transform.forward * lookAhead, ownerLocation);
    }

    // Have the enemy agent wander around naturally
    public static void Wander(NavMeshAgent agent, GameObject owner, float wanderRadius = 10, float wanderDistance = 10, float wanderJitter = 1)
    {
        ConfineValueInRange(ref wanderRadius, 1, Mathf.Infinity);
        ConfineValueInRange(ref wanderDistance, 1, Mathf.Infinity);
        ConfineValueInRange(ref wanderJitter, 1, Mathf.Infinity);

        Vector3 wanderTarget = Vector3.zero;

        // Creation of point on the circumference of the wanderTarget circle
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                    0,
                                    Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        // Displacement of center of circle
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = owner.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(agent, targetWorld);
    }

    private static void ConfineValueInRange(ref float value, float min, float max)
    {
        if (max < min)
        {
            float temp = min;
            min = max;
            max = temp;
        }

        if (value < min)
            value = min;

        if (value > max)
            value = max;
    }
}
