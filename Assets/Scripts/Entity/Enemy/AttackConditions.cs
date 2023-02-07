using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Conditions for an enemy attack to be used
 */

[System.Serializable]
public class AttackConditions
{
    private float m_Timer;

    // Time in seconds before the attack can be called
    [SerializeField]
    private float m_AttackCooldown;

    // Public version of m_AttackCooldown
    public float AttackCooldown
    {
        get 
        {
            return m_AttackCooldown;
        }

        set
        {
            m_AttackCooldown = value;

            if (m_AttackCooldown < 0)
                m_AttackCooldown = 0;
        }
    }

    // Range of distance (min, max) that an attack can be used
    [SerializeField]
    private Vector2 m_AttackDistances;

    // Public version of m_AttackDistances
    public Vector2 AttackDistances
    {
        get
        {
            return m_AttackDistances;
        }
        set
        {
            m_AttackDistances = value;

            if (m_AttackDistances.x < 0)
                m_AttackDistances.x = 0;

            if (m_AttackDistances.y < 0)
                m_AttackDistances.y = 0;
        }
    }

    // Updates cooldown timer
    public void UpdateTimer()
    {
        if (m_Timer > 0)
            m_Timer -= Time.deltaTime;
        else if (m_Timer != 0)
            m_Timer = 0;
    }

    // Determines if attack is not on cooldown
    public bool IsNotOnCooldown()
    {
        return Mathf.Abs(m_Timer) < 0.001f;
    }

    // Determines if attack is in preferred distance
    public bool IsWithinDistance(float distance)
    {
        return m_AttackDistances.x <= distance && m_AttackDistances.y >= distance;
    }

    // Call this when its corresponding attack is called
    public void UseAttack()
    {
        m_Timer = m_AttackCooldown;
    }
}
