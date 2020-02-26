using System;
using UnityEngine;

namespace Enemy.Attack
{
    public class EnemyTripleDistanceAttack : EnemyDistanceAttack, IAttackBehaviour
    {
        private float _angle = -20;
        private readonly float _originalAngle;

        public EnemyTripleDistanceAttack(EnemyActionSetting settings, HealthState healthState, Transform target)
        {
            this.healthState = healthState;
            this.target = target;
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

            _originalAngle = _angle;
        }
        
        public void Attack()
        {
            for (int i = 1; i < 4; i++)
            {
                shootingDirectionNormalized = distanceAttackBehaviour.GetAttackDirection(healthState.transform, target, projectileSpeed);
                
                var dir = Quaternion.Euler(0,_angle,0) * shootingDirectionNormalized;
                GameObject go = ProjectileLauncher.Instance.LaunchProjectile(healthState, projectilePrefab, muzzlePosition, dir, projectileSpeed, attackDamage);
                _angle += Mathf.Abs(_originalAngle);
            }
            _angle = _originalAngle;
        }
    }
}