using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoss", menuName = "Boss/Create New Boss")]
public class BossScriptableObject : ScriptableObject
{
    [Header("Statistics")]
    public string bossName;
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
    public DropItem[] dropItems;
    [Header("Death effect")]
    public GameObject deathEffect;
    [Header("Dead awards")]
    public int deathMoneyAward;
    public int deathXPAward;
}
