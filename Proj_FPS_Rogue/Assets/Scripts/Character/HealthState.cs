using System;
using NaughtyAttributes;
using UnityEngine;

public class HealthState : MonoBehaviour, IReceiveDamage
{
    #region EVENTS

    [HideInInspector] public Events.OnHealthChangeEvent onHealthChange;
    [HideInInspector] public Events.OnLastDamageChangedEvent onLastDamageChanged;
    [HideInInspector] public Events.OnDamageEvent onDamage;
    [HideInInspector] public Events.OnDeathEvent onDeath;

    #endregion

    #region STATS
    
    public int maxHealth;
    
    #endregion

    #region DEBUG

    [BoxGroup("DEBUG")][ReadOnly] public bool dead;
    [BoxGroup("DEBUG")][ReadOnly] public int health;
    [BoxGroup("DEBUG")][ReadOnly] public Damage lastDamage;
    
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
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}


