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
        set => currentPart = value;
    }

    public IShootBehaviour ShootBehaviour
    {
        get => _shootBehaviour;
        set => _shootBehaviour = value;
    }

    public Events.OnShootEvent shootEvent;

    public GameObject projectilePrefab;
    public Transform muzzleTransform;
    public float reloadSpeed;
    public float rateOfFire;
    public float projectileForce;
    public int damage;
    public int magSize;

    private bool _isReloading;
    
    private int _shotCount;

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

    public void UpdateDictionaryValue(PartSlot slot ,IGunPart part)
    {
        parts[slot] = part;
        part.UpdateGun();
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
        if (_timeBetweenLastShot > rateOfFire && !_isReloading)
        {
            shootEvent.Invoke();
            _shootBehaviour.Fire();

            _timeBetweenLastShot = 0;

            _shotCount++;

            if (_shotCount >= magSize)
            {
                StartCoroutine(ReloadGun());
            }
        }
    }

    IEnumerator ReloadGun()
    {
        _isReloading = true;
        float i = 0;
        Debug.Log("Reload Started");
        while (i < reloadSpeed)
        {
            i += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            Debug.Log("Reloading");
        }

        _shotCount = 0;
        _isReloading = false;
        Debug.Log("Reload Ended");

    }
    
}