using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private EnemiesInfo enemiesInfo;
    [SerializeField] private GameStatsManager gameStatsManager;
    private List<EnemyInfo> enemiesToSpawn;
    private float spawnChancesSum;
    private GameStats currentGameStats;
    private Transform mainCharacterTransform;
    private Coroutine spawnEnemiesCoroutine;
    public int enemyCounter;

    private void Start()
    {
        switch (GameStatsManager.currentGame)
        {
            case 1:
                currentGameStats = gameStatsManager.game1Stats;
                break;
            case 2:
                currentGameStats = gameStatsManager.game2Stats;
                break;
            case 3:
                currentGameStats = gameStatsManager.game3Stats;
                break;
            default:
                currentGameStats = gameStatsManager.game1Stats;
                break;
        }
        mainCharacterTransform = GameObject.FindGameObjectWithTag("MainCharacter").transform;
        enemiesToSpawn = new List<EnemyInfo>();
        if (spawnEnemiesCoroutine == null)
        {
            spawnEnemiesCoroutine = StartCoroutine(SpawnEnemiesCoroutine());
        }
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemiesInfo.averageSpawnIntervals[currentGameStats.currentSublocation] * Random.Range(enemiesInfo.MinSpawnIntervalMultiplier, enemiesInfo.MaxSpawnIntervalMultiplier));
            if (enemiesToSpawn.Count != 0 && Random.Range(0f, 1f) > enemiesInfo.skipEnemySpawnChances[currentGameStats.currentSublocation] && enemyCounter < enemiesInfo.MaxEnemyNumber)
            {
                float randomFloatValue = Random.Range(0, spawnChancesSum);
                float bufferFloatValue = 0f;
                for (int i = 0; i < enemiesToSpawn.Count; i++)
                {
                    if (randomFloatValue <= bufferFloatValue + enemiesToSpawn[i].SpawnChance)
                    {
                        GameObject bufferEnemy = Instantiate(enemiesToSpawn[i].EnemyPrefab, gameObject.transform);
                        NavMeshAgent agent = bufferEnemy.GetComponent<NavMeshAgent>();
                        agent.enabled = false;
                        bufferEnemy.transform.position = GetRandomNavMeshLocation();
                        agent.Warp(bufferEnemy.transform.position);
                        agent.enabled = true;
                        break;
                    }
                    bufferFloatValue += enemiesToSpawn[i].SpawnChance;
                }
                enemyCounter++;
            }
        }
    }

    public void ChangeEnemiesToSpawn()
    {
        enemiesToSpawn.Clear();
        for (int i = 0; i < enemiesInfo.enemiesInfo.Count; i++)
        {
            for (int j = 0; j < enemiesInfo.enemiesInfo[i].SpawnSublocations.Length; j++)
            {
                if (enemiesInfo.enemiesInfo[i].SpawnSublocations[j] == currentGameStats.currentSublocation)
                {
                    enemiesToSpawn.Add(enemiesInfo.enemiesInfo[i]);
                    break;
                }
            }
        }
        spawnChancesSum = 0f;
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            spawnChancesSum += enemiesToSpawn[i].SpawnChance;
        }
    }

    private Vector3 GetRandomNavMeshLocation()
    {
        int maxAttempts = 30;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized;
            Vector3 candidatePos = mainCharacterTransform.position + new Vector3(randomCircle.x, 0f, randomCircle.y) * Random.Range(enemiesInfo.MinSpawnRadius, enemiesInfo.MaxSpawnRadius);

            if (NavMesh.SamplePosition(candidatePos, out NavMeshHit hit, 10f, NavMesh.AllAreas))
            {
                float distance = Vector3.Distance(mainCharacterTransform.position, hit.position);
                if (distance >= enemiesInfo.MinSpawnRadius && distance <= enemiesInfo.MaxSpawnRadius)
                {
                    return hit.position;
                }
            }
        }

        return Vector3.zero;
    }
}