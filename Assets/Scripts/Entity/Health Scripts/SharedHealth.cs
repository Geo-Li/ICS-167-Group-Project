using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedHealth : MonoBehaviour
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

    [SerializeField]
    private List<Entity> m_SharingEntities = new List<Entity>();

    // Start is called before the first frame update
    void Start()
    {
        if (m_Health == null)
            m_Health = HealthGenerator.CreateHealthStats();

        HealthBar bar = GetComponent<HealthBar>();
        if (bar != null)
            bar.SetHealthReference(m_Health);

        if (m_SharingEntities.Count > 0)
            foreach (Entity e in m_SharingEntities)
                UpdateHealthforEntity(e);
    }

    // Adds an entity to this list of shared entities
    public void AddSharingEntity(Entity newEntity)
    {
        if (m_SharingEntities.Contains(newEntity))
            return;

        m_SharingEntities.Add(newEntity);
        UpdateHealthforEntity(newEntity);
    }

    private void UpdateHealthforEntity(Entity entity)
    {
        entity.Health = m_Health;
    }

    // Removes an entity from this list of shared entities
    public void RemoveSharingEntity(Entity leavingEntity)
    {
        m_SharingEntities.Remove(leavingEntity);
    }
}
