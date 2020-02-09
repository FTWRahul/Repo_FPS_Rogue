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