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

    // The parameter representation of the Action State of the animator
    [SerializeField]
    public ParameterRep<int> ActionState;

    // The parameter representation of the IsMoving boolean of the animator
    [SerializeField]
    public ParameterRep<float> MovementSpeed;

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

    // Updates the IsDead parameter for entities
    public virtual void LateUpdate()
    {
        UpdateAction();
        UpdateMovement();
        UpdateDeath();
    }

    // Activates an attack animation based on the ActionState parameter
    private void UpdateAction()
    {
        if (m_Reference == null)
            return;

        string name = ActionState.ParameterName;
        if (HasParameterInAnimator(name))
            m_Reference.SetInteger(name, ActionState.ParameterValue);

        ActionState.ParameterValue = 0;
    }

    // Updates the movement animation based on the MovementSpeed parameter
    private void UpdateMovement()
    {
        if (m_Reference == null)
            return;

        string name = MovementSpeed.ParameterName;
        if (HasParameterInAnimator(name))
            m_Reference.SetFloat(name, MovementSpeed.ParameterValue);
    }

    // Activates the death animation if the IsDead parameter declares so
    private void UpdateDeath()
    {
        if (m_Reference == null)
            return;

        if (IsDead.ParameterValue)
        {
            string name = IsDead.ParameterName;
            if (HasParameterInAnimator(name))
                m_Reference.SetTrigger(name);
            
            IsDead.ParameterValue = false;
        }
    }

    private bool HasParameterInAnimator(string parameterName)
    {
        AnimatorControllerParameter[] array = m_Reference.parameters;
        bool result = false;
        int i = 0;

        while(i < array.Length && !result)
        {
            AnimatorControllerParameter acp = array[i];
            if (acp.name.Equals(parameterName))
                result = true;

            i++;
        }

        return result;
    }
}
