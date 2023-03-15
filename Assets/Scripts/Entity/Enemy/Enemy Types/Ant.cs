using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Ant AI and movemment
 */
public class Ant : Enemy
{
    // Sprites for change in animations
    [SerializeField]
    private GameObject m_AngryFace, m_WorriedFace, m_Wheat;

    // Attacking hitbox for pursuing ants
    [SerializeField]
    private GameObject m_AttackHitbox;

    protected override void Start()
    {
        DisplayGameObjectNullErrorMessage(m_AngryFace);
        DisplayGameObjectNullErrorMessage(m_WorriedFace);
        DisplayGameObjectNullErrorMessage(m_Wheat);

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
            if (IsHoldingWheat())
                SetEnemyStrategy(new EnemyEvade(m_MovementManager, m_ActiveSpeed));
            else
            {
                SetEnemyStrategy(new EnemyPursue(m_MovementManager, m_ActiveSpeed));
                if (m_AttackHitbox != null)
                    m_AttackHitbox.SetActive(true);
            }
            SetEnemyDetector(new EnemyDistanceDetector(m_MovementManager, false, m_PlayerTag, m_DetectionDistance));
        }
    }

    // Checks if the ant has a wheat loot table
    public bool IsHoldingWheat()
    {
        return m_Loot != null;
    }

    protected override void ExpressionMaker()
    {
        if (m_IsDying)
            return;

        bool activeAF = false;
        bool activeWF = false;
        bool activeW = false;

        bool isHoldingWheat = IsHoldingWheat();

        if (isHoldingWheat)
            activeW = true;
        
        if (m_MovementManager.Target != null)
        {
            if (isHoldingWheat)
                activeWF = true;
            else
                activeAF = true;
        }

        m_AngryFace.SetActive(activeAF);
        m_WorriedFace.SetActive(activeWF);
        m_Wheat.SetActive(activeW);

        base.ExpressionMaker();
    }
}
