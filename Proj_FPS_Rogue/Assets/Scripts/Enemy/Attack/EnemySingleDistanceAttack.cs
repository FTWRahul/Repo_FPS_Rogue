using System;
using UnityEngine;

namespace Enemy.Attack
{
    public class EnemySingleDistanceAttack : IAttackBehaviour
    {
        private readonly GameObject _projectilePrefab;
        private readonly Transform _muzzlePosition;
        private readonly float _projectileSpeed;
        private readonly int _attackDamage;
        private  Vector3 _shootingDirectionNormalized;
        
        private readonly IDistanceAttackBehaviour _distanceAttackBehaviour;
        
        //shouldn't be here - temporary
        private readonly HealthState _healthState;
        private readonly Transform _target;
        
        public  EnemySingleDistanceAttack(EnemyActionSetting settings, HealthState healthState, Transform target)
        {
            this._target = target;
            this._healthState = healthState;
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