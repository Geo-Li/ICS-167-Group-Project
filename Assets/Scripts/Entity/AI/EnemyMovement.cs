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

    // The entity owner of the NavMeshAgent
    private Entity m_Owner;

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

        m_Owner = m_Agent.gameObject.GetComponent<Entity>();
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
        if (!m_Agent.isActiveAndEnabled)
            return;

        Vector3 agentV = m_Agent.velocity;

        if (agentV.sqrMagnitude > Mathf.Epsilon)
            UpdateRotation(agentV.normalized);
    }

    public void UpdateRotation(Vector3 lookingPosition)
    {
        transform.rotation = Quaternion.LookRotation(lookingPosition);
    }

    // Returns the current speed of m_Agent
    public float GetCurrentSpeed()
    {
        if (m_Agent.isActiveAndEnabled)
            return m_Agent.velocity.magnitude;
        else
            return 0;
    }

    // Allows the toggle of the activation of the agent
    public void ToggleAgentActivity(bool isActive)
    {
        if (m_Agent.isActiveAndEnabled)
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
        if (m_Agent.isActiveAndEnabled)
            m_Agent.SetDestination(location);
    }

    // Have the enemy agent flee from a certain location
    public void Flee(Vector3 location)
    {
        Vector3 ownerLocation = m_Owner.transform.position;
        Vector3 fleeVector = location - ownerLocation;
        Seek(ownerLocation - fleeVector);
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
        if (!m_Agent.isActiveAndEnabled)
            return;

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
        if (!m_Agent.isActiveAndEnabled)
            return;

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

        EntityMovement component = m_Target.GetComponent<EntityMovement>();
        if (component != null)
            speed = component.GetCurrentSpeed();

        return speed;
    }

    /*
     * Entity Detection
     */

    // Detects what entity has dealt damage to the owner
    public GameObject GetLatestOffensiveEntity()
    {
        return GetLatestEntity(true);
    }

    // Detects what entity has the owner hit
    public GameObject GetLatestVictimEntity()
    {
        return GetLatestEntity(false);
    }

    private GameObject GetLatestEntity(bool wantOffensive)
    {
        List<GameObject> list = m_Owner.GetEntityList(wantOffensive);

        int count = list.Count;

        if (count >= 1)
            return list[count - 1];
        else
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
