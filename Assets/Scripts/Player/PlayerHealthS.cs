using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Geo Li

public class PlayerHealthS : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    [SerializeField] private HealthBarS healthBar;
    [SerializeField] private int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        // for the test purpose, make it initialize with half the max health
        playerHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
    }

    public int GetPlayerMaxHealth() {
        return maxHealth;
    }

    public int GetPlayerHealth() {
        return playerHealth;
    }

    public void IncreasePlayerHealth(int heal) {
        playerHealth += heal;
    }

    public void DecreasePlayerHealth(int damage) {
        playerHealth -= damage;
    }
}
