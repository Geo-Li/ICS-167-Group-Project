using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// William Min

/*
 * Represents an entity
 */
public class Entity : MonoBehaviour, IPunObservable
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
    protected AnimationManager m_AnimationManager;

    // Attack Conditions
    [SerializeField]
    protected AttackConditions[] m_AttackConditions;

    // List of projectiles that the entity can summon
    [SerializeField]
    private ProjectileSummoner[] m_Projectiles;

    private GameObject m_ProjectileContainer;

    // The entity movement manager
    protected EntityMovement m_MovementManager;

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

    private List<GameObject> m_OffensiveObjects, m_VictimObjects;

    // Maximum number of offensive objects and victim objects that the entity can remember
    [SerializeField]
    private int m_MaxOffensiveObjectCount = 5, m_MaxVictimObjectCount = 5;

    // Initializes all references
    protected virtual void Start()
    {
        m_AnimationManager = GetComponent<AnimationManager>();
        m_MovementManager = GetComponent<EntityMovement>();

        DisplayGameObjectNullErrorMessage(m_AnimationManager);
        DisplayGameObjectNullErrorMessage(HealthGenerator);

        if (m_Health == null)
            m_Health = HealthGenerator.CreateHealthStats();

        HealthBar bar = GetComponent<HealthBar>();
        if (bar != null)
            bar.SetHealthReference(m_Health);

        m_VictimObjects = new List<GameObject>();
        m_OffensiveObjects = new List<GameObject>();
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

    private void FixedUpdate()
    {
        UpdateTimers();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (m_Health != null)
            m_Health.OnPhotonSerializeView(stream, info);
    }

    // Updates all attack condition timers
    private void UpdateTimers()
    {
        int length = m_AttackConditions.Length;

        for (int i = 0; i < length; i++)
            m_AttackConditions[i].UpdateTimer();
    }

    // The animation controller of the entity, making sure all animations are updated
    protected virtual void AnimationUpdater()
    {
        if (m_AnimationManager == null)
            return;

        UpdateSpeedAnimation();

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

    // Updates entity speed for animation manager
    public void UpdateSpeedAnimation()
    {
        m_AnimationManager.MovementSpeed.ParameterValue = m_MovementManager.GetCurrentSpeed();
    }

    // Make the entity perform an attack if available
    public void StartAttack(int attackNumber)
    {
        m_AnimationManager.ActionState.ParameterValue = attackNumber;
    }

    // Summons a projectile on the index in the projectile list towards the targeted position
    public void DoProjectileAttack(int index, Vector3 targetPosition)
    {
        if (index < 0 || index >= m_Projectiles.Length)
        {
            Debug.LogError("The entity is trying to access a projectiles outside of the projectile list's size.");
            return;
        }

        if (m_ProjectileContainer == null)
        {
            m_ProjectileContainer = new GameObject("Projectile Container");
            m_ProjectileContainer.transform.position = new Vector3(0, 0, 0);
        }

        m_Projectiles[index].ProjectileAttack(this.gameObject, targetPosition).transform.parent = m_ProjectileContainer.transform;
    }

    // Summons projectiles from the minIndex to the maxIndex in the projectile list towards the targeted position
    public void DoProjectileAttacks(int minIndex, int maxIndex, Vector3 targetPosition)
    {
        if (minIndex < 0 || maxIndex >= m_Projectiles.Length)
        {
            Debug.LogError("The entity is trying to access a range of projectiles outside of the projectile list's size.");
            return;
        }

        while (minIndex < maxIndex)
        {
            DoProjectileAttack(minIndex, targetPosition);
            minIndex++;
        }
    }

    // Make the entity start dying
    public virtual void StartDyingAnimation()
    {
        if (m_IsDying)
            return;

        m_IsDying = true;

        if (m_AnimationManager != null)
            m_AnimationManager.IsDead.ParameterValue = true;
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

        if (m_ProjectileContainer != null)
            Destroy(m_ProjectileContainer);

        Destroy(this.gameObject);
    }

    // Make the entity take damage by demand
    public void TakeDamage(float damage)
    {
        m_Health.CurrentHealth -= damage;
    }

    // Returns either the list of offensive entities or victim objects based on wantOffesnive
    public List<GameObject> GetEntityList(bool wantOffesnive)
    {
        if (wantOffesnive)
            return m_OffensiveObjects;
        else
            return m_VictimObjects;
    }

    // Adds an entity gameobject to either the list of offensive entities or victim objects based on wantOffesnive
    public void AddEntity(GameObject otherEntity, bool wantOffesnive)
    {
        if (wantOffesnive)
            AddEntityToListWithLimit(m_OffensiveObjects, m_MaxOffensiveObjectCount, otherEntity);
        else
            AddEntityToListWithLimit(m_VictimObjects, m_MaxVictimObjectCount, otherEntity);
    }

    private void AddEntityToListWithLimit(List<GameObject> entityList, int maxCount, GameObject otherEntity)
    {
        entityList.Add(otherEntity);

        if (entityList.Count > maxCount)
            entityList.RemoveAt(0);
    }

    // Returns the list of Attack Conditions
    public AttackConditions[] GetAttackConditions()
    {
        return m_AttackConditions;
    }

    // Toggles an AttackCondition to tell them if their attack animation is currently playing
    // When it toggles off, the cooldown for the condition is applied
    public void ToggleAttackAnimationPlaying(int attackNumber)
    {
        attackNumber--;

        if (attackNumber < 0 || attackNumber >= m_AttackConditions.Length)
        {
            Debug.LogError("The entity is trying to access an attackCondition of the attackCondition list's size.");
            return;
        }

        bool oldBool = m_AttackConditions[attackNumber].IsPlayingAttackAnimation;

        m_AttackConditions[attackNumber].IsPlayingAttackAnimation = !oldBool;

        if (oldBool)
            m_AttackConditions[attackNumber].UseAttack();
    }
}
