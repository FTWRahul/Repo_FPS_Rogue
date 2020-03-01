using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBulletModifier : MonoBehaviour , IBulletModifier
{
    public static float radius;
    public static float rateOfZaps;
    public static int damage;

    private float timeBetweenZaps = 10f;
    
    public void OnAttach(GameObject behaviour)
    {
        Debug.Log("Here BOOP");
        PlasmaBulletModifier mod = behaviour.AddComponent<PlasmaBulletModifier>();
        SphereCollider col = mod.gameObject.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = radius;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (timeBetweenZaps >= rateOfZaps)
            {
                HealthState enemyHealth =  other.GetComponent<HealthState>();
                Zap(enemyHealth);
            }
        }
    }

    public void Zap(HealthState enemyHealth)
    {
        enemyHealth.ApplyDamage(damage, MathHelper.DamageAngle(transform, enemyHealth.transform));
        timeBetweenZaps = 0f;
    }

    private void Update()
    {
        timeBetweenZaps += Time.deltaTime;
    }
}
