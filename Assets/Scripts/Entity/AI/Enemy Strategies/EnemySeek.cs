using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Seeking Strategy
 */
public class EnemySeek : EnemyStrategy
{
    public EnemySeek(EnemyMovement enemyAI, float speed) : base(enemyAI, speed)
    {

    }

    public override void SetCourse()
    {
        m_EnemyAI.Seek(m_EnemyAI.Target.transform.position);
    }
}
