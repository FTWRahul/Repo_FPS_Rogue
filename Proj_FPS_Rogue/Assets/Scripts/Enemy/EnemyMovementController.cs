using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public enum MovementState
    {
        FOLLOWING,
        ACTING,
        DODGING
    }
    
    public class EnemyMovementController : MonoBehaviour
    {
        #region DEBUG

        [BoxGroup("DEBUG")][SerializeField][ReadOnly] private MovementState movementState;

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
                    _movementBehaviour = new NavMeshMovement(agent, settings.speed, settings.stoppingDistance);
                    break;
                case EnemyMovementType.FLYING:
                    Pursuer pursuer = gameObject.AddComponent<Pursuer>();
                    pursuer.SetConstraints(settings.xMin, settings.xMax, settings.yMin, settings.yMax, settings.zMin,
                        settings.zMax);
                    pursuer.SetPathfindingParameters(settings.selectedPFAlg, settings.pathfindingLevel,
                        settings.inEditorPathfindingTraverce, settings.heuristicFactor, settings.trajectoryOptimization,
                        settings.trajectorySmoothing);
                    pursuer.SetMovementSettings(settings.speed, settings.moveVectorOrientation, settings.turnSpeed);
                    
                    _movementBehaviour = new FlyingMovement(pursuer, settings.lesion, settings.updateOffset);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void Update()
        {
            if (movementState == MovementState.FOLLOWING)
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
                    movementState = MovementState.FOLLOWING;
                    break;
                case EnemyState.ATTACK:
                    movementState = MovementState.ACTING;
                    break;
                case EnemyState.DODGE:
                    movementState = MovementState.DODGING;
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