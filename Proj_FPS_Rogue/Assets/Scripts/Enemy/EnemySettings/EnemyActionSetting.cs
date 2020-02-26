using Enemy.Attack;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyActionSetting", menuName = "Enemy/ActionSetting", order = 2)]
    public class EnemyActionSetting : ScriptableObject
    {
        #region ACTION

        public EnemyActionType actionType;
        public DistanceAttackType distanceAttackType;
        public float attackRate;
        public int damage;
        public int maxAngle;
        public int maxDistance;

        #endregion

        #region DISTANCE ATTACK

        public AttackCalculationType attackCalculationType;
        public GameObject projectilePrefab;
        public Transform muzzlePosition;
        public float projectileSpeed;
        
        public float angleTripleAttack;
        
        public float offset;
        public int rate; 
        public int magazineAmount;
        
        #endregion
    }
}