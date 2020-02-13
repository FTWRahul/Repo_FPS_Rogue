using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyActionController : MonoBehaviour
    {
        #region STATS

        public bool canAttack;
        public bool isAttacking;
        public bool isResetting;

        #endregion
        
        #region ACTION SETTINGS
    
        private float _attackRate;

        #endregion
        
        #region REFERENCES
        
        private IAttackBehaviour _attackBehaviour;
        
        #endregion
        
        public void Init(EnemyActionSetting settings)
        {
            _attackRate = settings.attackRate;

            switch (settings.enemyActionType)
            {
                case EnemyActionType.DISTANCE:
                    _attackBehaviour = new EnemyDistanceAttack(settings);
                    break;
                case EnemyActionType.MELEE:
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateActionState(EnemyState newState)
        {
            switch (newState)
            {
                case EnemyState.PURSUE:
                    canAttack = false;
                    break;
                case EnemyState.ATTACK:
                    canAttack = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void Update()
        {
            if (canAttack && !isResetting)
            {
                isAttacking = true;
                _attackBehaviour.Attack();
                StartCoroutine(ResetAction());
            }
        }
    
        IEnumerator ResetAction()
        {
            isAttacking = false;
            isResetting = true;
        
            yield return new WaitForSeconds(_attackRate);
        
            isResetting = false;
        }
    }
}