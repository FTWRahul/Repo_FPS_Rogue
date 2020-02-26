using UnityEngine;

public class TripleShootBehaviour : MonoBehaviour, IShootBehaviour
{

    private Gun _gun;

    public float angle = -20;

    private float _originalAngle;
    
    private Vector3 _shootDir;
    private Vector3 _shootPosi;
    
    //just for now here sorry
    private HealthState _playerHealthState;

    private void Awake()
    {
        _originalAngle = angle;
    }

    public void Init()
    {
        _gun = GetComponentInParent<Gun>();
        _playerHealthState = GetComponentInParent<HealthState>();
        _shootDir = _gun.transform.forward;
        _shootPosi = _gun.muzzleTransform.position;
    }

    public void Fire()
    {
        for (int i = 1; i < 4; i++)
        {
            Debug.Log(angle);

            Projectile go = Instantiate(_gun.projectilePrefab, _gun.muzzleTransform.position, Quaternion.identity).GetComponent<Projectile>();
            var dir = Quaternion.Euler(0,angle,0) * _gun.transform.forward;
            angle += Mathf.Abs(_originalAngle);
            go.Initialize(_playerHealthState,_gun.projectileForce, dir, _gun.damage, _gun.bulletModifiers, LayerMask.NameToLayer("Enemy"));
        }
        angle = _originalAngle;
    }
}
