using System;
using UnityEngine;

public class GunPart : MonoBehaviour, IGunPart
{
    public GunPartSO partSO;
    
    private Gun _gun;

    public void Initialize(GunPartSO so)
    {
        partSO = so;
    }
    
    public virtual void TryPickUp(Gun gun)
    {
        _gun = gun;

        foreach (var slot in _gun.parts)
        {
            if (slot.Key.partEnum == partSO.partEnum)
            {
                if (slot.Value == null)
                {
                    transform.parent = slot.Key.transform;
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.Euler(Vector3.zero);

                    GetComponent<Collider>().enabled = false;
                    _gun.UpdateDictionaryValue(slot.Key, this);
                    _gun.PartToPickUp = null;
                    //Add Part
                    break;
                }
            }
        }
    }

    public virtual void UpdateGun()
    {
        // Gun Update Logic
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