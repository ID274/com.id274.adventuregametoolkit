using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DamageTest
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase(1)]
    [TestCase(15)]
    [TestCase(25)]
    [TestCase(76)]
    [TestCase(193)]

    public void HealPlayer(int health)
    {
        HealthScript healthScript = new HealthScript();
        healthScript.maxHealth = 100;
        healthScript.startHealth = healthScript.maxHealth - 30;
        healthScript.currentHealth = healthScript.startHealth;
        healthScript.HealHealth(health);

        if (health + healthScript.currentHealth <= healthScript.maxHealth)
        {
            Assert.AreEqual(healthScript.currentHealth, healthScript.startHealth + health); // Only goes through if the health after heal would be equal to maxHealth or lower, then checks if calculation was correct
        }
        Assert.IsTrue(healthScript.currentHealth <= healthScript.maxHealth); // Checks if current health is lower or equal to max health to avoid going above the maximum
    }


}
