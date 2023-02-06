using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{          
    // Components for health bar display
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;

    // Entity that holds the health script
    private Entity entity;

    private Health m_Health;

    // Makes sure that this component is associated with an entity
    private void Start()
    {
        entity = GetComponent<Entity>();

        if (entity == null)
            Debug.LogErrorFormat("This health bar is not associated with an entity.");
    }

    // Updates the health bar
    void Update()
    {
        SetHealthUI();
    }

    // Updates the health bar
    private void SetHealthUI()
    {
        if (m_Health == null)
            m_Health = entity.Health;

        float StartingHealth = m_Health.MaxHealth;
        float CurrentHealth = m_Health.CurrentHealth;
        float healthFraction = CurrentHealth / StartingHealth;

        m_Slider.value = healthFraction * m_Slider.maxValue;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, healthFraction);
    }
}