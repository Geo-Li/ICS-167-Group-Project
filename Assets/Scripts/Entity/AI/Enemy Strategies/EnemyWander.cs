using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Wandering Strategy
 */
public class EnemyWander : EnemyStrategy
{
    private float m_Timer;

    private const float MIN_TIME_FOR_WANDER = 2, MAX_TIME_FOR_WANDER = 4, WANDER_RANGE = 25;

    public EnemyWander(EnemyMovement enemyAI, float speed) : base(enemyAI, speed)
    {

    }

    public override void SetCourse()
    {
        if (m_Timer > 0)
        {
            m_Timer -= Time.deltaTime;
            return;
        }

        Vector3 randomPoint = m_EnemyAI.transform.position + Random.insideUnitSphere * WANDER_RANGE;
        randomPoint = EnemyMovement.GetClosestPointOnNavMesh(randomPoint, WANDER_RANGE);

        m_EnemyAI.Seek(randomPoint);
        m_Timer = Random.Range(MIN_TIME_FOR_WANDER, MAX_TIME_FOR_WANDER);
    }
}
