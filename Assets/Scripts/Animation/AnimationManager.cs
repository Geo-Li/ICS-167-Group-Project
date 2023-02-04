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

    // The parameter representation of the Action State of the animator
    [SerializeField]
    public ParameterRep<int> ActionState;

    // The parameter representation of the IsMoving boolean of the animator
    [SerializeField]
    public ParameterRep<bool> IsMoving;

    // The parameter representation of the IsDead trigger of the animator
    [SerializeField]
    public ParameterRep<bool> IsDead;

    // Updates Action State, IsMoving, and IsDead according to the parameter reps
    // Destroys the entity after finishing its death animation
    void Update()
    {
        UpdateAction();
        UpdateMovement();
        UpdateDeath();
    }

    private void UpdateAction()
    {
        if (m_Reference == null)
            return;

        m_Reference.SetInteger(ActionState.ParameterName, ActionState.ParameterValue);

        ActionState.ParameterValue = 0;
    }

    private void UpdateMovement()
    {
        if (m_Reference == null)
            return;

        m_Reference.SetBool(IsMoving.ParameterName, IsMoving.ParameterValue);
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
