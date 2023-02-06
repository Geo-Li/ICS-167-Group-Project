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
        if (m_Health != null && m_Health.IsDying() && !m_IsDying)
        {
            m_IsDying = true;

            if (m_EntityManager != null)
                m_EntityManager.IsDead.ParameterValue = true;
            else
                m_Dead = true;
        }

        if (m_Dead)
            DestroyEntity();
    }

    // Make the entity perform an attack if available
    public void performAttack(int attackNumber)
    {
        m_EntityManager.ActionState.ParameterValue = attackNumber;
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
