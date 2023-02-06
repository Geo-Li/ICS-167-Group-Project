using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// William Min

/*
 * Provides functionality to collision objects to make them attacking hitboxes
 */
public class HitboxDataScript : MonoBehaviour
{
    // The damage unit that holds the stats of the hitbox
    [SerializeField]
    private DamageScript m_DamageUnit;

    // Records who originally made the hitbox
    private GameObject m_OriginalOwner;

    // Records who currently holds the hitbox
    [SerializeField]
    private GameObject m_CurrentOwner;

    // Public version of m_CurrentOwner
    public GameObject CurrentOwner
    {
        get
        {
            return m_CurrentOwner;
        }
        set
        {
            m_CurrentOwner = value;
        }
    }

    // Sets the original owner to this object when instantiated
    void Start()
    {
        if (m_CurrentOwner == null)
            Debug.LogErrorFormat("This hitbox is not assigned to an owner.");

        m_OriginalOwner = m_CurrentOwner;
    }

    // Applies effects listed in the damage unit to the gameObject of other
    void OnTriggerEnter(Collider other)
    {
        if (other != m_CurrentOwner 
            && !other.GetComponent<HitboxDataScript>() 
            && m_CurrentOwner.layer != other.gameObject.layer)
        {
            bool canBeHit = true;
            HitInvulScript hitInvul = other.GetComponent<HitInvulScript>();

            if (hitInvul != null && hitInvul.HasInvulFrames())
                canBeHit = false;

            if (canBeHit)
            {
                float damage = m_DamageUnit.Damage;
                float kForce = m_DamageUnit.KnockbackForce;
                float InvulSeconds = m_DamageUnit.InvulnerabilityInSeconds;
                
                Vector3 vector = other.transform.position - transform.position;

                m_DamageUnit.ApplyDamage(other);
                m_DamageUnit.ApplyKnockback(other, vector);
                m_DamageUnit.ApplyInvulFrames(other);

                /*
                Debug.Log("Punched " + other.gameObject.name + " and applied " +
                       damage + " damage, " +
                       kForce + " knockback units, and " +
                       InvulSeconds + " seconds of invulnerability.");
                */
            }
        }
    }
}
