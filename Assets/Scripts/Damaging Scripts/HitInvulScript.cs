using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Adds the ability to gain invulnerability frames after each hit taken (Mostly for the player)
 */
public class HitInvulScript : MonoBehaviour
{
    private float m_HitInvulTime;

    // The rendered sprite of the object
    [SerializeField]
    private SpriteRenderer Sprite;

    // The amount of time (in seconds)
    // that the entity in invulnerable to attacks
    public float HitInvulnerabilityTime
    {
        get
        {
            return m_HitInvulTime;
        }
        set
        {
            if (value < 0)
                value = 0;

            m_HitInvulTime = value;
        }
    }

    // Sets invulnerability time to 0 when instantiated
    void Start()
    {
        m_HitInvulTime = 0;
    }

    // Decrements the invulnerability timer and makes
    // the rendered sprite flash to indicate invulnerability frames
    void FixedUpdate()
    {
        FlashInvul();

        if (m_HitInvulTime > 0)
            m_HitInvulTime -= Time.deltaTime;
        else
            m_HitInvulTime = 0;
    }

    // Shows if the object currently has invulnerability frames
    public bool HasInvulFrames()
    {
        return m_HitInvulTime > 0;
    }

    private void FlashInvul()
    {
        if (Sprite == null || !HasInvulFrames())
        {
            Sprite.enabled = true;
            return;
        }

        if (Sprite.enabled)
            Sprite.enabled = false;
        else
            Sprite.enabled = true;
    }
}
