using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// health class that enable game object to have health
public class HealthS
{
    private int maxHealth;
    private int currHealth;

    public void SetMaxHealth(int health) {
        if (maxHealth == null) {
            maxHealth = health;
        }
    }

    public void SetCurrHealth(int health) {
        if (currHealth == null) {
            currHealth = health;
        }
    }

    public int GetCurrHealth() {
        return currHealth;
    }

    public void IncreaseCurrHealth(int heal) {
        currHealth += heal;
    }

    public bool DecreaseCurrHealth(int damage) {
        currHealth -= damage;
        if (currHealth <= 0) {
            return true;
        } else {
            return false;
        }
    }

}
