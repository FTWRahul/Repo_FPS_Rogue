using UnityEngine;

namespace Enemy
{
    public class EnemyMeleeAttack : IAttackBehaviour
    {
        private int _attackDamage;
        
        public EnemyMeleeAttack (EnemyActionSetting setting)
        {
            _attackDamage = setting.attackDamage;
        }
        
        public void Attack()
        {
            Debug.Log("Melee attack");
            //DamagePlayer
        }
    }
}