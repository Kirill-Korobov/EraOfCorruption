using UnityEngine;

public class ForestDragonFireball : MonoBehaviour
{
    [SerializeField] private ForestDragonInfo forestDragonInfo;
    private ForestDragon forestDragon;
    private MC_HealthManager healthManager;
    private Vector3 flightDirection;
    private float bufferDissapearTime;

    void Awake()
    {
        GameObject mainCharacter = GameObject.FindGameObjectWithTag("MainCharacter");
        healthManager = mainCharacter.GetComponent<MC_HealthManager>();
        forestDragon = FindAnyObjectByType<ForestDragon>().GetComponent<ForestDragon>();
        bufferDissapearTime = forestDragonInfo.FireballDissapearTime;
    }

    void Update()
    {
        gameObject.transform.Translate(flightDirection.normalized * forestDragonInfo.FireballSpeed * forestDragon.currentPhaseStatsMultiplier * Time.deltaTime);
        bufferDissapearTime -= Time.deltaTime;
        if (bufferDissapearTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetFlightDirection(Vector3 direction)
    {
        flightDirection = direction.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCharacter")
        {
            healthManager.TakeDamage(forestDragonInfo.FireballDamage * forestDragon.currentPhaseStatsMultiplier);
            Destroy(gameObject);
        }
    }
}