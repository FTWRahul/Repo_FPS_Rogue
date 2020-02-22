using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All other guns can subclass from this master gun class
public class Gun : MonoBehaviour
{
    //The shoot event things subscribe to
    public Events.OnShootEvent shootEvent;
    
    //Prefab of bullet
    public GameObject projectilePrefab;
    
    //Parameters for gun and bullet
    public Transform muzzleTransform;
    public float reloadSpeed;
    public float rateOfFire;
    public float projectileForce;
    public int damage;
    public int magSize;

    
    //Private member variables
    private bool _isReloading;
    private int _shotCount;
    private float _timeBetweenLastShot;
    
    //Shooting Behaviour  
    private IShootBehaviour _shootBehaviour;

    //Current part that the player is in radius of
    public IGunPart PartToPickUp { get; set; }

    //Property for Shoot Behaviour
    public IShootBehaviour ShootBehaviour
    {
        get => _shootBehaviour;
        set
        {
            Debug.Log("Shoot Behaviour changed");
            _shootBehaviour = value;
        }
    }
    
    //Data base of modular parts currently being applied to player
    public List<IBulletModifier> bulletModifiers = new List<IBulletModifier>();
    public Dictionary<PartSlot, IGunPart> parts = new Dictionary<PartSlot, IGunPart>();
    [SerializeField]
    private List<PartSlot> partSlot;

    private void Awake()
    {
        _shootBehaviour = GetComponentInChildren<IShootBehaviour>();
        InitializeDictionary();
    }

    //Called by part interfaces to inform gun of changes
    public void UpdateDictionaryValue(PartSlot slot ,IGunPart part)
    {
        parts[slot] = part;
        part.UpdateGun();
    }

    //Adds the given transform parts as null at the start
    private void InitializeDictionary()
    {
        foreach (var slot in partSlot)
        {
            parts.Add(slot, null);
        }
    }

    //Called by a part to see if it can be picked up
    public void TryEquipingPart()
    {
        PartToPickUp?.TryPickUp(this);
    }
    
    private void Update()
    {
        //increasing time between last shot
        _timeBetweenLastShot += Time.deltaTime;
    }

    //called by action controller on the correct input
    public void Shoot()
    {
        if (_timeBetweenLastShot > rateOfFire && !_isReloading)
        {
            shootEvent.Invoke();
            
            _shootBehaviour.Fire();

            _timeBetweenLastShot = 0;

            _shotCount++;

            //When bullets in the mag are over Reload
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