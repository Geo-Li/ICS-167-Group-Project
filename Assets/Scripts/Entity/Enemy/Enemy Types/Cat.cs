using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Cat AI and movemment
 */
public class Cat : Enemy
{
    // Sprites for change in animations
    [SerializeField]
    private GameObject m_NeutralFace, m_AngryFace;

    protected override void Start()
    {
        DisplayGameObjectNullErrorMessage(m_NeutralFace);
        DisplayGameObjectNullErrorMessage(m_AngryFace);

        base.Start();
    }

    protected override void EntityController()
    {
        base.EntityController();

        if (m_MovementManager.Target == null)
        {
            SetEnemyStrategy(new EnemyWander(m_MovementManager, m_WanderingSpeed));
            SetEnemyDetector(new EnemyDistanceDetector(m_MovementManager, true, m_PlayerTag, m_DetectionDistance));
        }
        else
        {
            SetEnemyStrategy(new EnemyPursue(m_MovementManager, m_ActiveSpeed));
            SetEnemyDetector(new EnemyDistanceDetector(m_MovementManager, false, m_PlayerTag, m_DetectionDistance));
        }
    }
    protected override void ExpressionMaker()
    {
        bool IsChasing = m_MovementManager.Target != null;

        m_NeutralFace.SetActive(!IsChasing);
        m_AngryFace.SetActive(IsChasing);
    }
}
