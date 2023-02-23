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
    private Color m_FullHealthColor = Color.green;
    [SerializeField]
    private Color m_ZeroHealthColor = Color.red;

    // The health class of an entity
    private Health m_Health;

    private void Start()
    {
        StartCoroutine(InitiateHealthBar());
    }

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

        float StartingHealth = m_Health.MaxHealth;
        float CurrentHealth = m_Health.CurrentHealth;
        float healthFraction = CurrentHealth / StartingHealth;

        m_Slider.value = healthFraction * m_Slider.maxValue;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, healthFraction);
    }

    // Intiates the health bar with a health class
    public IEnumerator InitiateHealthBar()
    {
        yield return new WaitForSeconds(0.01f);

        Entity entity = GetComponent<Entity>();

        if (entity == null)
            Debug.LogErrorFormat("This health bar is not associated with an entity.");
        else
            m_Health = entity.Health;
    }
}