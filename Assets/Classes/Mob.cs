using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    protected int health, maxHp, defense, damage;
    protected float speed;

    protected void SetStats(int _health, int _maxHp, int _defense, int _damage, float _speed)
    {
        health = _health; maxHp = _maxHp; defense = _defense; damage = _damage; speed = _speed;
    }

    public abstract void TakeDamage(int damage);

    protected abstract void Heal(int value);

    protected abstract void Die();
}