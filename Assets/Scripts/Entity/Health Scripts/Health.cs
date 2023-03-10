using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Represents the health of an entity
 */
public class Health
{
    // Maximum health of entity
    private float m_MaxHealth = 3f;

    // Public version of m_MaxHealth
    public float MaxHealth
    {
        get
        {
            return m_MaxHealth;
        }
        set
        {
            m_MaxHealth = value;

            if (m_MaxHealth < 0)
                m_MaxHealth = 0;

            if (m_MaxHealth < m_CurrentHealth)
                m_CurrentHealth = m_MaxHealth;
        }
    }

    // Current health of entity
    private float m_CurrentHealth;

    // Public version of m_CurrentHealth
    public float CurrentHealth
    {
        get
        {
            return m_CurrentHealth;
        }
        set
        {
            m_CurrentHealth = value;

            if (m_CurrentHealth < 0)
                m_CurrentHealth = 0;

            if (m_MaxHealth < m_CurrentHealth)
                m_CurrentHealth = m_MaxHealth;
        }
    }

    // Constructors of Health
    public Health(float maxHealth, float currentHealth)
    {
        m_MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }

    public Health(float maxHealth) : this(maxHealth, maxHealth)
    {

    }

    // Returns if there is no health left and the object is dying
    public bool IsDying()
    {
        return m_CurrentHealth <= 0f;
    }

    // Returns if the object is at full health
    public bool IsAtFullHealth()
    {
        return m_CurrentHealth >= m_MaxHealth;
    }

    // Returns the ratio of the health stats as a decimal float from 0f to 1f
    public float GetHealthRatio()
    {
        return m_CurrentHealth / m_MaxHealth;
    }
}
