using UnityEngine;

public interface IShootBehaviour
{
    void Fire();
}

public interface IReceiveDamage
{
    void ApplyDamage(int damage);
}


public interface IGunPart
{
    void TryPickUp(Gun gun);

    void UpdateGun();
    GunPartEnum Part();
}

public interface IBulletModifier
{
    
}

//Interface for different types of movement for enemy
public interface IMovementBehaviour
{
    void Init(float speed, float stop);
    void Move(Vector3 targetPosition);
}