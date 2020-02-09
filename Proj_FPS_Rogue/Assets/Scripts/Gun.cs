using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private IShootBehaviour _shootBehaviour;

    public Events.OnShootEvent shootEvent;

    public GameObject projectilePrefab;
    public Transform muzzleTransform;
    public float reloadTime;
    public float rateOfFire;
    public float projectileForce;
    public int damage;

    private float _timeBetweenLastShot;

    private void Awake()
    {
        _shootBehaviour = GetComponent<IShootBehaviour>();
    }

    private void Update()
    {
        _timeBetweenLastShot += Time.deltaTime;
    }

    public void Shoot()
    {
        if (_timeBetweenLastShot > rateOfFire)
        {
            shootEvent.Invoke();
            _shootBehaviour.Fire();

            _timeBetweenLastShot = 0;
        }
    }
}


