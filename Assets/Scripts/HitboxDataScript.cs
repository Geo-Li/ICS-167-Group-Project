using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// William Min
public class HitboxDataScript : MonoBehaviour
{
    [SerializeField]
    private DamageScript m_DamageUnit;

    private GameObject m_OriginalOwner;

    [SerializeField]
    private GameObject m_CurrentOwner;

    [SerializeField]
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

    void Start()
    {
        if (m_CurrentOwner == null)
            Debug.LogErrorFormat("This hitbox is not assigned to an owner.");

        m_OriginalOwner = m_CurrentOwner;
    }

    void OnTriggerEnter(Collider other)
    {
        float damage = m_DamageUnit.Damage;
        float kForce = m_DamageUnit.KnockbackForce;
        float InvulSeconds = m_DamageUnit.InvulnerabilityInSeconds;

        if (other != m_CurrentOwner && !other.GetComponent<HitboxDataScript>())
        {
            Debug.Log("Punched " + other.gameObject.name + " and applied " +
                   damage + " damage, " +
                   kForce + " knockback units, and " +
                   InvulSeconds + " seconds of invulnerability.");
        }
    }
}
