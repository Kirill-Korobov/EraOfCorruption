using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/Create New Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Statistics")]
    public string enemyName;
    public int health;
    public int defense;
    public int damage;
    public float damageCoolDown;
    public float moveSpeed;
    [Header("Attack effect")]
    public string attackEffect;
    public float attackEffectDuration;
    public float attackEffectChance;

    [Header("Drop methods")]
    public GameObject[] dropItems;
    public float dropChance; 

    [Header("Death effect")]
    public GameObject deathEffect;
    [Header("Dead awards")]
    public int deathMoneyAward;
    public int deathXPAward;
}
