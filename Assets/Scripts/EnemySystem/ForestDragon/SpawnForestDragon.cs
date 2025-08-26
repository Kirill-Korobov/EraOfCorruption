using UnityEngine;

public class SpawnForestDragon : MonoBehaviour
{
    [SerializeField] private ForestDragonInfo forestDragonInfo;
    [HideInInspector] public bool bossIsSpawned;

    private void Awake()
    {
        bossIsSpawned = false;
    }

    public void Spawn()
    {
        if (!bossIsSpawned)
        {
            Instantiate(forestDragonInfo.BossPrefab, gameObject.transform);
            bossIsSpawned = true;
        }
    }
}