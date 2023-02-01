using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min
public class AnimationManager : MonoBehaviour
{
    // Reference to an entity's animator
    [SerializeField]
    protected Animator m_Reference;

    [SerializeField]
    private ParameterRep<int> m_ActionState;

    [SerializeField]
    private ParameterRep<bool> m_IsMoving;

    //Updates ResponseState to the animation's state
    void Update()
    {
        UpdateAction();
        UpdateMovement();
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
}
