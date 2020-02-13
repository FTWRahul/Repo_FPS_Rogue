using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public enum MovementState
    {
        FOLLOWING,
        DODGING,
    }
    
    public class EnemyMovementController : MonoBehaviour
    {
        #region STATS

        private MovementState _movementState;

        #endregion
        
        #region REFERENCES

        private Transform _target; 
        private Vector3 TargetPosition => _target.position;
        private IMovementBehaviour _movementBehaviour;

        #endregion

        public void Init(Transform target,EnemyMovementSetting settings)
        {
            _target = target;
            switch (settings.movementType)
            {
                case EnemyMovementType.GROUND:
                    NavMeshAgent agent = gameObject.AddComponent<NavMeshAgent>();
                    _movementBehaviour = new NavMeshMovement(agent, settings.movementSpeed, settings.stoppingDistance);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void Update()
        {
            if (_movementState == MovementState.FOLLOWING)
            {
                _movementBehaviour.Move(TargetPosition);
            }
            
            FaceTarget();
        }

        public void UpdateState(EnemyState newState)
        {
            switch (newState)
            {
                case EnemyState.PURSUE:
                    _movementState = MovementState.FOLLOWING;
                    break;
                case EnemyState.ATTACK:
                    _movementState = MovementState.DODGING;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void FaceTarget()
        {
            Vector3 lookPosition = TargetPosition;
            lookPosition.y = transform.position.y;
            transform.LookAt(lookPosition);
        }
    }
}