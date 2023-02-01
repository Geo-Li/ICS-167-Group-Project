using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min
[System.Serializable]
public struct ParameterRep<T>
{
    // Value of parameter
    [SerializeField]
    private T m_ParameterValue;

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

    // Name of parameter
    [SerializeField]
    private string m_ParameterName;

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
