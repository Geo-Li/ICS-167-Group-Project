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
}
