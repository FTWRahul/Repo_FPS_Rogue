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
            if(NavMesh.SamplePosition(targetPosition, out var myNavHit, _navMeshAgent.stoppingDistance, -1))
            {
                _desirePosition = myNavHit.position;
            }
        }
    }
}