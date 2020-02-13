using System;
using UnityEngine;

public class HealthState : MonoBehaviour, IReceiveDamage
{
    #region EVENTS

    public Events.OnHealthChangeEvent onHealthChange;
    public Events.OnLastDamageChangedEvent onLastDamageChanged;
    public Events.OnDamageEvent onDamage;

    #endregion

    #region STATS

    public bool dead;
    public int health;
    public int maxHealth;
    public Damage lastDamage;

    #endregion
   
    
    private void Start()
    {
        SetMaxHealth();
    }

    private void Update()
    {
        if (dead)
        {
            Die();
        }
    }

    private void SetMaxHealth()
    {
        health = maxHealth;
    }
    
    public Damage ApplyDamage(int damage)
    {
        health -= damage;
        
        onDamage?.Invoke(health,damage);
        
        if (health <= 0)
        {
            dead = true;
        }
        return new Damage(dead);
    }

    public void SetLastDamage(Damage damage)
    {
        lastDamage = damage;
        onLastDamageChanged?.Invoke(lastDamage);
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


