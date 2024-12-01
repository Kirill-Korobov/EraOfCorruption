using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Transform spawnLocation; 
        public EnemyScriptableObject scriptableObject;
    }

    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public float spawnInterval = 5f;
    public GameObject enemyPrefab;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            foreach (var point in spawnPoints)
            {
                SpawnEnemy(point);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy(SpawnPoint point)
    {
        GameObject enemy = Instantiate(enemyPrefab, point.spawnLocation.position, Quaternion.identity);

        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if (enemyComponent != null && point.scriptableObject != null)
        {
            enemyComponent.Initialize(point.scriptableObject);
        }
    }
}
