using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Geo Li
public class HealthBarS : MonoBehaviour
{
    [SerializeField] private Slider slider;
	[SerializeField] private Gradient gradient;
	[SerializeField] private Image fill;

	// Set health bar's slider's max health 
	// Call when health bar is initialized
	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		// slider.value = health;
		// fill.color = gradient.Evaluate(1f);
	}

	// Set health bar's slider based on player's current health
    public void SetHealth(int health)
	{
		slider.value = health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
