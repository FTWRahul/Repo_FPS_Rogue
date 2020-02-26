using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Attack
{
    public class EnemyMachineGunDistanceAttack : EnemyDistanceAttack, IAttackBehaviour
    {
        private float offset = .20f;
        private int _rate = 150; //in milliseconds
        private int _magazineAmount = 36;
        private int _magazineLeft;

        public EnemyMachineGunDistanceAttack(EnemyActionSetting settings, HealthState healthState, Transform target)
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
                shootingDirectionNormalized = new Vector3(shootingDirectionNormalized.x + Random.Range(-offset, offset),
                    shootingDirectionNormalized.y + Random.Range(-offset, offset),
                    shootingDirectionNormalized.z + Random.Range(-offset, offset));
                ProjectileLauncher.Instance.LaunchProjectile(healthState, projectilePrefab, muzzlePosition,
                    shootingDirectionNormalized, projectileSpeed, attackDamage);
            }
        }
    }
}