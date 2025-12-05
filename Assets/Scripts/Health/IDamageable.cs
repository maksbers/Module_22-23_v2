public interface IDamageable
{
    float Health { get; }
    float TakeDamage(float damageAmount);
}
