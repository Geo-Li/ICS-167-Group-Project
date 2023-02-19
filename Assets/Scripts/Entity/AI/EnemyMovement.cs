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

    // Update every frame
    void Update()
    {
        UpdateTargetPosition();
    }

    // Update at the end of every frame
    void LateUpdate()
    {
        UpdatePathRotation();
    }

    // Updates the agent based on target and bool IsFollowingTarget
    private void UpdateTargetPosition()
    {
        if (m_Target != null)
            SetDestination(m_Target.transform.position);
    }

    // Makes sure that the entity faces toward the direction of movement
    private void UpdatePathRotation()
    {
        if (m_Agent.velocity.sqrMagnitude > Mathf.Epsilon)
            transform.rotation = Quaternion.LookRotation(m_Agent.velocity.normalized);
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

    // Allows the toggle of the activation of the agent
    public void ToggleAgentActivity(bool isActive)
    {
        m_Agent.isStopped = !isActive;
    }

    // If isActive > 0 ==> true
    // If isActive <= 0 ==> false
    private void AnimationUnityTAA(int isActive)
    {
        bool input = isActive > 0;
        ToggleAgentActivity(input);
    }
}
