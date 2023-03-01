using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Cow AI and movemment
 */
public class Cow : Enemy
{
    protected override void EntityController()
    {
        GameObject target = m_MovementManager.Target;
        NavMeshAgent agent = m_MovementManager.Agent;

        if (target == null)
        {
            m_MovementManager.Wander();

            GameObject obj = m_MovementManager.GetLatestOffensiveEntity();

            if (obj != null && (target == null || target != obj))
                m_MovementManager.Target = obj;

            agent.speed = m_WanderingSpeed;
        }
        else
        {
            m_MovementManager.SeekTarget();

            agent.speed = m_ActiveSpeed;
        }
    }
}
