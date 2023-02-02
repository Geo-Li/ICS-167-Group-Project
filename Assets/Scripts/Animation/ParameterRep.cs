using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Represents a variable for changing states of an AnimatorController
 */
[System.Serializable]
public struct ParameterRep<T>
{
    // The value of the parameter
    [SerializeField]
    private T m_ParameterValue;

    // Public version of m_ParameterValue
    public T ParameterValue
    {
        get
        {
            return m_ParameterValue;
        }
        set
        {
            m_ParameterValue = value;
        }
    }

    // The name of the parameter
    [SerializeField]
    private string m_ParameterName;

    // Public version of m_ParameterName
    public string ParameterName
    {
        get
        {
            return m_ParameterName;
        }
        set
        {
            m_ParameterName = value;
        }
    }
}
