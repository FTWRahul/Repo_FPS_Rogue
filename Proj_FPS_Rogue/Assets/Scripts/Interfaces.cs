using UnityEngine;

public interface IShootBehaviour
{
    void Init();
    void Fire();
}

public interface IReceiveDamage
{
    Damage ApplyDamage(int damage, float angle);
}


public interface IGunPart
{
    void TryPickUp(Gun inGun);

    void UpdateGun();

    void RemovePart();
    Transform GetTransform();
}

public interface IBulletModifier
{
    void OnAttach(GameObject behaviour);
}

//Interface for different types of movement for enemy
public interface IMovementBehaviour
{
    void Move(Vector3 targetPosition);
}

public interface IAttackBehaviour
{
    void Attack();
}

public interface IDistanceAttackBehaviour
{
    Vector3 GetAttackDirection(Transform transform,Transform target, float projectileSpeed);
    Vector3 GetAttackDirection(Transform transform,Transform target);
}

public interface IMeleeAttack
{
    
}

public interface IDropItem
{
    void Drop();
}