using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyDistanceAttack : IAttackBehaviour
    {
        private readonly GameObject _projectilePrefab;
        private readonly Transform _muzzlePosition;
        private readonly float _projectileSpeed;
        private int _attackDamage;
        private Vector3 _shootingDirectionNormalized;
        
        private IDistanceAttackBehaviour _distanceAttackBehaviour;
        
        //shouldn't be here - temporary
        private HealthState _healthState;
        private Transform _target;
        public EnemyDistanceAttack(EnemyActionSetting settings, HealthState healthState, Transform target)
        {
            _target = target;
            _healthState = healthState;
            _projectilePrefab = settings.projectilePrefab;
            _muzzlePosition = settings.muzzlePosition;
            _projectileSpeed = settings.projectileSpeed;
            _attackDamage = settings.attackDamage;
            
            switch (settings.enemyDistanceAttackType)
            {
                case EnemyDistanceAttackType.PREDICTED:
                    _distanceAttackBehaviour = new PredictedAttack();
                    break;
                case EnemyDistanceAttackType.RAY:
                    _distanceAttackBehaviour = new RayAttack();
                    break;
                case EnemyDistanceAttackType.NONE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        

        public void Attack()
        {
            _shootingDirectionNormalized = _distanceAttackBehaviour.GetAttackDirection(_healthState.transform, _target, _projectileSpeed);
            ProjectileLauncher.Instance.LaunchProjectile(_healthState, _projectilePrefab, _muzzlePosition, _shootingDirectionNormalized, _projectileSpeed, _attackDamage);
            /*Projectile go = Instantiate(_projectilePrefab, _muzzlePosition.position, Quaternion.identity).GetComponent<Projectile>();
            go.Initialize(_projectileSpeed, _shootingDirectionNormalized, _attackDamage);*/
        }
    }
}