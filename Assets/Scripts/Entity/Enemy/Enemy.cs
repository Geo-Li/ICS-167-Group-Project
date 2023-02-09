using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Represents an enemy
 */
public class Enemy : Entity
{
    // Attack Conditions
    [SerializeField]
    private AttackConditions[] m_AttackConditions;

    // The enemy AI movement
    private EnemyMovement m_Movement;

    // Initializes all references
    public override void Start()
    {
        base.Start();

        if (m_EntityManager.GetType() != typeof(MovingEntityAnimationManager))
            Debug.LogErrorFormat("This enemy does not have an enemy animation manager.");

        m_Movement = GetComponent<EnemyMovement>();
    }

    // Checks movement along with base entity checks
    public override void Update()
    {
        if (m_Movement != null)
        {
            UpdateSpeed();

            if (m_AttackConditions.Length > 0)
            {
                AttackConditions ac = m_AttackConditions[0];

                if (ac.IsNotOnCooldown() && ac.IsWithinDistance(m_Movement.DistanceFromTarget()))
                    StartCoroutine(PerformAttack(1));
            }
        }

        base.Update();
    }

    // Update is called once per physics frame
    void FixedUpdate()
    {
        UpdateTimers();
    }

    // Updates all attack condition timers
    private void UpdateTimers()
    {
        int length = m_AttackConditions.Length;

        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                m_AttackConditions[i].UpdateTimer();
            }
        }
    }

    // Updates entity speed for animation manager
    public void UpdateSpeed()
    {
        MovingEntityAnimationManager eam = (MovingEntityAnimationManager)m_EntityManager;
        eam.MovementSpeed.ParameterValue = m_Movement.GiveCurrentSpeed();
    }

    // Make the entity perform an attack if available
    public IEnumerator PerformAttack(int attackNumber)
    {
        MovingEntityAnimationManager eam = (MovingEntityAnimationManager)m_EntityManager;
        eam.ActionState.ParameterValue = attackNumber;

        yield return new WaitForSeconds(0.5f);

        m_AttackConditions[attackNumber - 1].UseAttack();
    }
}
