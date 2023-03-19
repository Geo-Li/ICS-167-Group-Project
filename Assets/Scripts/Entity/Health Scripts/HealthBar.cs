using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// William Min

/*
 * The visuals of a health bar
 */

public class HealthBar : MonoBehaviour
{          
    // Components for health bar display
    [SerializeField]
    private Slider m_Slider;
    [SerializeField]
    private Image m_FillImage;
    [SerializeField] 
    private Gradient gradient;

    // The health class of an entity
    private Health m_Health;

    private float m_CurrentHealthRatio = 0;

    // Updates the health bar
    private void Update()
    {
        SetHealthUI();
    }

    // Updates the health bar
    private void SetHealthUI()
    {
        if (m_Health == null)
            return;

        float newHealthRatio = m_Health.GetHealthRatio();
        if (Mathf.Abs(m_CurrentHealthRatio - newHealthRatio) < 0.001f)
            return;
        else
            m_CurrentHealthRatio = newHealthRatio;

        m_Slider.value = m_CurrentHealthRatio * m_Slider.maxValue;
        m_FillImage.color = gradient.Evaluate(m_Slider.normalizedValue);
    }

    // Sets the health bar with a health object reference
    public void SetHealthReference(Health reference)
    {
        if (reference == null)
            Debug.LogErrorFormat("This health bar is not associated with a component holding a health object.");
        else
            m_Health = reference;
    }
}