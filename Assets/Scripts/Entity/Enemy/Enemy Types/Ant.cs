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

    protected override void EntityController()
    {
        GameObject target = m_MovementManager.Target;
        NavMeshAgent agent = m_MovementManager.Agent;

        if (target == null)
        {
            m_MovementManager.Wander();

            m_MovementManager.FindTargetByDistance(m_PlayerTag, m_DetectionDistance);

            agent.speed = m_WanderingSpeed;

            m_AttackHitbox.SetActive(false);
        }
        else
        {
            if (IsHoldingWheat())
                m_MovementManager.Evade();
            else
            {
                m_MovementManager.Pursue();
                m_AttackHitbox.SetActive(true);
            }

            if (m_MovementManager.DistanceFromObject(target) >= m_DetectionDistance)
                m_MovementManager.Target = null;

            agent.speed = m_ActiveSpeed;
        }
    }

    // Checks if the ant has a wheat loot table
    public bool IsHoldingWheat()
    {
        return m_Loot != null;
    }

    protected override void ExpressionMaker()
    {
        if (m_IsDying)
            return;

        bool activeAF = false;
        bool activeWF = false;
        bool activeW = false;

        bool isHoldingWheat = IsHoldingWheat();

        if (isHoldingWheat)
            activeW = true;
        
        if (m_MovementManager.Target != null)
        {
            if (isHoldingWheat)
                activeWF = true;
            else
                activeAF = true;
        }

        m_AngryFace.SetActive(activeAF);
        m_WorriedFace.SetActive(activeWF);
        m_Wheat.SetActive(activeW);

        base.ExpressionMaker();
    }
}
