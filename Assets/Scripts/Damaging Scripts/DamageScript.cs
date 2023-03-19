


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
    private const float FORCE_MULTIPLIER = 100;

    private const byte HEALTH_CHANGE_EVENT = 0;

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
    public void ApplyDamage(Entity other, Entity owner)
    {
        if (other == null || owner == null)
            return;

        other.Health.CurrentHealth -= m_Damage;

        other.AddEntity(owner.gameObject, true);
        owner.AddEntity(other.gameObject, false);
    }

    // Applies the gameObject of other with the knockback force by a certain angle
    public void ApplyKnockback(Rigidbody otherRB, Vector3 angle)
    {
        if (otherRB == null)
            return;

        otherRB.velocity = Vector3.zero;
        angle.y = 0;

        Vector3 kVector = m_KForce * angle.normalized * FORCE_MULTIPLIER;
        kVector.y = 0f;

        otherRB.AddForce(kVector);
    }

    // Applies the gameObject of other with the invulnerability time
    public void ApplyInvulFrames(HitStunScript otherHitStun)
    {
        if (otherHitStun == null)
            return;

        otherHitStun.HitStunTime = m_InvulSeconds;
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
