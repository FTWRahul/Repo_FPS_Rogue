using System;
using UnityEngine;

public class HealthState : MonoBehaviour, IReceiveDamage
{
    public Events.OnHealthChangeEvent onHealthChange;
    public Events.OnDamageEvent onDamage;
    
    public bool dead;
    public int health;
    public int maxHealth;

    private CharacterData _characterData;

    private void Start()
    {
        SetMaxHealth();
        _characterData = FindObjectOfType<CharacterData>();
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
    
    public void ApplyDamage(int damage)
    {
        if (health <= 0)
            return;

        health -= damage;
        
        onHealthChange?.Invoke(health);
        onDamage?.Invoke(damage);
        
        if (health <= 0)
        {
            dead = true;
        }
        _characterData.SetLastDamage(new Shoot(this.gameObject, dead));
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


