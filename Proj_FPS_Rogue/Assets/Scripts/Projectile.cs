using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;

    private float _force;
    private Vector3 _shootDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Initialize(float force , Vector3 dir)
    {
        _force = force;
        _shootDir = dir;
        Launch();
    }

    void Launch()
    {
        _rb.AddForce(_shootDir * _force, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision other)
    {
        Destroy(this);
    }
}
