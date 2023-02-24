using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Ant AI and movemment
 */
public class Ant : Enemy
{
    // Sprites for change in animations
    [SerializeField]
    private GameObject m_AngryFace, m_WorriedFace, m_Wheat;

    // Attacking hitbox for pursuing ants
    [SerializeField]
    private GameObject m_AttackHitbox;

    protected override void Start()
    {
        DisplayGameObjectNullErrorMessage(m_AngryFace);
        DisplayGameObjectNullErrorMessage(m_WorriedFace);
        DisplayGameObjectNullErrorMessage(m_Wheat);
        DisplayGameObjectNullErrorMessage(m_AttackHitbox);

        base.Start();
    }

    private void DisplayGameObjectNullErrorMessage(GameObject obj)
    {
        if (obj == null)
            Debug.LogErrorFormat("The " + obj.name + " object is missing.");
    }

    protected override void EntityController()
    {
        GameObject target = m_Movement.Target;
        NavMeshAgent agent = m_Movement.Agent;

        if (target == null)
        {
            m_Movement.Wander();

            m_Movement.FindTargetByDistance(m_PlayerTag, m_DetectionDistance);

            agent.speed = m_WanderingSpeed;

            m_AttackHitbox.SetActive(false);
        }
        else
        {
            if (IsHoldingWheat())
                m_Movement.Evade();
            else
            {
                m_Movement.Pursue();
                m_AttackHitbox.SetActive(true);
            }

            if (m_Movement.DistanceFromObject(target) >= m_DetectionDistance)
                m_Movement.Target = null;

            agent.speed = m_ActiveSpeed;
        }
    }

    // Checks if the ant has a wheat loot table
    public bool IsHoldingWheat()
    {
        return m_Loot != null;
    }

    // Update the sprite of the ant
    void LateUpdate()
    {
        bool activeAF = false;
        bool activeWF = false;
        bool activeW = false;

        bool isHoldingWheat = IsHoldingWheat();

        if (isHoldingWheat)
            activeW = true;
        
        if (m_Movement.Target != null)
        {
            if (isHoldingWheat)
                activeWF = true;
            else
                activeAF = true;
        }

        m_AngryFace.SetActive(activeAF);
        m_WorriedFace.SetActive(activeWF);
        m_Wheat.SetActive(activeW);
    }
}
