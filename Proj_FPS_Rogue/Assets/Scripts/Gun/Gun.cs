using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private IShootBehaviour _shootBehaviour;

    private IGunPart currentPart;

    public IGunPart PartToPickUp
    {
        get => currentPart;
        set
        {
            currentPart = value;
            
        }
    }

    public IShootBehaviour ShootBehaviour
    {
        get => _shootBehaviour;
        set => _shootBehaviour = value;
    }

    public Events.OnShootEvent shootEvent;

    public GameObject projectilePrefab;
    public Transform muzzleTransform;
    public float reloadTime;
    public float rateOfFire;
    public float projectileForce;
    public int damage;

    private float _timeBetweenLastShot;

    [SerializeReference]
    public List<IBulletModifier> bulletModifiers;

    public Dictionary<PartSlot, IGunPart> parts = new Dictionary<PartSlot, IGunPart>();

    [SerializeField]
    private List<PartSlot> partSlot;

    private void Awake()
    {
        _shootBehaviour = GetComponent<IShootBehaviour>();
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        foreach (var slot in partSlot)
        {
            parts.Add(slot, null);
        }
    }

    public void TryEquipingPart()
    {
        currentPart?.TryPickUp(this);
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