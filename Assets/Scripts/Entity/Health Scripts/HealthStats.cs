using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// William Min

/*
 * Generate health objects with its generated stats
 */
[CreateAssetMenu]
public class HealthStats : ScriptableObject
{
    // Maximum & Current health of entity
    [SerializeField]
    private float m_MaxHealth = 3f, m_CurrentHealth = 0f;

    [SerializeField]
    private bool m_WillGenereateWithFullHealth = false;

    // Creates a class instance of Health for individual entities
    public Health CreateHealthStats()
    {
        if (m_WillGenereateWithFullHealth)
            return new Health(m_MaxHealth);
        else
            return new Health(m_MaxHealth, m_CurrentHealth);
    }
}
