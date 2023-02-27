using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Cat AI and movemment
 */
public class Cat : Enemy
{
    // Sprites for change in animations
    [SerializeField]
    private GameObject m_NeutralFace, m_AngryFace;

    protected override void Start()
    {
        DisplayGameObjectNullErrorMessage(m_NeutralFace);
        DisplayGameObjectNullErrorMessage(m_AngryFace);

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
        }
        else
        {
            m_MovementManager.Pursue();

            if (m_MovementManager.DistanceFromObject(target) >= m_DetectionDistance)
                m_MovementManager.Target = null;

            agent.speed = m_ActiveSpeed;
        }
    }
    protected override void ExpressionMaker()
    {
        bool IsChasing = m_MovementManager.Target != null;

        m_NeutralFace.SetActive(!IsChasing);
        m_AngryFace.SetActive(IsChasing);
    }
}
