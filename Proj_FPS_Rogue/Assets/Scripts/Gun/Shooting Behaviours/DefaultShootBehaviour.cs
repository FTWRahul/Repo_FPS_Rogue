using UnityEditorInternal;
using UnityEngine;

public class DefaultShootBehaviour : MonoBehaviour , IShootBehaviour
{
    private Gun _gun;
    
    //just for now here sorry
    private HealthState _playerHealthState;

    private Vector3 _shootDir;
    private Vector3 _shootPosi;

    private void Awake()
    {
       Init();
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
        Projectile go = Instantiate(_gun.projectilePrefab, _gun.muzzleTransform.position, Quaternion.identity).GetComponent<Projectile>();
        go.Initialize(_playerHealthState,_gun.projectileForce, _gun.transform.forward, _gun.damage, _gun.bulletModifiers);
    }

    
}
