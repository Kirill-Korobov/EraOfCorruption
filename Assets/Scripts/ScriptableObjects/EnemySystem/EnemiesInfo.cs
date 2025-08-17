using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesInfoConfig", menuName = "ScriptableObjects/EnemiesInfo")]

public class EnemiesInfo : ScriptableObject
{
    public List<EnemyInfo> enemiesInfo;

    [SerializeField] private float wanderDestinatioMaxRadius;
    public float WanderDestinatioMaxRadius => wanderDestinatioMaxRadius;

    [SerializeField] private float wanderDestinationRefreshTime;
    public float WanderDestinationRefreshTime => wanderDestinationRefreshTime;

    [SerializeField] private int maxEnemyNumber;
    public int MaxEnemyNumber => maxEnemyNumber;

    [SerializeField] private List<SublocationFloatPair> averageSpawnIntervalsList;

    public Dictionary<Sublocation, float> averageSpawnIntervals;

    [SerializeField] private List<SublocationFloatPair> skipEnemySpawnChancesList;

    public Dictionary<Sublocation, float> skipEnemySpawnChances;

    private void OnEnable()
    {
        averageSpawnIntervals = new Dictionary<Sublocation, float>();
        for (int i = 0; i < averageSpawnIntervalsList.Count; i++)
        {
            averageSpawnIntervals[averageSpawnIntervalsList[i].sublocation] = averageSpawnIntervalsList[i].value;
        }

        skipEnemySpawnChances = new Dictionary<Sublocation, float>();
        for (int i = 0; i < skipEnemySpawnChancesList.Count; i++)
        {
            skipEnemySpawnChances[skipEnemySpawnChancesList[i].sublocation] = skipEnemySpawnChancesList[i].value;
        }
    }

    [SerializeField] private float minSpawnIntervalMultiplier;
    public float MinSpawnIntervalMultiplier => minSpawnIntervalMultiplier;

    [SerializeField] private float maxSpawnIntervalMultiplier;
    public float MaxSpawnIntervalMultiplier => maxSpawnIntervalMultiplier;

    [SerializeField] private float minSpawnRadius;
    public float MinSpawnRadius => minSpawnRadius;

    [SerializeField] private float maxSpawnRadius;
    public float MaxSpawnRadius => maxSpawnRadius;

    [SerializeField] private float enemyDisappearRadius;
    public float EnemyDisappearRadius => enemyDisappearRadius;
}

[Serializable]
public class EnemyInfo
{
    [SerializeField] private GameObject enemyPrefab;
    public GameObject EnemyPrefab => enemyPrefab;

    [SerializeField] private string name;
    public string Name => name;

    [SerializeField] private float _HP;
    public float HP => _HP;

    [SerializeField] private int defense;
    public int Defense => defense;

    [SerializeField] private float damage;
    public float Damage => damage;

    [SerializeField] private float rechargeTime;
    public float RechargeTime => rechargeTime;

    [SerializeField] private float visionRadius;
    public float VisionRadius => visionRadius;

    [SerializeField] private float wanderSpeed;
    public float WanderSpeed => wanderSpeed;

    [SerializeField] private float chaseSpeed;
    public float ChaseSpeed => chaseSpeed;

    [SerializeField] private float attackRadius;
    public float AttackRadius => attackRadius;

    [SerializeField] private int moneyDropAmount;
    public int MoneyDropAmount => moneyDropAmount;

    [SerializeField] private int _XPDropAmount;
    public int XPDropAmount => _XPDropAmount;

    // drops array

    [SerializeField] private Sublocation[] spawnSublocations;
    public Sublocation[] SpawnSublocations => spawnSublocations;

    [SerializeField] private float spawnChance;
    public float SpawnChance => spawnChance;
}

[Serializable]
public struct SublocationFloatPair
{
    public Sublocation sublocation;
    public float value;
}