using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Basic Enemy AI movement
 */
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour, EntityMovement
{
    private const float LOOK_AHEAD_MULTIPLIER = 5, TO_TARGET_ANGLE = 90, RELATIVE_ANGLE = 20;

    // The agent we'll be moving around
    private NavMeshAgent m_Agent;

    // Public version of m_Agent
    public NavMeshAgent Agent
    {
        get
        {
            return m_Agent;
        }
        set
        {
            
        }
    }

    // The target the enemy will move towards
    [SerializeField]
    private GameObject m_Target;

    // Public version of m_Target
    public GameObject Target
    {
        get
        {
            return m_Target;
        }
        set 
        {
            m_Target = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.updateRotation = false;
    }

    // Update at the end of every frame
    void LateUpdate()
    {
        UpdateEntityRotation();
        RespondToKnockback();
    }

    // Makes the enemy respond to knockback by temporarily disabling the agent during hit stun
    private void RespondToKnockback()
    {
        HitStunScript hit = GetComponent<HitStunScript>();
        bool isInHitStun = hit.HasHitStunFrames();

        if (m_Agent.enabled == isInHitStun)
            m_Agent.enabled = !isInHitStun;
    }

    // Makes sure that the entity faces toward the direction of movement
    private void UpdateEntityRotation()
    {
        if (!AgentIsActive())
            return;

        Vector3 agentV = m_Agent.velocity;

        if (agentV.sqrMagnitude > Mathf.Epsilon)
            UpdateRotation(agentV.normalized);
        else if (m_Target != null)
            UpdateRotation(m_Target.transform.position - transform.position);
    }

    public void UpdateRotation(Vector3 lookingPosition)
    {
        transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(lookingPosition).eulerAngles.y, 0f);
    }

    // Returns the current speed of m_Agent
    public float GetCurrentSpeed()
    {
        if (AgentIsActive())
            return m_Agent.velocity.magnitude;
        else
            return 0;
    }

    // Allows the toggle of the activation of the agent
    public void ToggleAgentActivity(bool isActive)
    {
        if (AgentIsActive())
            m_Agent.isStopped = !isActive;
    }

    // If isActive > 0 ==> true
    // If isActive <= 0 ==> false
    private void AnimationUnityTAA(int isActive)
    {
        bool input = isActive > 0;
        ToggleAgentActivity(input);
    }

    // Gets the closest point in the NavMesh within distance
    public static Vector3 GetClosestPointOnNavMesh(Vector3 position, float distance)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, distance, NavMesh.AllAreas))
            return hit.position;
        else
            return position;
    }

    // Have the enemy agent seek a certain location
    public void Seek(Vector3 location)
    {
        if (AgentIsActive())
            m_Agent.SetDestination(location);
    }

    // Have the enemy agent flee from a certain location
    public void Flee(Vector3 location)
    {
        Vector3 ownerLocation = transform.position;
        Vector3 fleeVector = location - ownerLocation;
        Seek(ownerLocation - fleeVector);
    }

    // Returns the speed of the target
    public float GetTargetSpeed()
    {
        float speed = 0f;

        EntityMovement component = m_Target.GetComponent<EntityMovement>();
        if (component != null)
            speed = component.GetCurrentSpeed();

        return speed;
    }

    // Returns the distance between the enemy and its target
    public float DistanceFromObject(GameObject target)
    {
        if (target == null)
            return -1;
        else
            return Vector3.Distance(transform.position, target.transform.position);
    }

    // Returns if the agent of the enemy movement is active or not
    public bool AgentIsActive()
    {
        return m_Agent != null && m_Agent.isActiveAndEnabled;
    }
}
