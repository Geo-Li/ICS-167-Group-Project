using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Represents an entity
 */
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

    // Checks if the cat has a set face expression (Mainly for animation)
    [SerializeField]
    private bool m_HasSetFace;

    // Loot Table that the entity use to drop collectibles
    [SerializeField]
    protected LootTable m_Loot;

    // Initializes all references
    protected virtual void Start()
    {
        m_EntityManager = GetComponent<AnimationManager>();

        DisplayGameObjectNullErrorMessage(m_EntityManager);
        DisplayGameObjectNullErrorMessage(HealthGenerator);

        m_Health = HealthGenerator.CreateHealthStats();
    }

    // Displays when the object being checked is missing
    public void DisplayGameObjectNullErrorMessage(Object obj)
    {
        if (obj == null)
            Debug.LogErrorFormat("The " + obj.name + " object is missing.");
    }

    private void Update()
    {
        AnimationUpdater();
        EntityController();
    }

    private void LateUpdate()
    {
        if (!m_HasSetFace)
            ExpressionMaker();
    }

    // The animation controller of the entity, making sure all animations are updated
    protected virtual void AnimationUpdater()
    {
        if (m_Health != null && m_Health.IsDying())
            StartDyingAnimation();

        if (m_Dead)
            DestroyEntity();
    }

    // The controller of the entity, guided by either AI or player control
    protected virtual void EntityController()
    {
        
    }

    // The expression maker (facial) of the entities
    protected virtual void ExpressionMaker()
    {

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

    // Make the entity take damage by demand
    public void TakeDamage(float damage)
    {
        m_Health.CurrentHealth -= damage;
    }
}
