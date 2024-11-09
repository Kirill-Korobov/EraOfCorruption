using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterStats : Mob
{
    private int currentXPAmount;
    private int level;
    private int statisticsPoints;

    public void GetXP(int value)
    {
        currentXPAmount += value;
        CheckForLevelUp();
    }

    protected void CheckForLevelUp()
    {
        // if (currentXPAmount >= nessesaryXPAmount)
        // {
        //    LevelUp();
        // }
    }

    protected void LevelUp()
    {
        level++;
    }

    public override void TakeDamage(int _damage)
    {
        if (defense >= damage)
        {
            health--;
        }
        else
        {
            health -= damage - defense;
        }
        if (health <= 0)
        {
            Die();
        }
    }

    protected override void Heal(int value)
    {
        if (health + value > maxHp) 
        {
            health = maxHp;
        }
        else
        {
            health += value;
        }
    }

    protected override void Die()
    {
        Debug.Log("You died");
        Destroy(gameObject);
    }
}