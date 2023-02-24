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
        GameObject target = m_Movement.Target;
        NavMeshAgent agent = m_Movement.Agent;

        if (target == null)
        {
            m_Movement.Wander();

            m_Movement.FindTargetByDistance(m_PlayerTag, m_DetectionDistance);

            agent.speed = m_WanderingSpeed;
        }
        else
        {
            m_Movement.Pursue();

            if (m_Movement.DistanceFromObject(target) >= m_DetectionDistance)
                m_Movement.Target = null;

            agent.speed = m_ActiveSpeed;
        }
    }
    protected override void ExpressionMaker()
    {
        bool IsChasing = m_Movement.Target != null;

        m_NeutralFace.SetActive(!IsChasing);
        m_AngryFace.SetActive(IsChasing);
    }
}
