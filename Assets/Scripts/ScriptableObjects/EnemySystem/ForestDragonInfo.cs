using UnityEngine;

[CreateAssetMenu(fileName = "ForestDragonInfoConfig", menuName = "ScriptableObjects/ForestDragonInfo")]

public class ForestDragonInfo : ScriptableObject
{
    [Header("General")]

    [SerializeField] private GameObject bossPrefab;
    public GameObject BossPrefab => bossPrefab;

    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private Vector3 spawnPosition;
    public Vector3 SpawnPosition => spawnPosition;

    [SerializeField] private float totalHP;
    public float TotalHP => totalHP;

    [SerializeField] private int generalDefense;
    public int GeneralDefense => generalDefense;

    [SerializeField] private float touchDamage;
    public float TouchDamage => touchDamage;

    [SerializeField] private float changePhaseHPPersent;
    public float ChangePhaseHPPersent => changePhaseHPPersent;

    [SerializeField] private float firstPhaseStatsMultiplier;
    public float FirstPhaseStatsMultiplier => firstPhaseStatsMultiplier;

    [SerializeField] private float secondPhaseStatsMultiplier;
    public float SecondPhaseStatsMultiplier => secondPhaseStatsMultiplier;

    [SerializeField] private float averageAttackDuration;
    public float AverageAttackDuration => averageAttackDuration;

    [SerializeField] private float minAttackDurationMultiplier;
    public float MinAttackDurationMultiplier => minAttackDurationMultiplier;

    [SerializeField] private float maxAttackDurationMultiplier;
    public float MaxAttackDurationMultiplier => maxAttackDurationMultiplier;

    [SerializeField] private int moneyDropAmount;
    public int MoneyDropAmount => moneyDropAmount;

    [SerializeField] private int _XPDropAmount;
    public int XPDropAmount => _XPDropAmount;

    // drops array

    [Header("Bite")]

    [SerializeField] private float walkSpeed;
    public float WalkSpeed => walkSpeed;

    [SerializeField] private float biteDamage;
    public float BiteDamage => biteDamage;

    [SerializeField] private float biteRadius;
    public float BiteRadius => biteRadius;

    [SerializeField] private float biteRechargeTime;
    public float BiteRechargeTime => biteRechargeTime;

    [Header("Shoot Fireball")]

    [SerializeField] private GameObject fireballPrefab;
    public GameObject FireballPrefab => fireballPrefab;

    [SerializeField] private Vector3 fireballSpawnPosition;
    public Vector3 FireballSpawnPosition => fireballSpawnPosition;

    [SerializeField] private float fireballDamage;
    public float FireballDamage => fireballDamage;

    [SerializeField] private float fireballSpeed;
    public float FireballSpeed => fireballSpeed;

    [SerializeField] private float fireballDissapearTime;
    public float FireballDissapearTime => fireballDissapearTime;

    [Header("Spawn Enemies")]

    [SerializeField] private int orcIndex;
    public int OrcIndex => orcIndex;

    [SerializeField] private int golemIndex;
    public int GolemIndex => golemIndex;

    [SerializeField] private Vector3 enemySpawnPosition;
    public Vector3 EnemySpawnPosition => enemySpawnPosition;

    [SerializeField] private float spawnEnemiesRechargeTime;
    public float SpawnEnemiesRechargeTime => spawnEnemiesRechargeTime;

    [SerializeField] private int maxSpawnedEnemiesNumber;
    public int MaxSpawnedEnemiesNumber => maxSpawnedEnemiesNumber;

    [SerializeField] private float spawnEnemiesDefenseMultiplier;
    public float SpawnEnemiesDefenseMultiplier => spawnEnemiesDefenseMultiplier;

    [Header("Heal")]

    [SerializeField] private float healSpeed;
    public float HealSpeed => healSpeed;

    [SerializeField] private float healDefenseMultiplier;
    public float HealDefenseMultiplier => healDefenseMultiplier;

    [Header("Dash")]

    [SerializeField] private float dashSpeed;
    public float DashSpeed => dashSpeed;

    [SerializeField] private float dashTime;
    public float DashTime => dashTime;

    [SerializeField] private float dashStopTime;
    public float DashStopTime => dashStopTime;

    [SerializeField] private float dashDefenseMultiplier;
    public float DashDefenseMultiplier => dashDefenseMultiplier;

    [SerializeField] private float dashStopDefenseMultiplier;
    public float DashStopDefenseMultiplier => dashStopDefenseMultiplier;
}

public enum ForestDragonAttack
{
    bite,
    shootFireball,
    spawnEnemies,
    heal,
    dash
}