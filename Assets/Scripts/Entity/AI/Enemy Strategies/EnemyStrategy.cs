using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Base Strategy
 */
public abstract class EnemyStrategy
{
    // Reference to EnemyMovement mainly for agent
    protected EnemyMovement m_EnemyAI;

    // Sets the reference to the EnemyMovement and the speed for its agent
    public EnemyStrategy(EnemyMovement enemyAI, float speed)
    {
        m_EnemyAI = enemyAI;
        m_EnemyAI.Agent.speed = speed;
    }

    public abstract void SetCourse();
}
