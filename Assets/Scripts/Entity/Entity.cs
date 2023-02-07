using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Starting stats of the health of an entity
    [SerializeField]
    protected HealthStats HealthGenerator;

    // Actual health instance of the entity
    protected Health m_Health;

    // Public version of m_Health
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
    protected AnimationManager m_EntityManager;

    // Determines if the entity is ready to be destroyed
    [SerializeField]
    protected bool m_Dead = false;

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

    // Determines if the entity is currently playing its dying animation
    protected bool m_IsDying;

    // Loot Table that the entity use to drop collectibles
    [SerializeField]
    protected LootTable m_Loot;

    // Initializes all references
    public virtual void Start()
    {
        m_Health = HealthGenerator.CreateHealthStats();

        m_EntityManager = GetComponent<AnimationManager>();

        if (m_EntityManager == null)
            Debug.LogErrorFormat("An Entity component is not on an Entity.");
    }

    // Checks if the entity is ready to die and be destroyed from the scene
    public virtual void Update()
    {
        if (m_Health != null && m_Health.IsDying())
            StartDyingAnimation();

        if (m_Dead)
            DestroyEntity();
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
