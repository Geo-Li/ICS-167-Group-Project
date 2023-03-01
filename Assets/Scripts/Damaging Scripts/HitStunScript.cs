using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Adds the ability to gain invulnerability frames after each hit taken (Mostly for the player)
 */
public class HitStunScript : MonoBehaviour
{
    // The hitstun timer
    private float m_HitStunTime;

    // The amount of time (in seconds)
    // that the entity is in stun
    public float HitStunTime
    {
        get
        {
            return m_HitStunTime;
        }
        set
        {
            if (value < 0)
                value = 0;

            m_HitStunTime = value;
        }
    }

    // The rendered sprite of the object
    [SerializeField]
    private GameObject m_Sprite;

    // Checks if the object should become invincible when in hitstun
    [SerializeField]
    private bool m_MakeHitStunInvincible;

    // Sets hitstun time to 0 when instantiated
    void Start()
    {
        m_HitStunTime = 0;
    }

    // Decrements the invulnerability timer and makes
    // the rendered sprite flash to indicate invulnerability frames
    void FixedUpdate()
    {
        FlashInvul();

        if (m_HitStunTime > 0)
            m_HitStunTime -= Time.deltaTime;
        else
            m_HitStunTime = 0;
    }

    // Shows if the object currently has hitstun frames
    public bool HasHitStunFrames()
    {
        return m_HitStunTime > 0;
    }

    // Shows if the object currently invincible due to stun
    public bool IsInvincible()
    {
        return m_MakeHitStunInvincible && HasHitStunFrames();
    }

    // Makes the sprite "flash" when in hitstun
    private void FlashInvul()
    {
        if (m_Sprite == null)
            return;

        bool isActive = true;

        if (!m_Sprite.activeInHierarchy || !HasHitStunFrames())
            isActive = true;
        else
            isActive = false;

        m_Sprite.SetActive(isActive);
    }
}
