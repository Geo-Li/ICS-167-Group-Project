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
        m_OriginalOwner = m_CurrentOwner;
    }

    // Applies effects listed in the damage unit to the gameObject of other
    void OnTriggerEnter(Collider other)
    {
        if (m_CurrentOwner == null || other != m_CurrentOwner 
            && !other.GetComponent<HitboxDataScript>() 
            && m_CurrentOwner.tag != other.gameObject.tag)
        {
            bool canBeHit = true;
            HitStunScript hitInvul = other.GetComponent<HitStunScript>();

            if (hitInvul != null && hitInvul.IsInvincible())
                canBeHit = false;

            if (canBeHit)
            {   
                Vector3 vector = other.transform.position - transform.position;

                Entity owner = null;
                if (m_CurrentOwner != null)
                    owner = m_CurrentOwner.GetComponent<Entity>();

                m_DamageUnit.ApplyDamage(other, owner);
                m_DamageUnit.ApplyKnockback(other, vector);
                m_DamageUnit.ApplyInvulFrames(other);
            }
        }
    }

    // Returns the original owner of the hitbox
    public GameObject GetOriginalOwner()
    {
        return m_OriginalOwner;
    }
}
