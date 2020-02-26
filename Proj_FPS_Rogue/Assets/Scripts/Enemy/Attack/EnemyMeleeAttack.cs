using UnityEngine;

namespace Enemy
{
    public class EnemyMeleeAttack : IAttackBehaviour
    {
        private int _attackDamage;
        
        public EnemyMeleeAttack (EnemyActionSetting setting)
        {
            _attackDamage = setting.damage;
        }
        
        public void Attack()
        {
            //some check if player will get damage
            Debug.Log("Melee attack");
            //DamagePlayer
        }
    }
}