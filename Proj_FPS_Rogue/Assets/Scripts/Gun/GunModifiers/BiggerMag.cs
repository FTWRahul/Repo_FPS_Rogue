using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerMag : GunPart
{
   public int size;
   private int originalMagSize;
   public override void UpdateGun()
   {
      base.UpdateGun();
      originalMagSize = gun.magSize;
      gun.magSize += size;
      Debug.Log("Mag Updated");
   }

   public override void RemovePart()
   {
      base.RemovePart();
      gun.magSize = originalMagSize;
   }
}
