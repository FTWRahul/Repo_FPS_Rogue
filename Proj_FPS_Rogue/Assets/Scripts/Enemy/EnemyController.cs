using System;
using UnityEngine;

namespace Enemy
{
    public enum EnemyState
    {
        PURSUE,
        ATTACK
    }

    public enum EnemyMovementType
    {
        GROUND
    }

    public enum EnemyActionType
    {
        DISTANCE,
        MELEE
    }

    public enum EnemyDistanceAttackType
    {
        NONE,
        PREDICTED,
        RAY
    }
    
    public class EnemyController : MonoBehaviour
    {
        #region STATS

        public bool inRange;
        public bool inAngle;
        public EnemyState enemyState;

        #endregion
        

        #region ACTION

        public EnemyMovementSetting movementSetting;
        public EnemyActionSetting actionSetting;
        
        #endregion

        #region EVENTS

        public Events.OnEnemyStateUpdateEvent onEnemyStateUpdateEvent;

        #endregion

        #region REFERENCES
        
        private Transform _target;
        private EnemyMovementController _enemyMovementController;
        private EnemyActionController _enemyActionController;

        #endregion


        private void Start()
        {
            _target = FindObjectOfType<CharacterData>().transform;
            
            _enemyActionController = GetComponent<EnemyActionController>();
            _enemyActionController.Init(actionSetting);
            onEnemyStateUpdateEvent.AddListener(_enemyActionController.UpdateActionState);
            
            
            _enemyMovementController = GetComponent<EnemyMovementController>();
            _enemyMovementController.Init(_target,movementSetting);
            onEnemyStateUpdateEvent.AddListener(_enemyMovementController.UpdateState);
        }

        private void Update()
        {
            inRange = CheckDistance();
            inAngle = CheckAngle();

            if (inRange && inAngle && enemyState != EnemyState.ATTACK)
            {
                UpdateState(EnemyState.ATTACK);
            }
            else if(!inRange || !inAngle && enemyState != EnemyState.PURSUE)
            {
                UpdateState(EnemyState.PURSUE);
            }
        }

        bool CheckAngle()
        {
            return Vector3.Angle(transform.TransformDirection(Vector3.forward), _target.position - transform.position) <= actionSetting.maxAttackAngle;
        }

        bool CheckDistance()
        {
            return (_target.position - transform.position).sqrMagnitude <= actionSetting.maxAttackDistance * actionSetting.maxAttackDistance;
        }

        void UpdateState(EnemyState newState)
        {
            enemyState = newState;
            onEnemyStateUpdateEvent?.Invoke(enemyState);
        }
    }
}
