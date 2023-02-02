using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min
public class AnimationManager : MonoBehaviour
{
    // Reference to an entity's animator
    [SerializeField]
    protected Animator m_Reference;

    // Reference to Mesh Sprite
    [SerializeField]
    private GameObject sprite;

    [SerializeField]
    private ParameterRep<int> m_ActionState;

    [SerializeField]
    private ParameterRep<bool> m_IsMoving;

    [SerializeField]
    private ParameterRep<bool> m_IsDead;

    //Updates ResponseState to the animation's state
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
