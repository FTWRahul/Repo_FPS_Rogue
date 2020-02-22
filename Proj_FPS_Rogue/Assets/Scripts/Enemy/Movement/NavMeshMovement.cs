using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class NavMeshMovement : IMovementBehaviour
    {
        private Vector3 _desirePosition;
        private readonly NavMeshAgent _navMeshAgent;

        public NavMeshMovement(NavMeshAgent agent, float speed, float stop)
        {
            _navMeshAgent = agent;
            _navMeshAgent.speed = speed;
            _navMeshAgent.stoppingDistance = stop;
        }

        public void Move(Vector3 targetPosition)
        {
            CalculateDesiredPosition(targetPosition);
            _navMeshAgent.SetDestination(_desirePosition);
        }

        void CalculateDesiredPosition(Vector3 targetPosition)
        {
            if (NavMesh.SamplePosition(targetPosition, out var myNavHit, _navMeshAgent.stoppingDistance, -1))
            {
                _desirePosition = myNavHit.position;
            }
        }
    }

    public class FlyingMovement : IMovementBehaviour
    {

        private Vector3 _targetOldPosition;
        private float _targetLesionAreaRadius;
        private float _targetPathUpdateOffset;
        private Pursuer _pursuer;

        public FlyingMovement(Pursuer pursuer, float lesion, float updateOffset)
        {
            _pursuer = pursuer;
            _targetLesionAreaRadius = lesion;
            _targetPathUpdateOffset = updateOffset;
        }
        
        public void Move(Vector3 targetPosition)
        {
            //if the target has moved from the previous coordinate(targetOldPos) to more than "targetPathUpdateOffset", update the path to the target
            if ((_targetOldPosition - targetPosition).sqrMagnitude > _targetPathUpdateOffset * _targetPathUpdateOffset)
            {
                _targetOldPosition = targetPosition;
                if (_pursuer.GetCurCondition() == "Movement")
                    _pursuer.RefinePath(targetPosition);
                else
                    _pursuer.MoveTo(targetPosition, true);
            }
        }
        
    }

}