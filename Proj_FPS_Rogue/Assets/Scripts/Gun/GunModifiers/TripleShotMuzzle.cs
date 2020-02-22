using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotMuzzle : GunPart
{
    public IShootBehaviour newBehaviour;
    private IShootBehaviour _originalBehaviour;

    private void Awake()
    {
        newBehaviour = GetComponent<IShootBehaviour>();
    }

    public override void UpdateGun()
    {
        base.UpdateGun();
        _originalBehaviour = gun.ShootBehaviour;
        gun.ShootBehaviour = newBehaviour;
        newBehaviour.Init();
        Debug.Log("Triple Shot BBY!");
    }

    public override void RemovePart()
    {
        base.RemovePart();
        gun.ShootBehaviour = _originalBehaviour;
        _originalBehaviour.Init();
    }
}
