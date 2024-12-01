using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int defense;
    public float moveSpeed;

    public void Initialize(EnemyScriptableObject data)
    {
        health = data.health;
        damage = data.damage;
        defense = data.defense;
        moveSpeed = data.moveSpeed;
    }

    private void OnDeath()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}
