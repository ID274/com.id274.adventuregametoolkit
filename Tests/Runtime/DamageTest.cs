using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthTest
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase(1)]
    [TestCase(15)]
    [TestCase(25)]
    [TestCase(76)]
    [TestCase(193)]
    
    public void DamagePlayer(int damage)
    {
        HealthScript health = new HealthScript();
        health.maxHealth = 100;
        health.currentHealth = health.maxHealth;
        health.TakeDamage(damage);

        if (damage <= health.currentHealth)
        {
            Assert.AreEqual(health.currentHealth, health.maxHealth - damage); // Only goes through if the damage is lower or equal to 0
        }
        Assert.IsTrue(health.currentHealth >= 0); // Checks if health isn't below 0
    }
}
