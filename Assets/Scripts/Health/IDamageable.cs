public interface IDamageable
{
    public void Damage(int damage);
    public bool IsDead { get; }
}