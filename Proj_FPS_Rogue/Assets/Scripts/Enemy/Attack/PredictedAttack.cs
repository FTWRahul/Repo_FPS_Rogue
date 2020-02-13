using UnityEngine;

namespace Enemy
{
    public class PredictedAttack : IDistanceAttackBehaviour
    {
        #region Predicrtions
    
        private float _projectileTravelTime;
        private float _distanceToTarget;
        private Vector3 _predictedPosition;

        #endregion
    
        public Vector3 GetAttackDirection(Transform transform, Transform target, float projectileSpeed)
        {
            _distanceToTarget = Vector3.Distance(transform.position, target.position);
            _projectileTravelTime = _distanceToTarget / projectileSpeed;
            _predictedPosition = target.position + target.GetComponent<CharacterController>().velocity * _projectileTravelTime;
            return (_predictedPosition - transform.position).normalized;
        }

        public Vector3 GetAttackDirection(Transform transform, Transform target)
        {
            throw new System.NotImplementedException();
        }
    }
}