using System;
using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform muzzlePosition;
    
    public bool isAttacking;
    public bool inRange;
    public bool inAngle;
    public bool isReloading;
    
    #region AttackSettings

    public float maxAngleDelta;

    public float attackRange;
    public float attackRate;
    public float projectileSpeed;
    public int attackDamage;
    
    #endregion

    #region Predicrtions
    
    private float _projectileTravelTime;
    private float _distanceToTarget;
    private Vector3 _predictedPosition;
    private Vector3 _shootingDirectionNormalized;

    #endregion
    
    #region References

    private Transform _target;
    private CharacterController _targetCharacterController;

    #endregion

    private void Start()
    {
        _target = FindObjectOfType<CharacterData>().transform;
        _targetCharacterController = _target.GetComponent<CharacterController>();
    }

    private void Update()
    {
        inRange = (_target.position - transform.position).sqrMagnitude <= attackRange * attackRate;
        //also check if enemy is looking at target
        
        if (inRange && !isReloading)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toTraget = _target.position - transform.position;
            float angle = Vector3.Angle(forward, toTraget);
            Debug.Log(angle);
        
            inAngle = angle <= maxAngleDelta;
            
            if (inAngle)
            {
                CalculatePrediction();
                Attack();
                StartCoroutine(Reloading());
            }
        }
    }

    
    void CalculatePrediction()
    {
        _distanceToTarget = Vector3.Distance(transform.position, _target.position);
        _projectileTravelTime = _distanceToTarget / projectileSpeed;
        _predictedPosition = _target.position + _targetCharacterController.velocity * _projectileTravelTime;
        _shootingDirectionNormalized = (_predictedPosition - transform.position).normalized;
    }

    void Attack()
    {
        Projectile go = Instantiate(projectilePrefab, muzzlePosition.position, Quaternion.identity).GetComponent<Projectile>();
        go.Initialize(projectileSpeed, _shootingDirectionNormalized, attackDamage);
    }

    IEnumerator Reloading()
    {
        isAttacking = false;
        isReloading = true;
        yield return new WaitForSeconds(attackRate);
        isReloading = false;
    }
}
