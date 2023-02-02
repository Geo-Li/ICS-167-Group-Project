using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min
[CreateAssetMenu]
public class DamageScript : ScriptableObject
{
    [SerializeField]
    private float m_Damage, m_KForce, m_InvulSeconds;

    private static float m_ForceMultiplier = 100;

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

    public void ApplyKnockback(Collider other, Vector3 angle)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb == null)
            return;

        angle.y = 0;

        Vector3 kVector = m_KForce * angle.normalized * m_ForceMultiplier;
        rb.AddForce(kVector);
    }

    public void ApplyInvulFrames(Collider other)
    {
        HitInvulScript hitInvul = other.GetComponent<HitInvulScript>();

        if (hitInvul == null)
            return;

        hitInvul.HitInvulnerabilityTime = m_InvulSeconds;
    }
}
