using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Fleeing Strategy
 */
public class EnemyFlee : EnemyStrategy
{
    public EnemyFlee(EnemyMovement enemyAI, float speed) : base(enemyAI, speed)
    {

    }

    public override void SetCourse()
    {
        m_EnemyAI.Flee(m_EnemyAI.Target.transform.position);
    }
}
