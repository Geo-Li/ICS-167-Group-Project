using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private const float FORCE_MULTIPLIER = 100;

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
    public void ApplyDamage(Collider other, Entity owner)
    {
        Entity otherEntity = other.GetComponent<Entity>();

        if (otherEntity == null)
            return;

        PlayerHealthS playerHealth = other.GetComponent<PlayerHealthS>();

        if (playerHealth != null)
        {
            playerHealth.DecreasePlayerHealth((int)m_Damage);
        }
        else
        {
            Health health = otherEntity.Health;

            health.CurrentHealth -= m_Damage;
        }

        otherEntity.AddEntity(owner.gameObject, true);
        owner.AddEntity(otherEntity.gameObject, false);
    }

    // Applies the gameObject of other with the knockback force by a certain angle
    public void ApplyKnockback(Collider other, Vector3 angle)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb == null)
            return;

        angle.y = 0;

        Vector3 kVector = m_KForce * angle.normalized * FORCE_MULTIPLIER;
        kVector.y = 0f;

        rb.AddForce(kVector);
    }

    // Applies the gameObject of other with the invulnerability time
    public void ApplyInvulFrames(Collider other)
    {
        HitStunScript hitInvul = other.GetComponent<HitStunScript>();

        if (hitInvul == null)
            return;

        hitInvul.HitStunTime = m_InvulSeconds;
    }

    // Displays the damage stats
    public void DisplayDamageStats()
    {
        Debug.Log("This damage set deals " +
                       m_Damage + " damage, " +
                       m_KForce + " knockback units, and " +
                       m_InvulSeconds + " seconds of invulnerability.");
    }
}
