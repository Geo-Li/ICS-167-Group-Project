using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min
[CreateAssetMenu]
public class DamageScript : ScriptableObject
{
    [SerializeField]
    private float m_Damage, m_KForce, m_InvulSeconds;

    public float Damage
    {
        get
        {
            return m_Damage;
        }
        set 
        {
            if (value < 0)
                value = 0;

            m_Damage = value;
        }
    }

    public float KnockbackForce
    {
        get
        {
            return m_KForce;
        }
        set
        {
            if (value < 0)
                value = 0;

            m_KForce = value;
        }
    }

    public float InvulnerabilityInSeconds
    {
        get
        {
            return m_InvulSeconds;
        }
        set
        {
            if (value < 0)
                value = 0;

            m_InvulSeconds = value;
        }
    }
}
