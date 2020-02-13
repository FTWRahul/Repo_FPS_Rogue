using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyMovementSetting", menuName = "Enemy/MovementSetting", order = 1)]
    public class EnemyMovementSetting : ScriptableObject
    {
        #region MOVEMENT

        public EnemyMovementType movementType;
        public float stoppingDistance;
        public float movementSpeed;

        #endregion
    }
}