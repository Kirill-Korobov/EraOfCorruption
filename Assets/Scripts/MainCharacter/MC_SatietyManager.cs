using UnityEngine;

public class MC_SatietyManager : MonoBehaviour
{
    private float satiety;
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private MC_HealthManager healthManager;
    [SerializeField] private MC_EnergyManager energyManager;
    [SerializeField] private MC_ManaManager manaManager;
    [SerializeField] private MC_StatisticsManager statisticsManager;
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
            else if (value > statisticsInfo.SatietyMaxValue)
            {
                satiety = statisticsInfo.SatietyMaxValue;
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
        if (Satiety >= statisticsInfo.SatietySpendingMultiplier * Time.deltaTime)
        {
            if (healthManager.Health < statisticsInfo.MaxHPValues[statisticsManager.HPLevel] && canReplenishHealth)
            {
                healthManager.GetHealth(statisticsInfo.MaxHPValues[statisticsManager.HPLevel] * statisticsInfo.HealthReplenishmentMultiplier * Time.deltaTime);
                Satiety -= statisticsInfo.SatietySpendingMultiplier * Time.deltaTime;
            }
        }
        else
        {
            healthManager.TakeDamage(2 * statisticsInfo.MaxHPValues[statisticsManager.HPLevel] * statisticsInfo.HealthReplenishmentMultiplier * Time.deltaTime);
        }

        if (energyManager.Energy < statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel] && Satiety >= statisticsInfo.SatietySpendingMultiplier * Time.deltaTime && canReplenishEnergy)
        {
            energyManager.ReplenishEnergy(statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel] * statisticsInfo.EnergyReplenishmentMultiplier * Time.deltaTime);
            Satiety -= statisticsInfo.SatietySpendingMultiplier * Time.deltaTime;
        }

        if (manaManager.Mana < (statisticsInfo.ÑloseCombatAdditionalManaValues[statisticsManager.CloseCombatLevel] + statisticsInfo.RangedCombatAdditionalManaValues[statisticsManager.RangedCombatLevel] + statisticsInfo.MagicCombatAdditionalManaValues[statisticsManager.MagicCombatLevel]) && Satiety >= statisticsInfo.SatietySpendingMultiplier * Time.deltaTime && canReplenishMana)
        {
            manaManager.ReplenishMana((statisticsInfo.ÑloseCombatAdditionalManaValues[statisticsManager.CloseCombatLevel] + statisticsInfo.RangedCombatAdditionalManaValues[statisticsManager.RangedCombatLevel] + statisticsInfo.MagicCombatAdditionalManaValues[statisticsManager.MagicCombatLevel]) * statisticsInfo.ManaReplenishmentMultiplier * Time.deltaTime);
            Satiety -= statisticsInfo.SatietySpendingMultiplier * Time.deltaTime;
        }
    }
}