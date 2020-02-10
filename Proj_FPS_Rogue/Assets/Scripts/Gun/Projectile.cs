using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;

    private float _force;
    private Vector3 _shootDir;
    private int _damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Initialize(float force , Vector3 dir, int damage)
    {
        _force = force;
        _shootDir = dir;
        _damage = damage;
        Launch();
    }

    void Launch()
    {
        _rb.AddForce(_shootDir * _force, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision other)
    {

        if (other.collider.GetComponent<IReceiveDamage>() != null)
        {
            other.collider.GetComponent<IReceiveDamage>().ApplyDamage(_damage);
        }
        Destroy(this.gameObject);
    }
}
