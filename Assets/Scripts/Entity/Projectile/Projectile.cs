using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Represents a projectile object
 */
public class Projectile : MonoBehaviour
{
    private float m_LifeTime;

    // The starting life time of the projectile
    public float LifeTime
    {
        get
        {
            return m_LifeTime;
        }
        set
        {
            m_LifeTime = value;
        }
    }

    private float m_Timer;

    private float m_PushForce;

    // Magnitude of the force that moves the projectile
    public float PushForce
    {
        get
        {
            return m_PushForce;
        }
        set
        {
            m_PushForce = value;
        }
    }

    private Quaternion m_MovingDirection;

    // Vector input and output of the quaternion rotation m_MovingDirection
    public Vector3 MovingDirection
    {
        get
        {
            return m_MovingDirection.eulerAngles;
        }
        set
        {
            m_MovingDirection = Quaternion.Euler(value);
        }
    }

    // The knockback force mulitplier to make m_KForce make a more noticeable force
    private const float FORCE_MULTIPLIER = 300, CONSTANT_FORCE_MULTIPLIER = 10;

    private bool m_HasConstantForce;

    // Determines if the projecile moves with an initial force or with a constant force
    public bool HasConstantForce
    {
        get
        {
            return m_HasConstantForce;
        }
        set
        {
            m_HasConstantForce = value;
        }
    }

    private Rigidbody m_RB;

    private void Start()
    {
        m_Timer = m_LifeTime;

        m_RB = GetComponent<Rigidbody>();
        if (m_RB == null)
            Debug.LogErrorFormat("This projectile does not use a rigidbody.");

        m_MovingDirection = transform.rotation;

        PushProjectile();
    }

    private void Update()
    {
        UpdateLifeTimer();
        MoveProjectile();
        Expire();
    }

    private void UpdateLifeTimer()
    {
        if (m_Timer > 0)
        {
            float nextTime = m_Timer - Time.deltaTime;
            if (nextTime < 0)
                nextTime = 0;

            m_Timer = nextTime;
        }
    }

    // Movement behavior of the projectile
    protected virtual void MoveProjectile()
    {
        if (m_HasConstantForce)
            PushProjectile();

        transform.rotation = m_MovingDirection;
    }

    // Makes the projectile move base on its stats
    public virtual void PushProjectile()
    {
        Vector3 v = transform.forward;

        if (m_HasConstantForce)
        {
            v *= m_PushForce * CONSTANT_FORCE_MULTIPLIER;
            m_RB.velocity = v;
        }
        else
        {
            v *= m_PushForce * FORCE_MULTIPLIER;
            m_RB.AddForce(v);
        }
    }

    private void Expire()
    {
        if (Mathf.Abs(m_Timer) < 0.0001f)
            Destroy(this.gameObject);
    }

    // Returns the life time left in seconds (infinity if set to lasting forever)
    public float GetLifeTimeLeft()
    {
        if (m_Timer < 0)
            return Mathf.Infinity;
        else
            return m_Timer;
    }

    // Change the owner of the projectile
    public void ChangeOwner(GameObject newOwner)
    {
        HitboxDataScript[] hitboxes = GetComponentsInChildren<HitboxDataScript>();

        foreach(HitboxDataScript temp in hitboxes)
            temp.CurrentOwner = newOwner;
    }

    // Returns the owner of the projectile (wantOriginal determines if the client wants the original or current owner)
    public GameObject GetOwner(bool wantOriginal)
    {
        HitboxDataScript hitbox = GetComponentInChildren<HitboxDataScript>();

        GameObject result = hitbox.CurrentOwner;
        if (wantOriginal)
            result = hitbox.GetOriginalOwner();

        return result;
    }
}
