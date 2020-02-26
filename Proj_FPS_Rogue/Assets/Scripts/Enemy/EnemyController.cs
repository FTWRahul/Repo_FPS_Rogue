using System;
using NaughtyAttributes;
using UnityEngine;

namespace Enemy
{
    public enum EnemyState
    {
        PURSUE,
        ATTACK,
        DODGE
    }

    public enum EnemyMovementType
    {
        GROUND, 
        FLYING
    }

    public enum EnemyActionType
    {
        DISTANCE,
        MELEE
    }

    public enum DistanceAttackType
    {
        SINGLE,
        TRIPLE,
        MACHINE_GUN,
        NONE
    }

    public enum MeleeAttackType
    {
        EXPLOSION,
        NONE
    }

    public enum AttackCalculationType
    {
        NONE,
        PREDICTED,
        RAY
    }
    
    public class EnemyController : MonoBehaviour
    {
        #region DEBUG

        [BoxGroup("DEBUG")][ReadOnly] public bool inRange;
        [BoxGroup("DEBUG")][ReadOnly] public bool isPlayerBlocked;
        [BoxGroup("DEBUG")][ReadOnly] public bool inAngle;
        [BoxGroup("DEBUG")][ReadOnly] public EnemyState enemyState;

        #endregion
        
        #region ACTION

        public EnemyMovementSetting movementSetting;
        public EnemyActionSetting actionSetting;
        
        #endregion

        #region EVENTS

        [HideInInspector] public Events.OnEnemyStateUpdateEvent onEnemyStateUpdateEvent;

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
            _enemyActionController.Init(actionSetting, _target);
            onEnemyStateUpdateEvent.AddListener(_enemyActionController.UpdateActionState);
            
            _enemyMovementController = GetComponent<EnemyMovementController>();
            _enemyMovementController.Init(_target,movementSetting);
            onEnemyStateUpdateEvent.AddListener(_enemyMovementController.UpdateState);
        }

        private void Update()
        {
            inRange = CheckDistance();
            inAngle = CheckAngle();

            if (inRange && inAngle && !isPlayerBlocked && enemyState != EnemyState.ATTACK)
            {
                UpdateState(EnemyState.ATTACK);
            }
            else if(!inRange || !inAngle || isPlayerBlocked && enemyState != EnemyState.PURSUE)
            {
                UpdateState(EnemyState.PURSUE);
            }
        }

        bool CheckAngle()
        {
            return Vector3.Angle(transform.TransformDirection(Vector3.forward), _target.position - transform.position) <= actionSetting.maxAngle;
        }

        bool CheckDistance()
        {
            return (_target.position - transform.position).sqrMagnitude <= actionSetting.maxDistance * actionSetting.maxDistance;
        }

        void UpdateState(EnemyState newState)
        {
            enemyState = newState;
            onEnemyStateUpdateEvent?.Invoke(enemyState);
        }

        void CheckForBlocking()
        {
            Ray ray = new Ray(transform.position, _target.position);
            if (Physics.Raycast(ray, out RaycastHit hit, actionSetting.maxDistance))
            {
                isPlayerBlocked = hit.transform != _target;
            }
        }
    }
}
