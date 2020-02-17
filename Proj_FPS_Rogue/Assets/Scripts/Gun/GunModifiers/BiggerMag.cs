using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerMag : GunPart
{
   public int size;
   public override void UpdateGun()
   {
      base.UpdateGun();
      gun.magSize += size;
      Debug.Log("Mag Updated");
   }
}
