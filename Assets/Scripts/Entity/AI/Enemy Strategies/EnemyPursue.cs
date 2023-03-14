using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Pursuing Strategy (Smart Seeking with tracking)
 */
public class EnemyPursue : EnemyStrategy
{
    private const float LOOK_AHEAD_MULTIPLIER = 5, TO_TARGET_ANGLE = 90, RELATIVE_ANGLE = 20;

    public EnemyPursue(EnemyMovement enemyAI, float speed) : base(enemyAI, speed)
    {

    }

    public override void SetCourse()
    {
        Transform target = m_EnemyAI.Target.transform;
        Transform owner = m_EnemyAI.transform;

        Vector3 targetLocation = target.position;
        Vector3 targetDir = targetLocation - owner.position;
        float targetSpeed = m_EnemyAI.GetTargetSpeed();

        float relativeHeading = Vector3.Angle(owner.forward, owner.TransformVector(target.forward));
        float toTarget = Vector3.Angle(owner.forward, owner.TransformVector(targetDir));

        if (toTarget > TO_TARGET_ANGLE && relativeHeading < RELATIVE_ANGLE || targetSpeed < 0.01f)
        {
            m_EnemyAI.Seek(targetLocation);
            return;
        }

        float lookAhead = targetDir.magnitude / (m_EnemyAI.Agent.speed + targetSpeed);
        m_EnemyAI.Seek(targetLocation + target.forward * lookAhead * LOOK_AHEAD_MULTIPLIER);
    }
}
