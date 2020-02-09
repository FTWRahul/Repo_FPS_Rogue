using System;
using UnityEngine;

public class GunPart : MonoBehaviour, IGunPart
{
    [SerializeField]
    private GunPartEnum partEnum;
    
    private Gun _gun;
    
    public virtual void TryPickUp(Gun gun)
    {
        _gun = gun;

        foreach (var slot in _gun.parts)
        {
            if (slot.Key.partEnum == partEnum)
            {
                if (slot.Value == null)
                {
                    transform.parent = slot.Key.transform;
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.Euler(Vector3.zero);
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
        return partEnum;
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
        _gun.PartToPickUp = null;
    }
}