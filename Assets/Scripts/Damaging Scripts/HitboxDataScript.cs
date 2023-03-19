using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

                m_DamageUnit.ApplyDamage(other.GetComponent<Entity>(), owner.GetComponent<Entity>());
                m_DamageUnit.ApplyKnockback(other.GetComponent<Rigidbody>(), vector);
                m_DamageUnit.ApplyInvulFrames(other.GetComponent<HitStunScript>());

                /*
                PhotonView view = m_CurrentOwner.GetComponent<PhotonView>();
                if (view.IsMine)
                    view.RPC("DamageOther", RpcTarget.All, other.GetInstanceID(), owner.GetInstanceID(), vector, m_DamageUnit.GetInstanceID());
                */
            }
        }
    }

    /*
    [PunRPC]
    private void DamageOther(int otherID, int ownerID, Vector3 vector, int damageUnitID)
    {
        GameObject other = (GameObject)FindObjectFromInstanceID(otherID);
        GameObject owner = (GameObject)FindObjectFromInstanceID(ownerID);
        DamageScript damageUnit = ((GameObject)FindObjectFromInstanceID(damageUnitID)).GetComponent<DamageScript>();

        damageUnit.ApplyDamage(other.GetComponent<Entity>(), owner.GetComponent<Entity>());
        damageUnit.ApplyKnockback(other.GetComponent<Rigidbody>(), vector);
        damageUnit.ApplyInvulFrames(other.GetComponent<HitStunScript>());
    }

    public static UnityEngine.Object FindObjectFromInstanceID(int iid)
    {
        return (UnityEngine.Object)typeof(UnityEngine.Object)
                .GetMethod("FindObjectFromInstanceID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, new object[] { iid });
    }
    */

    // Returns the original owner of the hitbox
    public GameObject GetOriginalOwner()
    {
        return m_OriginalOwner;
    }
}
