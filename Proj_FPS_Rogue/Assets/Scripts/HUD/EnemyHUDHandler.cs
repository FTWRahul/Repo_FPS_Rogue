using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHUDHandler : MonoBehaviour
{
    public GameObject floatingDamage;
    public GameObject healthBar;
    
    public int distanceToShow;

    private Transform _target;

    private HealthState _enemiesInRange;


    private void Update()
    {
        throw new NotImplementedException();
    }
}
