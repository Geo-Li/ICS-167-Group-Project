using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Evading Strategy (Smart Fleeing with tracking)
 */
public class EnemyEvade : EnemyStrategy
{
    public EnemyEvade(EnemyMovement enemyAI, float speed) : base(enemyAI, speed)
    {

    }

    public override void SetCourse()
    {
        Transform target = m_EnemyAI.Target.transform;

        Vector3 targetLocation = target.position;
        float targetSpeed = m_EnemyAI.GetTargetSpeed();

        Vector3 targetDir = targetLocation - m_EnemyAI.transform.position;
        float lookAhead = targetDir.magnitude / (m_EnemyAI.Agent.speed + targetSpeed);

        m_EnemyAI.Flee(targetLocation + target.forward * lookAhead);
    }
}
