using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Represents an entity (Props, Enemies)
 */
public class Entity : MonoBehaviour
{
    // Health of an entity
    [SerializeField]
    private HealthStats HealthGenerator;

    private Health m_Health;

    // Public version of m_HealthBar
    public Health Health
    {
        get
        {
            return m_Health;
        }
        set
        {
            m_Health = value;
        }
    }



    // The manager that allows switching between animations
    private AnimationManager m_EntityManager;

    // Attack Conditions
    [SerializeField]
    private AttackConditions[] m_AttackConditions;



    [SerializeField]
    private EnemyMovement m_Movement;



    // Determines if the entity is ready to be destroyed
    [SerializeField]
    private bool m_Dead = false;

    // Public version of m_Dead;
    public bool IsDead
    {
        get 
        {
            return m_Dead;
        }
        set
        {
            m_Dead = value;
        }
    }

    private bool m_IsDying;



    // Loot Table that the entity use to drop collectibles
    [SerializeField]
    private LootTable m_Loot;

    // Makes sure that the game object has an animation manager
    void Start()
    {
        m_Health = HealthGenerator.CreateHealthStats();

        m_EntityManager = this.GetComponent<AnimationManager>();

        if (m_EntityManager == null)
            Debug.LogErrorFormat("An Entity component is not on an Entity.");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Movement != null)
        {
            UpdateSpeed();

            if (m_AttackConditions.Length > 0)
            {
                AttackConditions ac = m_AttackConditions[0];

                if (ac.IsNotOnCooldown() && ac.IsWithinDistance(m_Movement.DistanceFromTarget()))
                {
                    Debug.Log("IsAttacking");
                    StartCoroutine(PerformAttack(1));
                }
            }
        }

        if (m_Health != null && m_Health.IsDying())
            StartDyingAnimation();

        if (m_Dead)
            DestroyEntity();
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
        m_EntityManager.MovementSpeed.ParameterValue = m_Movement.GiveCurrentSpeed();
    }

    // Make the entity perform an attack if available
    public IEnumerator PerformAttack(int attackNumber)
    {
        m_EntityManager.ActionState.ParameterValue = attackNumber;

        yield return new WaitForSeconds(0.5f);

        m_AttackConditions[attackNumber - 1].UseAttack();
    }

    // Make the entity start dying
    public void StartDyingAnimation()
    {
        if (m_IsDying)
            return;

        m_IsDying = true;

        if (m_EntityManager != null)
            m_EntityManager.IsDead.ParameterValue = true;
        else
            m_Dead = true;
    }

    private void DestroyEntity()
    {
        if (m_Loot != null)
        {
            m_Loot.DropPosition = transform.position;
            m_Loot.GenerateLootDrops();
        }

        Destroy(this.gameObject);
    }
}
