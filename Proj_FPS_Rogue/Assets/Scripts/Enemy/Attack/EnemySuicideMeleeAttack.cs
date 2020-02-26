using System.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public class EnemySuicideMeleeAttack : EnemyMeleeAttack, IAttackBehaviour
    {
        private readonly int _timeToExplode;

        public EnemySuicideMeleeAttack(EnemyActionSetting setting)
        {
            attackDamage = setting.damage;
            _timeToExplode = setting.timeToExplode;
        }
        public void Attack()
        {
            Debug.Log("Start to explode");
            
            Explode();
        }
        
        async void Explode()
        {
            await Task.Delay(_timeToExplode);
            
            Debug.Log("Explode");
        }
    }
}