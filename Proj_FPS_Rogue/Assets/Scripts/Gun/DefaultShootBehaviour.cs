using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultShootBehaviour : MonoBehaviour , IShootBehaviour
{
    private Gun _gun;
    
    //just for now here sorry
    private HealthState _playerHealthState;

    private void Awake()
    {
        _gun = GetComponent<Gun>();
        _playerHealthState = GetComponentInParent<HealthState>();
    }

    public void Fire()
    {
        Projectile go = Instantiate(_gun.projectilePrefab, _gun.muzzleTransform.position, Quaternion.identity).GetComponent<Projectile>();
        go.Initialize(_playerHealthState,_gun.projectileForce, _gun.transform.forward, _gun.damage);
    }
}
