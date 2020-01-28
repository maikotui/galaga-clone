using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Tooltip("The maximum amount of health")]
    public int maxHealth = 100;
    [Tooltip("The amount of health the gameobject initializes with")]
    public int startingHealth = 100;

    public bool isDead;

    public delegate void HandleDeath(int actualDamageTaken, GameObject source);
    public event HandleDeath OnDeath;

    private int m_currentHealth;

    void Start()
    {
        m_currentHealth = startingHealth;
        isDead = m_currentHealth == 0;
    }

    public void TakeHealth(int healthAmount, GameObject source)
    {
        if (!isDead)
        {
            m_currentHealth = Mathf.Clamp(m_currentHealth + healthAmount, 0, maxHealth);
        }
    }

    public void TakeDamage(int damageAmount, GameObject source)
    {
        if (!isDead)
        {
            int healthBeforeDamage = m_currentHealth;
            m_currentHealth = Mathf.Clamp(m_currentHealth - damageAmount, 0, maxHealth);
            if (m_currentHealth == 0)
            {
                OnDeath(healthBeforeDamage - m_currentHealth, source);
                isDead = true;
            }
        }
    }
}
