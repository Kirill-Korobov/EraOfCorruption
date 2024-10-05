using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterStats : Mob
{
    public override void TakeDamage(int _damage)
    {
        if (defense >= damage)
        {
            hp--;
        }
        else
        {
            hp -= damage - defense;
        }
        if (hp <= 0)
        {
            Die();
        }
    }

    protected override void Heal(int value)
    {
        if (hp + value > maxHp) 
        {
            hp = maxHp;
        }
        else
        {
            hp += value;
        }
    }

    protected override void Die()
    {
        Debug.Log("You died");
        Destroy(gameObject);
    }
}