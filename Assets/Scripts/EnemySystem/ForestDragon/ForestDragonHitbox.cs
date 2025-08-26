using UnityEngine;

public class ForestDragonHitbox : MonoBehaviour
{
    [SerializeField] private ForestDragonInfo forestDragonInfo;
    private ForestDragon forestDragon;
    private MC_HealthManager healthManager;

    private void Awake()
    {
        healthManager = FindAnyObjectByType<MC_HealthManager>().GetComponent<MC_HealthManager>();
        forestDragon = FindAnyObjectByType<ForestDragon>().GetComponent<ForestDragon>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "MainCharacter")
        {
            healthManager.TakeDamage(forestDragonInfo.TouchDamage * forestDragon.currentPhaseStatsMultiplier * Time.deltaTime);
        }
    }
}