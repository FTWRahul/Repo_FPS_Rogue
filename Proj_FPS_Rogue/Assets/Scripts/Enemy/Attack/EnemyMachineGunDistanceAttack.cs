using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Attack
{
    public class EnemyMachineGunDistanceAttack : EnemyDistanceAttack, IAttackBehaviour
    {
        private readonly float _offset;
        private readonly int _rate; //in milliseconds
        private readonly int _magazineAmount;

        public EnemyMachineGunDistanceAttack(EnemyActionSetting settings, HealthState healthState, Transform target)
        {
            this.healthState = healthState;
            this.target = target;
            projectilePrefab = settings.projectilePrefab;
            muzzlePosition = settings.muzzlePosition;
            projectileSpeed = settings.projectileSpeed;
            attackDamage = settings.damage;
            _offset = settings.offset;
            _rate = settings.rate;
            _magazineAmount = settings.magazineAmount;
            
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
            AttackAsync();
        }


        async void AttackAsync()
        {
            for (int i = 0; i < _magazineAmount; i++)
            {
                // Wait for rate
                await Task.Delay(_rate);

                shootingDirectionNormalized =
                    distanceAttackBehaviour.GetAttackDirection(healthState.transform, target, projectileSpeed);
                shootingDirectionNormalized = new Vector3(shootingDirectionNormalized.x + Random.Range(-_offset, _offset),
                    shootingDirectionNormalized.y + Random.Range(-_offset, _offset),
                    shootingDirectionNormalized.z + Random.Range(-_offset, _offset));
                ProjectileLauncher.Instance.LaunchProjectile(healthState, projectilePrefab, muzzlePosition,
                    shootingDirectionNormalized, projectileSpeed, attackDamage);
            }
        }
    }
}