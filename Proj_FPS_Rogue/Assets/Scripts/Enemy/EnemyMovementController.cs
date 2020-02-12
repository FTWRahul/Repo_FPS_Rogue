using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovementController : MonoBehaviour
    {
        #region Movement settings

        public float stoppingDistance;
        public float movementSpeed;
        
        #endregion
        
        #region References

        private Transform _target; 
        private Vector3 TargetPosition => _target.position;
        private IMovementBehaviour _movementBehaviour;

        #endregion


        private void Start()
        {
            _target = FindObjectOfType<CharacterData>().transform;
            
            _movementBehaviour = GetComponent<IMovementBehaviour>();
            _movementBehaviour.Init(movementSpeed,stoppingDistance);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, TargetPosition) > stoppingDistance)
            {
                _movementBehaviour.Move(TargetPosition);
            }
            
            FaceTarget();
        }

        private void FaceTarget()
        {
            Vector3 lookPosition = TargetPosition;
            lookPosition.y = transform.position.y;
            transform.LookAt(lookPosition);
        }
    }
}