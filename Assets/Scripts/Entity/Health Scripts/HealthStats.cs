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
    // Maximum health of entity
    [SerializeField]
    private float m_MaxHealth = 3f;

    public Health CreateHealthStats()
    {
        return new Health(m_MaxHealth);
    }
}
