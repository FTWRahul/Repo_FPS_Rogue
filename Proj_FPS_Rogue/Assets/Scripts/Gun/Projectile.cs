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

    //For saving last damage into saver
    private HealthState _sender;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Initialize(HealthState sender,float force , Vector3 dir, int damage)
    {
        _sender = sender;
        _force = force;
        _shootDir = dir;
        _damage = damage;
        Launch();
    }

    void Launch()
    {
        Debug.Log("Launching" + _shootDir + _force);
        _rb.AddForce(_shootDir * _force, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision other)
    {

        Debug.Log("Collision");
        if (other.collider.GetComponent<IReceiveDamage>() != null)
        {
            //apply damage will return damage struct with damage data
            _sender.SetLastDamage(other.collider.GetComponent<IReceiveDamage>().ApplyDamage(_damage)); 
        }
        Destroy(this.gameObject);
    }
}
