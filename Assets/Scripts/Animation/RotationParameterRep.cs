using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Represents a rotational float variable for changing states of an AnimatorController
 */
[System.Serializable]
public struct RotationParameterRep
{
    //private ParameterRep<float> m_ParameterValue;

    private static float Rotation_Min = 0f, Rotation_Max = 360f;

    [Range(0,360)]
    // The value of the parameter
    [SerializeField]
    private float m_ParameterValue;

    // Public version of m_ParameterValue for rotation float
    public float ParameterValue
    {
        get
        {
            return m_ParameterValue;
        }
        set
        {
            float rawRotation = value;
            float sign = rawRotation / Mathf.Abs(rawRotation);

            float newRotation = 0f;
            if (rawRotation > Rotation_Max)
                newRotation = rawRotation % Rotation_Max;

            if (sign < 0)
                newRotation = Rotation_Max - newRotation;

            m_ParameterValue = newRotation;
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

    /*
    [SerializeField]
    // Public version of m_ParameterValue for rotation float
    public float ParameterValue
    {
        get
        {
            return m_ParameterValue.ParameterValue;
        }
        set
        {
            float rawRotation = value;
            float sign = rawRotation / Mathf.Abs(rawRotation);

            float newRotation = 0f;
            if (rawRotation > Rotation_Max)
                newRotation = rawRotation % Rotation_Max;

            if (sign < 0)
                newRotation = Rotation_Max - newRotation;

            m_ParameterValue.ParameterValue = newRotation;
        }
    }

    [SerializeField]
    // Public version of m_ParameterName
    public string ParameterName
    {
        get
        {
            return m_ParameterValue.ParameterName;
        }
        set
        {
            m_ParameterValue.ParameterName = value;
        }
    }
    */
}
