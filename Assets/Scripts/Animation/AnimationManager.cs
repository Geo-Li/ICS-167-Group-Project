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
    [SerializeField]
    protected Animator m_Reference;

    // Reference to the sprite of the entity that is being animated
    [SerializeField]
    private GameObject sprite;

    // The parameter representation of the Action State of the animator
    [SerializeField]
    private ParameterRep<int> m_ActionState;

    // The parameter representation of the IsMoving boolean of the animator
    [SerializeField]
    private ParameterRep<bool> m_IsMoving;

    // The parameter representation of the IsDead trigger of the animator
    [SerializeField]
    private ParameterRep<bool> m_IsDead;

    // Updates Action State, IsMoving, and IsDead according to the parameter reps
    // Destroys the entity after finishing its death animation
    void Update()
    {
        UpdateAction();
        UpdateMovement();
        UpdateDeath();

        if (sprite != null && !sprite.activeInHierarchy)
            Destroy(m_Reference.gameObject);
    }

    private void UpdateAction()
    {
        if (m_Reference == null)
            return;

        m_Reference.SetInteger(m_ActionState.ParameterName, m_ActionState.ParameterValue);
    }

    private void UpdateMovement()
    {
        if (m_Reference == null)
            return;

        m_Reference.SetBool(m_IsMoving.ParameterName, m_IsMoving.ParameterValue);
    }

    private void UpdateDeath()
    {
        if (m_Reference == null)
            return;

        if (m_IsDead.ParameterValue)
        {
            m_Reference.SetTrigger(m_IsDead.ParameterName);
            m_IsDead.ParameterValue = false;
        }
    }
}
