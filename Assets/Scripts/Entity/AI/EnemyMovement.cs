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

    // The gameObject owner of the NavMeshAgent
    private GameObject m_Owner;

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

    // Wander variables
    [SerializeField]
    private float m_WanderRadius = 10, m_WanderDistance = 10, m_WanderJitter = 1;

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.updateRotation = false;

        m_Owner = m_Agent.gameObject;
    }

    // Update at the end of every frame
    void LateUpdate()
    {
        UpdatePathRotation();
    }

    // Makes sure that the entity faces toward the direction of movement
    private void UpdatePathRotation()
    {
        if (m_Agent.velocity.sqrMagnitude > Mathf.Epsilon)
            transform.rotation = Quaternion.LookRotation(m_Agent.velocity.normalized);
    }

    // Returns the current speed of m_Agent
    public float GiveCurrentSpeed()
    {
        return m_Agent.velocity.magnitude;
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

    /*
     * Entity Movement Behavior
     */

    // Have the enemy agent seek a certain location
    public void Seek(Vector3 location)
    {
        m_Agent.SetDestination(location);
    }

    // Have the enemy agent flee from a certain location
    public void Flee(Vector3 location)
    {
        Vector3 ownerLocation = m_Owner.transform.position;
        Vector3 fleeVector = location - ownerLocation;
        m_Agent.SetDestination(ownerLocation - fleeVector);
    }

    // Have the enemy agent seek the target
    public void SeekTarget()
    {
        Seek(m_Target.transform.position);
    }

    // Have the enemy agent flee from the target
    public void FleeTarget()
    {
        Flee(m_Target.transform.position);
    }

    // Have the enemy agent intelligently cut you off during a chase
    public void Pursue()
    {
        Vector3 targetLocation = m_Target.transform.position;
        Vector3 targetDir = targetLocation - m_Owner.transform.position;
        float targetSpeed = GetTargetSpeed();

        float relativeHeading = Vector3.Angle(m_Owner.transform.forward, m_Owner.transform.TransformVector(m_Target.transform.forward));
        float toTarget = Vector3.Angle(m_Owner.transform.forward, m_Owner.transform.TransformVector(targetDir));

        if (toTarget > TO_TARGET_ANGLE && relativeHeading < RELATIVE_ANGLE || targetSpeed < 0.01f)
        {
            Seek(targetLocation);
            return;
        }

        float lookAhead = targetDir.magnitude / (m_Agent.speed + targetSpeed);
        Seek(targetLocation + m_Target.transform.forward * lookAhead * LOOK_AHEAD_MULTIPLIER);
    }

    // Have the enemy agent intelligently avoid your attempts at cutting them off
    public void Evade()
    {
        Vector3 targetLocation = m_Target.transform.position;
        float targetSpeed = GetTargetSpeed();

        Vector3 targetDir = targetLocation - m_Owner.transform.position;
        float lookAhead = targetDir.magnitude / (m_Agent.speed + targetSpeed);

        Flee(targetLocation + m_Target.transform.forward * lookAhead);
    }

    // Have the enemy agent wander around naturally
    public void Wander()
    {
        ConfineValueInRange(ref m_WanderRadius, 1, Mathf.Infinity);
        ConfineValueInRange(ref m_WanderDistance, 1, Mathf.Infinity);
        ConfineValueInRange(ref m_WanderJitter, 1, Mathf.Infinity);

        Vector3 wanderTarget = Vector3.zero;

        // Creation of point on the circumference of the wanderTarget circle
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * m_WanderJitter,
                                    0,
                                    Random.Range(-1.0f, 1.0f) * m_WanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= m_WanderRadius;

        // Displacement of center of circle
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, m_WanderDistance);
        Vector3 targetWorld = m_Owner.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    private void ConfineValueInRange(ref float value, float min, float max)
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

    private float GetTargetSpeed()
    {
        float speed = 0f;
        System.Type targetType = m_Target.GetType();

        EnemyMovement component1 = m_Target.GetComponent<EnemyMovement>();
        if (component1 != null)
        {
            speed = component1.m_Agent.speed;
        }

        PlayerMovement component2 = m_Target.GetComponent<PlayerMovement>();
        if (component2 != null)
        {
            speed = component2.GetVelocity();
        }

        return speed;
    }

    /*
     * Entity Detection
     */

    // Detects what entity has dealt damage to the owner
    public GameObject HasBeenHit()
    {
        return null;
    }

    // Detects what entity has the owner hit
    public GameObject HasHitSomething()
    {
        return null;
    }

    // Picks a target object with a certain tag based on closest distance
    public void FindTargetByDistance(string targetTag, float detectionDistance)
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag(targetTag);

        GameObject newTarget = null;
        int index = 0;

        while (newTarget == null && index < list.Length)
        {
            GameObject temp = list[index];

            if (DistanceFromObject(list[index]) < detectionDistance)
                newTarget = temp;

            index++;
        }

        m_Target = newTarget;
    }

    // Returns the distance between the enemy and its target
    public float DistanceFromObject(GameObject target)
    {
        if (target == null)
            return -1;
        else
            return Vector3.Distance(this.transform.position, target.transform.position);
    }
}
