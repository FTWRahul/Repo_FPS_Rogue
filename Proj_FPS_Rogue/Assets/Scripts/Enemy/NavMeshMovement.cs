using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class NavMeshMovement : MonoBehaviour, IMovementBehaviour
    {
        public float stoppingDistance;
        private Vector3 _desirePosition;
    
        public NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Init(float speed, float stop)
        {
            navMeshAgent.speed = speed;
            navMeshAgent.stoppingDistance = stop;
            stoppingDistance = stop;
        }

        public void Move(Vector3 targetPosition)
        {
            CalculateDesiredPosition(targetPosition);
            navMeshAgent.SetDestination(_desirePosition);
        }
    
        void CalculateDesiredPosition(Vector3 targetPosition)
        {
            if(NavMesh.SamplePosition(targetPosition, out var myNavHit, stoppingDistance, -1))
            {
                _desirePosition = myNavHit.position;
            }
        }
    }
}