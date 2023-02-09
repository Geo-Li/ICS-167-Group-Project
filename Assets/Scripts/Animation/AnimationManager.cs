using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Manages the general customly-made animation system of an entity
 */
public class AnimationManager : MonoBehaviour
{
    // Reference to an entity's animator
    protected Animator m_Reference;

    // The parameter representation of the IsDead trigger of the animator
    [SerializeField]
    public ParameterRep<bool> IsDead;

    // Checks if this manager has an animator to work with
    void Start()
    {
        m_Reference = GetComponent<Animator>();

        if (m_Reference == null)
            Debug.LogErrorFormat("This manager is not with an animator.");
    }

    public virtual void Update()
    {
        UpdateDeath();
    }

    private void UpdateDeath()
    {
        if (m_Reference == null)
            return;

        if (IsDead.ParameterValue)
        {
            m_Reference.SetTrigger(IsDead.ParameterName);
            IsDead.ParameterValue = false;
        }
    }
}
