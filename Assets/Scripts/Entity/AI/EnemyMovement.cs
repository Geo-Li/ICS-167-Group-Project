using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Basic Enemy AI movement
 */
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    // The agent we'll be moving around
    private NavMeshAgent m_Agent;

    // The target the enemy will move towards
    [SerializeField]
    private GameObject m_Target;

    // Determines if the enemy will follow the target
    [SerializeField]
    private bool m_IsFollowingTarget;

    // Public version of m_IsFollowingTarget
    public bool IsFollowingTarget
    {
        get
        {
            return m_IsFollowingTarget;
        }
        set
        {
            m_IsFollowingTarget = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.updateRotation = false;
        m_IsFollowingTarget = true;
    }

    // When the usser clicks move the agent.
    void Update()
    {
        if (m_Target != null)
            SetDestination(m_Target.transform.position);

        if (m_IsFollowingTarget != m_Agent.isStopped)
            m_Agent.isStopped = m_IsFollowingTarget;
    }

    void LateUpdate()
    {
        if (m_Agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(m_Agent.velocity.normalized);
        }
    }

    // Sets the destination coordinates for the enemy
    public void SetDestination(Vector3 destination)
    {
        m_Agent.destination = destination;
    }

    // Returns the current speed of m_Agent
    public float GiveCurrentSpeed()
    {
        return m_Agent.velocity.magnitude;
    }

    // Returns the distance between the enemy and its target
    public float DistanceFromTarget()
    {
        if (m_Target == null)
            return -1;
        else
            return Vector3.Distance(this.transform.position, m_Target.transform.position);
    }
}
