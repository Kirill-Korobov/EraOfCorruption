using UnityEngine;

public class MC_SatietyManager : MonoBehaviour
{
    private float satiety;
    public float satietyMaxValue;
    [SerializeField] private float healthReplenishmentMultiplier, energyReplenishmentMultiplier, manaReplenishmentMultiplier, satietySpendingMultiplier;
    public float timeUntilHealthCanBeReplenished;
    [SerializeField] private MC_HealthManager healthManager;
    [SerializeField] private MC_EnergyManager energyManager;
    [SerializeField] private MC_ManaManager manaManager;
    [HideInInspector] public bool canReplenishHealth, canReplenishEnergy, canReplenishMana;

    private void Awake()
    {
        // Set satiety stats.
        Satiety = 50;

        canReplenishHealth = true;
        canReplenishEnergy = true;
        canReplenishMana = true;
    }

    public float Satiety
    {
        get
        {
            return satiety;
        }
        set
        {
            if (value <= 0)
            {
                satiety = 0;
            }
            else if (value > satietyMaxValue)
            {
                satiety = satietyMaxValue;
            }
            else
            {
                satiety = value;
            }
        }
    }

    public void SpendSatiety(float value)
    {
        Satiety -= value;
    }

    public void ReplenishSatiety(float value)
    {
        Satiety += value;
    }

    private void Update()
    {
        if (Satiety >= satietySpendingMultiplier * Time.deltaTime)
        {
            if (healthManager.CurrentHealth < healthManager.MaxHealth && canReplenishHealth)
            {
                healthManager.GetHealth(healthManager.MaxHealth * healthReplenishmentMultiplier * Time.deltaTime);
                Satiety -= satietySpendingMultiplier * Time.deltaTime;
            }
        }
        else
        {
            healthManager.TakeDamage(2 * healthManager.MaxHealth * healthReplenishmentMultiplier * Time.deltaTime);
        }

        if (energyManager.CurrentEnergy < energyManager.MaxEnergy && Satiety >= satietySpendingMultiplier * Time.deltaTime && canReplenishEnergy)
        {
            energyManager.ReplenishEnergy(energyManager.MaxEnergy * energyReplenishmentMultiplier * Time.deltaTime);
            Satiety -= satietySpendingMultiplier * Time.deltaTime;
        }

        if (manaManager.CurrentMana < manaManager.MaxMana && Satiety >= satietySpendingMultiplier * Time.deltaTime && canReplenishMana)
        {
            manaManager.ReplenishMana(manaManager.MaxMana * manaReplenishmentMultiplier * Time.deltaTime);
            Satiety -= satietySpendingMultiplier * Time.deltaTime;
        }
    }
}