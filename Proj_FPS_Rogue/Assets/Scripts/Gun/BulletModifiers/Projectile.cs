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
    public LayerMask _layerToDamage;
    private List<IBulletModifier> _bulletModifiers = new List<IBulletModifier>();
    
    //For saving last damage into saver
    private HealthState _sender;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Initialize(HealthState sender,float force , Vector3 dir, int damage, List<IBulletModifier> mods, LayerMask layerMask)
    {
        _sender = sender;
        _force = force;
        _shootDir = dir;
        _damage = damage;
        _bulletModifiers = mods;
        _layerToDamage = layerMask;
        UpdateBullet();
        Launch();
    }

    void Launch()
    {
        _rb.AddForce(_shootDir * _force, ForceMode.Impulse);
    }

    public void UpdateBullet()
    {
        Debug.Log("Starting Update for mods");

        for (int i = 0; i < _bulletModifiers.Count; i++)
        {
            _bulletModifiers[i].OnAttach(this.gameObject);
            Debug.Log("Times updated " + i);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<IReceiveDamage>() != null && other.collider.gameObject.layer == _layerToDamage)
        {
            //apply damage will return damage struct with damage data
            _sender.SetLastDamage(other.collider.GetComponent<IReceiveDamage>().ApplyDamage(_damage)); 
        }
        Destroy(this.gameObject);
    }
}
