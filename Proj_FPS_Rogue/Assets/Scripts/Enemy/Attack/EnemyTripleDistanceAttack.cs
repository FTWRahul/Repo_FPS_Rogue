using System;
using UnityEngine;

namespace Enemy.Attack
{
    public class EnemyTripleDistanceAttack : IAttackBehaviour
    { 
        private readonly GameObject _projectilePrefab;
        private readonly Transform _muzzlePosition;
        private readonly float _projectileSpeed;
        private readonly int _attackDamage;
        private  Vector3 _shootingDirectionNormalized;
        private float _angle = -20;
        private readonly float _originalAngle;
        
        private readonly IDistanceAttackBehaviour _distanceAttackBehaviour;
        
        //shouldn't be here - temporary
        private readonly HealthState _healthState;
        private readonly Transform _target;

        public EnemyTripleDistanceAttack(EnemyActionSetting settings, HealthState healthState, Transform target)
        {
            _healthState = healthState;
            _target = target;
            target = target;
            healthState = healthState;
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

            _originalAngle = _angle;
        }
        
        public void Attack()
        {
            for (int i = 1; i < 4; i++)
            {
                Debug.Log(i + " times ");
                _shootingDirectionNormalized = _distanceAttackBehaviour.GetAttackDirection(_healthState.transform, _target, _projectileSpeed);
                
                var dir = Quaternion.Euler(0,_angle,0) * _shootingDirectionNormalized;
                GameObject go = ProjectileLauncher.Instance.LaunchProjectile(_healthState, _projectilePrefab, _muzzlePosition, dir, _projectileSpeed, _attackDamage);
                _angle += Mathf.Abs(_originalAngle);
            }
            _angle = _originalAngle;
        }
    }
}