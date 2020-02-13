using System;
using UnityEngine;

public class RayAttack : IDistanceAttackBehaviour
{
    public Vector3 GetAttackDirection(Transform transform, Transform target, float projectileSpeed)
    {
        throw new NotImplementedException();
    }

    public Vector3 GetAttackDirection(Transform transform, Transform target)
    {
        return (target.position - transform.position).normalized;
    }
}