using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Manages the animation system of a moving entity
 */
public class MovingEntityAnimationManager : AnimationManager
{
    // The parameter representation of the Action State of the animator
    [SerializeField]
    public ParameterRep<int> ActionState;

    // The parameter representation of the IsMoving boolean of the animator
    [SerializeField]
    public ParameterRep<float> MovementSpeed;

    // Updates Action State, IsMoving, and IsDead according to the parameter reps
    public override void Update()
    {
        UpdateAction();
        UpdateMovement();

        base.Update();
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

        m_Reference.SetFloat(MovementSpeed.ParameterName, MovementSpeed.ParameterValue);
    }
}
