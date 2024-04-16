using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int startHealth;

    void Start()
    {
        maxHealth = 100;
        startHealth = maxHealth;
        currentHealth = startHealth;
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth - damage < 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }
    }
    public void HealHealth(int health)
    {
        if (currentHealth + health > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
        }
    }
}
