public interface IShootBehaviour
{
    void Fire();
}

public interface IReceiveDamage
{
    void ApplyDamage(int damage);
}