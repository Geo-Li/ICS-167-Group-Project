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

    // Attack Conditions
    [SerializeField]
    protected AttackConditions[] m_AttackConditions;

    // The enemy AI movement
    protected EnemyMovement m_Movement;

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

        if (m_EntityManager.GetType() != typeof(MovingEntityAnimationManager))
            Debug.LogErrorFormat("This enemy does not have an enemy animation manager.");

        m_Movement = GetComponent<EnemyMovement>();
    }

    protected override void AnimationUpdater()
    {
        if (m_Movement != null)
        {
            UpdateSpeedAnimation();

            if (m_IsDying)
                m_Movement.ToggleAgentActivity(false);
            else
            {
                for (int i = 0; i < m_AttackConditions.Length; i++)
                {
                    AttackConditions ac = m_AttackConditions[i];

                    if (ac.IsNotOnCooldown() && ac.IsWithinDistance(m_Movement.DistanceFromObject(m_Movement.Target)))
                        StartCoroutine(StartAttack(i + 1));
                }
            }
        }

        base.AnimationUpdater();
    }

    protected override void EntityController()
    {
        GameObject target = m_Movement.Target;

        if (target != null)
            m_Movement.Seek(target.transform.position);
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

        for (int i = 0; i < length; i++)
            m_AttackConditions[i].UpdateTimer();
    }

    // Updates entity speed for animation manager
    public void UpdateSpeedAnimation()
    {
        MovingEntityAnimationManager eam = (MovingEntityAnimationManager)m_EntityManager;
        eam.MovementSpeed.ParameterValue = m_Movement.GiveCurrentSpeed();
    }

    // Make the entity perform an attack if available
    public IEnumerator StartAttack(int attackNumber)
    {
        MovingEntityAnimationManager eam = (MovingEntityAnimationManager)m_EntityManager;
        eam.ActionState.ParameterValue = attackNumber;

        yield return new WaitForSeconds(0.3f);

        m_AttackConditions[attackNumber - 1].UseAttack();
    }
}
