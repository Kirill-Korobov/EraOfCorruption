using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    protected int hp, maxHp, defense, damage;
    protected float speed;

    protected void SetStats(int _hp, int _maxHp, int _defense, int _damage, float _speed)
    {
        hp = _hp; maxHp = _maxHp; defense = _defense; damage = _damage; speed = _speed;
    }

    public abstract void TakeDamage(int damage);

    protected abstract void Heal(int value);

    protected abstract void Die();
}