using UnityEngine;

public class ForestDragonFightSphereLogic : MonoBehaviour
{
    private SpawnForestDragon spawnForestDragon;

    private void Awake()
    {
        spawnForestDragon = FindAnyObjectByType<SpawnForestDragon>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCharacter" && spawnForestDragon.bossIsSpawned)
        {
            FindAnyObjectByType<MC_HealthManager>().TakeDamage(float.MaxValue);
        }
    }
}