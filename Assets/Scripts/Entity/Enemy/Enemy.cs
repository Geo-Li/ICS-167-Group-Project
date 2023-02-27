using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Represents an enemy
 */
public class Enemy : Entity
{
    private const float LOOK_AHEAD_MULTIPLIER = 5, TO_TARGET_ANGLE = 90, RELATIVE_ANGLE = 20;

    // The enemyy movement manager
    protected new EnemyMovement m_MovementManager;

    // Detection range for players to target
    [SerializeField]
    protected float m_DetectionDistance;

    // speeds for wandering and being active
    [SerializeField]
    protected float m_WanderingSpeed, m_ActiveSpeed;

    // The name of the player tag to find players
    [SerializeField]
    protected string m_PlayerTag;

    // Initializes all references
    protected override void Start()
    {
        base.Start();

        m_MovementManager = GetComponent<EnemyMovement>();

        if (m_MovementManager == null)
            Debug.LogErrorFormat("This enemy does not have an enemy movement manager.");

        base.m_MovementManager = m_MovementManager;
    }

    protected override void AnimationUpdater()
    {
        if (m_AnimationManager == null)
            return;

        if (m_IsDying)
            m_MovementManager.ToggleAgentActivity(false);
        else
        {
            int i = 0;
            int max = m_AttackConditions.Length;
            bool hasMadeMove = false;

            while (!hasMadeMove && i < max)
            {
                AttackConditions ac = m_AttackConditions[i];

                if (ac.IsNotOnCooldown() && ac.IsWithinDistance(m_MovementManager.DistanceFromObject(m_MovementManager.Target)))
                {
                    StartCoroutine(StartAttack(i + 1));
                    hasMadeMove = true;
                }

                i++;
            }
        }

        base.AnimationUpdater();
    }

    protected override void EntityController()
    {
        GameObject target = m_MovementManager.Target;

        if (target != null)
            m_MovementManager.Seek(target.transform.position);
    }

    public void DoProjectileAttack(int index)
    {
        GameObject target = m_MovementManager.Target;

        if (target != null)
            base.DoProjectileAttack(index, target.transform.position);
    }
}
