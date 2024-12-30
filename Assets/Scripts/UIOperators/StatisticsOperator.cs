using TMPro;
using UnityEngine;

public class StatisticsOperator : MonoBehaviour
{
    [SerializeField] private MC_StatisticsManager statisticsManager;
    [SerializeField] private MC_LevelManager levelManager;
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private TMP_Text statisticPointsText, _HPLevelText, energyLevelText, movementLevelText, _XPMultiplierLevelText, closeCombatLevelText, rangedCombatLevelText, magicCombatLevelText;
    private int _HPLevelIncrease, energyLevelIncrease, movementLevelIncrease, _XPMultiplierLevelIncrease, closeCombatLevelIncrease, rangedCombatLevelIncrease, magicCombatLevelIncrease;

    private void OnEnable()
    {
        RefreshUI();
    }

    public void LevelUpHPButton()
    {
        if (statisticsManager.StatisticPoints >= 1 && statisticsManager.HPLevel + _HPLevelIncrease != levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            statisticsManager.StatisticPoints--;
            _HPLevelIncrease++;
            RefreshUI();
        }
        RefreshUI();
    }

    public void LevelUpEnergyButton()
    {
        if (statisticsManager.StatisticPoints >= 1 && statisticsManager.EnergyLevel + energyLevelIncrease != levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            statisticsManager.StatisticPoints--;
            energyLevelIncrease++;
            RefreshUI();
        }
    }

    public void LevelUpMovementButton()
    {
        if (statisticsManager.StatisticPoints >= 1 && statisticsManager.MovementLevel + movementLevelIncrease != levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            statisticsManager.StatisticPoints--;
            movementLevelIncrease++;
            RefreshUI();
        }
    }

    public void LevelUpXPMultiplierButton()
    {
        if (statisticsManager.StatisticPoints >= 1 && statisticsManager.XPMultiplierLevel + _XPMultiplierLevelIncrease != levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            statisticsManager.StatisticPoints--;
            _XPMultiplierLevelIncrease++;
            RefreshUI();
        }
    }

    public void LevelUpCloseCombatButton()
    {
        if (statisticsManager.StatisticPoints >= 1 && statisticsManager.CloseCombatLevel + closeCombatLevelIncrease != levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            statisticsManager.StatisticPoints--;
            closeCombatLevelIncrease++;
            RefreshUI();
        }
    }

    public void LevelUpRangedCombatButton()
    {
        if (statisticsManager.StatisticPoints >= 1 && statisticsManager.RangedCombatLevel + rangedCombatLevelIncrease != levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            statisticsManager.StatisticPoints--;
            rangedCombatLevelIncrease++;
            RefreshUI();
        }
    }

    public void LevelUpMagicCombatButton()
    {
        if (statisticsManager.StatisticPoints >= 1 && statisticsManager.MagicCombatLevel + magicCombatLevelIncrease != levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            statisticsManager.StatisticPoints--;
            magicCombatLevelIncrease++;
            RefreshUI();
        }
    }

    private void SetAllIncreasesZero()
    {
        _HPLevelIncrease = 0;
        energyLevelIncrease = 0;
        movementLevelIncrease = 0;
        _XPMultiplierLevelIncrease = 0;
        closeCombatLevelIncrease = 0;
        rangedCombatLevelIncrease = 0;
        magicCombatLevelIncrease = 0;
    }

    private void RefreshUI()
    {
        statisticPointsText.text = "Statistic points: " + statisticsManager.StatisticPoints.ToString();
        if (statisticsManager.HPLevel + _HPLevelIncrease == levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            _HPLevelText.text = (statisticsManager.HPLevel + _HPLevelIncrease + 1).ToString() + " (max)";
        }
        else
        {
            _HPLevelText.text = (statisticsManager.HPLevel + _HPLevelIncrease + 1).ToString();
        }
        if (statisticsManager.EnergyLevel + energyLevelIncrease == levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            energyLevelText.text = (statisticsManager.EnergyLevel + energyLevelIncrease + 1).ToString() + " (max)";
        }
        else
        {
            energyLevelText.text = (statisticsManager.EnergyLevel + energyLevelIncrease + 1).ToString();
        }
        if (statisticsManager.MovementLevel + movementLevelIncrease == levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            movementLevelText.text = (statisticsManager.MovementLevel + movementLevelIncrease + 1).ToString() + " (max)";
        }
        else
        {
            movementLevelText.text = (statisticsManager.MovementLevel + movementLevelIncrease + 1).ToString();
        }
        if (statisticsManager.XPMultiplierLevel + _XPMultiplierLevelIncrease == levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            _XPMultiplierLevelText.text = (statisticsManager.XPMultiplierLevel + _XPMultiplierLevelIncrease + 1).ToString() + " (max)";
        }
        else
        {
            _XPMultiplierLevelText.text = (statisticsManager.XPMultiplierLevel + _XPMultiplierLevelIncrease + 1).ToString();
        }
        if (statisticsManager.CloseCombatLevel + closeCombatLevelIncrease == levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            closeCombatLevelText.text = (statisticsManager.CloseCombatLevel + closeCombatLevelIncrease + 1).ToString() + " (max)";
        }
        else
        {
            closeCombatLevelText.text = (statisticsManager.CloseCombatLevel + closeCombatLevelIncrease + 1).ToString();
        }
        if (statisticsManager.RangedCombatLevel + rangedCombatLevelIncrease == levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            rangedCombatLevelText.text = (statisticsManager.RangedCombatLevel + rangedCombatLevelIncrease + 1).ToString() + " (max)";
        }
        else
        {
            rangedCombatLevelText.text = (statisticsManager.RangedCombatLevel + rangedCombatLevelIncrease + 1).ToString();
        }
        if (statisticsManager.MagicCombatLevel + magicCombatLevelIncrease == levelManager.Level * statisticsInfo.MaxStatisticLevelMultiplier)
        {
            magicCombatLevelText.text = (statisticsManager.MagicCombatLevel + magicCombatLevelIncrease + 1).ToString() + " (max)";
        }
        else
        {
            magicCombatLevelText.text = (statisticsManager.MagicCombatLevel + magicCombatLevelIncrease + 1).ToString();
        }
    }

    public void CancelButton()
    {
        statisticsManager.StatisticPoints += _HPLevelIncrease + energyLevelIncrease + movementLevelIncrease + _XPMultiplierLevelIncrease + closeCombatLevelIncrease + rangedCombatLevelIncrease + magicCombatLevelIncrease;
        SetAllIncreasesZero();
        RefreshUI();
        gameObject.SetActive(false);
    }

    public void SaveButton()
    {
        for (int i = 0; i < _HPLevelIncrease; i++) 
        {
            statisticsManager.LevelUpHP();
        }
        for (int i = 0; i < energyLevelIncrease; i++)
        {
            statisticsManager.LevelUpEnergy();
        }
        for (int i = 0; i < movementLevelIncrease; i++)
        {
            statisticsManager.LevelUpMovement();
        }
        for (int i = 0; i < _XPMultiplierLevelIncrease; i++)
        {
            statisticsManager.LevelUpXPMultiplier();
        }
        for (int i = 0; i < closeCombatLevelIncrease; i++)
        {
            statisticsManager.LevelUpCloseCombat();
        }
        for (int i = 0; i < rangedCombatLevelIncrease; i++)
        {
            statisticsManager.LevelUpRangedCombat();
        }
        for (int i = 0; i < magicCombatLevelIncrease; i++)
        {
            statisticsManager.LevelUpMagicCombat();
        }
        SetAllIncreasesZero();
        RefreshUI();
        gameObject.SetActive(false);
    }
}