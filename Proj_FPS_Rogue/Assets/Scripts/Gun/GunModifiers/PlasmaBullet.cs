using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBullet : GunPart
{
   public PlasmaBulletModifier bulletModifier;
   public float radius = 3f;
   public float rateOfZaps = .5f;
   public int damage = 5;

   private void Awake()
   {
      bulletModifier = GetComponent<PlasmaBulletModifier>();
      PlasmaBulletModifier.radius = radius;
      PlasmaBulletModifier.rateOfZaps = rateOfZaps;
      PlasmaBulletModifier.damage = damage;
   }

   public override void UpdateGun()
   {
      base.UpdateGun();
      gun.bulletModifiers.Add(bulletModifier);
   }

   public override void RemovePart()
   {
      base.RemovePart();
      gun.bulletModifiers.Remove(bulletModifier);
   }
}
