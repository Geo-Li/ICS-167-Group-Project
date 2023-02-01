using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min
public class HitInvulScript : MonoBehaviour
{
    private float m_HitInvulTime;

    [SerializeField]
    private MeshRenderer Sprite;

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

    void Start()
    {
        m_HitInvulTime = 0;
    }

    void FixedUpdate()
    {
        FlashInvul();

        if (m_HitInvulTime > 0)
            m_HitInvulTime -= Time.deltaTime;
        else
            m_HitInvulTime = 0;
    }

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
