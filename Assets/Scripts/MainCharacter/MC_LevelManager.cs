using UnityEngine;

public class MC_LevelManager : MonoBehaviour
{
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private MC_StatisticsManager statisticsManager;
    [SerializeField] private LevelUpTextBehaviour levelUpTextBehaviour;
    [SerializeField] private MC_HealthManager healthManager;
    [SerializeField] private MC_EnergyManager energyManager;
    [SerializeField] private MC_ManaManager manaManager;
    [SerializeField] private MC_SatietyManager satietyManager;
    [SerializeField] private GameStatsManager gameStatsManager;
    private Coroutine showLevelUpTextCoroutine;
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
        Level = currentGameStats.mainCharacterStats.level;
        XP = currentGameStats.mainCharacterStats._XP;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            IncreaseXP(100);
        }
    }

    public int Level
    {
        get
        {
            return currentGameStats.mainCharacterStats.level;
        }
        set
        {
            if (value < 0)
            {
                currentGameStats.mainCharacterStats.level = 0;
            }
            else
            {
                currentGameStats.mainCharacterStats.level = value;
            }
        }
    }

    public int XP
    {
        get
        {
            return currentGameStats.mainCharacterStats._XP;
        }
        set
        {
            if (value < 0)
            {
                currentGameStats.mainCharacterStats._XP = 0;
            }
            else
            {
                currentGameStats.mainCharacterStats._XP = value;
            }
            CheckForLevelUp();
        }
    }

    public void IncreaseXP(int value)
    {
        XP += (int)(value * statisticsInfo.XPMultiplierValues[statisticsManager.XPMultiplierLevel]);
    }

    private void CheckForLevelUp()
    {
        if (Level != statisticsInfo.MaxLevel && XP >= statisticsInfo.NessaseryXPValuesToLevelUp[Level])
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (showLevelUpTextCoroutine != null)
        {
            StopCoroutine(showLevelUpTextCoroutine);
        }
        levelUpTextBehaviour.makeTransparent = false;
        showLevelUpTextCoroutine = StartCoroutine(levelUpTextBehaviour.ShowLevelUpText());
        healthManager.Health = statisticsInfo.MaxHPValues[statisticsManager.HPLevel];
        energyManager.Energy = statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel];
        manaManager.Mana = statisticsInfo.ÑloseCombatAdditionalManaValues[statisticsManager.CloseCombatLevel] + statisticsInfo.RangedCombatAdditionalManaValues[statisticsManager.RangedCombatLevel] + statisticsInfo.MagicCombatAdditionalManaValues[statisticsManager.MagicCombatLevel];
        satietyManager.Satiety = statisticsInfo.SatietyMaxValue;
        XP -= statisticsInfo.NessaseryXPValuesToLevelUp[Level];
        Level++;
        statisticsManager.StatisticPoints += statisticsInfo.StatisticPointsPerLevel;
    }
}