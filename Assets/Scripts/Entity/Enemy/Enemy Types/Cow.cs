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
        base.EntityController();

        if (m_MovementManager.Target == null)
        {
            SetEnemyStrategy(new EnemyWander(m_MovementManager, m_WanderingSpeed));
            SetEnemyDetector(new EnemyTargetHit(m_MovementManager, true, this, true));
        }
        else
        {
            SetEnemyStrategy(new EnemySeek(m_MovementManager, m_ActiveSpeed));
            SetEnemyDetector(new EnemyDistanceDetector(m_MovementManager, false, m_PlayerTag, m_DetectionDistance));
        }
    }
}
