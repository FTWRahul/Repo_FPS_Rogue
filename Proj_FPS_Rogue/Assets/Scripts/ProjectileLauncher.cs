using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is temporary until normal pooling system
public class ProjectileLauncher : MonoBehaviour
{
    public static ProjectileLauncher Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject LaunchProjectile(HealthState sender,GameObject prefab, Transform from, Vector3 to, float speed, int damage)
    {
        Debug.Log("BOOP" + speed);
        Projectile go = Instantiate(prefab, from.position, from.rotation).GetComponent<Projectile>();
        go.Initialize(sender, speed, to, damage, new List<IBulletModifier>(), LayerMask.NameToLayer("Player")); // PLEASE REMOVE THIS LIST
        return go.gameObject;
    }
}
