using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Represents the stats of damaging hitboxes
 * - Damage
 * - Knockback Force
 * - Invulnerability time in seconds
 */
[CreateAssetMenu]
public class DamageScript : ScriptableObject
{
    // The values of damage, knockback force,
    // and invulnerability time in seconds represented by floats
    [SerializeField]
    private float m_Damage, m_KForce, m_InvulSeconds;

    // The knockback force mulitplier to make m_KForce make a more noticeable force
    private static float m_ForceMultiplier = 100;

    // Public version of the damage float value
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

    // Public version of the knockback force float value
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

    // Public version of the invulnerability time float value
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

    // Applies the gameObject of other with the damage
    public void ApplyDamage(Collider other)
    {
        Entity otherEntity = other.GetComponent<Entity>();

        if (otherEntity == null)
            return;

        Health health = otherEntity.Health;

        health.CurrentHealth -= m_Damage;
    }

    // Applies the gameObject of other with the knockback force by a certain angle
    public void ApplyKnockback(Collider other, Vector3 angle)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb == null)
            return;

        angle.y = 0;

        Vector3 kVector = m_KForce * angle.normalized * m_ForceMultiplier;
        rb.AddForce(kVector);
    }

    // Applies the gameObject of other with the invulnerability time
    public void ApplyInvulFrames(Collider other)
    {
        HitInvulScript hitInvul = other.GetComponent<HitInvulScript>();

        if (hitInvul == null)
            return;

        hitInvul.HitInvulnerabilityTime = m_InvulSeconds;
    }
}
