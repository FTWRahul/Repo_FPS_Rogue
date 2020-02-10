using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyMovementController : MonoBehaviour
    {
        #region Movement settings

        public float targetStoppingDistance;
        public float movementSpeed;
        
        #endregion

        #region References

        private Transform _target;
        private Vector3 TargetPosition => _target.position;
        private Vector3 _desirePosition;
        
        private NavMeshAgent _navMeshAgent;

        #endregion

        private void Start()
        {
            _target = FindObjectOfType<CharacterData>().transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = movementSpeed;
            _navMeshAgent.stoppingDistance = targetStoppingDistance;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, TargetPosition) > targetStoppingDistance)
            {
                CalculateDesirePosition();

                _navMeshAgent.SetDestination(_desirePosition);
            }
            
            FaceTarget();
        }

        void CalculateDesirePosition()
        {
            if(NavMesh.SamplePosition(TargetPosition, out var myNavHit, targetStoppingDistance, -1))
            {
                _desirePosition = myNavHit.position;
            }
        }
        
        private void FaceTarget()
        {
            Vector3 lookPosition = TargetPosition;
            lookPosition.y = 0;
            Quaternion rot = Quaternion.LookRotation(lookPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);  
        }
    }
}
