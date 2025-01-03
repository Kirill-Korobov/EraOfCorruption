using UnityEngine;

public class MC_SatietyManager : MonoBehaviour
{
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private MC_HealthManager healthManager;
    [SerializeField] private MC_EnergyManager energyManager;
    [SerializeField] private MC_ManaManager manaManager;
    [SerializeField] private MC_StatisticsManager statisticsManager;
    [SerializeField] private GameStatsManager gameStatsManager;
    [HideInInspector] public bool canReplenishHealth, canReplenishEnergy, canReplenishMana;
    private GameStats currentGameStats;

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
        Satiety = currentGameStats.mainCharacterStats.satiety;
        StartCoroutine(healthManager.WaitUntilHealthCanBeReplenished());
        canReplenishEnergy = true;
        canReplenishMana = true;
    }

    public float Satiety
    {
        get
        {
            return currentGameStats.mainCharacterStats.satiety;
        }
        set
        {
            if (value <= 0)
            {
                currentGameStats.mainCharacterStats.satiety = 0;
            }
            else if (value > statisticsInfo.SatietyMaxValue)
            {
                currentGameStats.mainCharacterStats.satiety = statisticsInfo.SatietyMaxValue;
            }
            else
            {
                currentGameStats.mainCharacterStats.satiety = value;
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