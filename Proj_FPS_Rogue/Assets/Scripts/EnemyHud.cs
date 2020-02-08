using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHud : MonoBehaviour
{
    public Slider healthBar;
    
    private HealthState _healthState;
    
    private void Start()
    {
        _healthState = GetComponentInParent<HealthState>();
        _healthState.onHealthChange.AddListener(UpdateHealthBar);
    }

    private void UpdateHealthBar()
    {
        healthBar.value = _healthState.health;
    }
}
