using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Base Detector
 */
public abstract class EnemyDetector
{
    // Reference to EnemyMovement mainly for agent
    protected EnemyMovement m_EnemyAI;
    // Determines if this detector will make the enemy find or lose a target
    protected bool m_WillFindTarget;

    public EnemyDetector(EnemyMovement enemyAI, bool willFindTarget)
    {
        m_EnemyAI = enemyAI;
        m_WillFindTarget = willFindTarget;
    }

    public abstract void DoDetection();
}
