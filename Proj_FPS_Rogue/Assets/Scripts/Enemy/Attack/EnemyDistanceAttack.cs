using UnityEngine;

namespace Enemy.Attack
{
    public class EnemyDistanceAttack
    {
        private protected GameObject projectilePrefab;
        private protected Transform muzzlePosition;
        private protected float projectileSpeed;
        private protected int attackDamage;
        private protected Vector3 shootingDirectionNormalized;

        private protected IDistanceAttackBehaviour distanceAttackBehaviour;
        
        //shouldn't be here - temporary
        private protected HealthState healthState;
        private protected Transform target;
    }
}