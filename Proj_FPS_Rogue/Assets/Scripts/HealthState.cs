using System;
using UnityEngine;

public class HealthState : MonoBehaviour, IReceiveDamage
{
    public Events.OnHealthChangeEvent onHealthChange;
    public Events.OnDamageEvent onDamage;
    
    public int health;
    public int maxHealth;

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
        onHealthChange?.Invoke(health);
        onDamage?.Invoke(damage);
        if (health <= 0)
        {
            health = 0;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    [ContextMenu("Damage")]
    public void Deal20()
    {
        ApplyDamage(20);
    }
}


