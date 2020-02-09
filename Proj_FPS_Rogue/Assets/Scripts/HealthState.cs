﻿using System;
using UnityEngine;

public class HealthState : MonoBehaviour, IReceiveDamage
{
    public Events.OnHealthChangeEvent onHealthChange;
    
    public float health;
    public float maxHealth;

    private void Start()
    {
        SetMaxHealth();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void SetMaxHealth()
    {
        health = maxHealth;
    }
    
    public void ApplyDamage(int damage)
    {
        if (health <= 0)
            return;

        health -= damage;
        onHealthChange?.Invoke();
        if (health <= 0)
        {
            health = 0;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

public interface IReceiveDamage
{
    void ApplyDamage(int damage);
}
