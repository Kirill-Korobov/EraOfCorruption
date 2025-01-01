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
    private int level, _XP;
    private Coroutine showLevelUpTextCoroutine;

    private void Awake()
    {
        // Set XP and level.
        Level = 0;
        XP = 0;
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
            return level;
        }
        set
        {
            if (value < 0)
            {
                level = 0;
            }
            else
            {
                level = value;
            }
        }
    }

    public int XP
    {
        get
        {
            return _XP;
        }
        set
        {
            if (value < 0)
            {
                _XP = 0;
            }
            else
            {
                _XP = value;
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