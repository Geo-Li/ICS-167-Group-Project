using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthS : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    [SerializeField] private HealthBarS healthBar;
    [SerializeField] private int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        // for the test purpose, make it initialize with half the max health
        playerHealth = maxHealth/2;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth/2);
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
