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
        GameObject target = m_Movement.Target;
        NavMeshAgent agent = m_Movement.Agent;

        if (target == null)
        {
            m_Movement.Wander();

            GameObject obj = m_Movement.GetLatestOffensiveEntity();

            if (obj != null && (target == null || target != obj))
                m_Movement.Target = obj;

            agent.speed = m_WanderingSpeed;
        }
        else
        {
            m_Movement.SeekTarget();

            agent.speed = m_ActiveSpeed;
        }
    }
}
