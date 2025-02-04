using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "NewBoss", menuName = "Boss/NewBoss")]

public class BossScriptableObject : ScriptableObject
{
    [Header("Statistics")]
    public GameObject prefab;
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
    public DropItem[] dropItems;

    [Header("Death effect")]
    public GameObject deathEffect;
    [Header("Dead awards")]
    public int deathMoneyAward;
    public int deathXPAward;
}