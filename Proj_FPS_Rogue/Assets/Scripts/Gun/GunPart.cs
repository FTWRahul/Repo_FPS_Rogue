﻿using System;
using UnityEngine;

public class GunPart : MonoBehaviour, IGunPart
{
    public GunPartSO partSO;
    
    protected Gun gun;

    public void Initialize(GunPartSO so)
    {
        partSO = so;
    }
    
    public virtual void TryPickUp(Gun inGun)
    {
        this.gun = inGun;

        foreach (var slot in this.gun.parts)
        {
            if (slot.Key.partEnum == partSO.partEnum)
            {
                if (slot.Value == null)
                {
                    transform.parent = slot.Key.transform;
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.Euler(Vector3.zero);

                    GetComponent<Collider>().enabled = false;
                    this.gun.UpdateDictionaryValue(slot.Key, this);
                    this.gun.PartToPickUp = null;
                    //Add Part
                    //UpdateGun();
                    break;
                }
            }
        }
    }

    public virtual void UpdateGun()
    {
        Debug.Log("Base call: Gun being Updated by child");
    }

    public GunPartEnum Part()
    {
        return partSO.partEnum;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponentInChildren<Gun>().PartToPickUp == null)
            {
                other.GetComponentInChildren<Gun>().PartToPickUp = this;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<Gun>().PartToPickUp = null;
        }
        
    }
}