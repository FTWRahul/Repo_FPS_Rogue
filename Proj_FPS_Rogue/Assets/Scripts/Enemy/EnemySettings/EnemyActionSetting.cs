using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyActionSetting", menuName = "Enemy/ActionSetting", order = 2)]
    public class EnemyActionSetting : ScriptableObject
    {
        #region ACTION

        public EnemyActionType enemyActionType;
        public float attackRate;
        public int attackDamage;
        public int maxAttackAngle;
        public int maxAttackDistance;

        #endregion

        #region DISTANCE ATTACK

        public EnemyDistanceAttackType enemyDistanceAttackType;
        public GameObject projectilePrefab;
        public Transform muzzlePosition;
        public float projectileSpeed;

        #endregion
    }
}