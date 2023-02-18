using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthS : MonoBehaviour
{
    public int playerHealth;
    public HealthBarS healthBar;
    public int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        // for the test purpose, make it initialize with half the max health
        playerHealth = maxHealth/2;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth/2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
