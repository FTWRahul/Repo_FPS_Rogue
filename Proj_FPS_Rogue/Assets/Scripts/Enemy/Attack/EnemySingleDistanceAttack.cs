using System;
using UnityEngine;

namespace Enemy.Attack
{
    public class EnemySingleDistanceAttack : EnemyDistanceAttack, IAttackBehaviour
    {

        public  EnemySingleDistanceAttack(EnemyActionSetting settings, HealthState healthState, Transform target)
        {
            this.target = target;
            this.healthState = healthState;
            projectilePrefab = settings.projectilePrefab;
            muzzlePosition = settings.muzzlePosition;
            projectileSpeed = settings.projectileSpeed;
            attackDamage = settings.attackDamage;
            
            switch (settings.attackCalculationType)
            {
                case AttackCalculationType.PREDICTED:
                    distanceAttackBehaviour = new PredictedAttack();
                    break;
                case AttackCalculationType.RAY:
                    distanceAttackBehaviour = new RayAttack();
                    break;
                case AttackCalculationType.NONE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        

        public void Attack()
        {
            shootingDirectionNormalized = distanceAttackBehaviour.GetAttackDirection(healthState.transform, target, projectileSpeed);
            ProjectileLauncher.Instance.LaunchProjectile(healthState, projectilePrefab, muzzlePosition, shootingDirectionNormalized, projectileSpeed, attackDamage);
        }
    }
}