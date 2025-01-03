using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameCanvasOperator : MonoBehaviour
{
    [SerializeField] private MC_HealthManager healthManager;
    [SerializeField] private MC_EnergyManager energyManager;
    [SerializeField] private MC_ManaManager manaManager;
    [SerializeField] private MC_SatietyManager satietyManager;
    [SerializeField] private MC_StatisticsManager statisticsManager;
    [SerializeField] private MC_LevelManager levelManager;
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private Image healthBar, energyBar, manaBar, satietyBar, _XPBar;
    [SerializeField] private TMP_Text healthText, energyText, manaText, satietyText, _XPText, levelText;

    private void Update()
    { 
        healthBar.fillAmount = healthManager.Health / statisticsInfo.MaxHPValues[statisticsManager.HPLevel];
        energyBar.fillAmount = energyManager.Energy / statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel];
        manaBar.fillAmount = manaManager.Mana / (statisticsInfo.ÑloseCombatAdditionalManaValues[statisticsManager.CloseCombatLevel] + statisticsInfo.RangedCombatAdditionalManaValues[statisticsManager.RangedCombatLevel] + statisticsInfo.MagicCombatAdditionalManaValues[statisticsManager.MagicCombatLevel]);
        satietyBar.fillAmount = satietyManager.Satiety / statisticsInfo.SatietyMaxValue;
        healthText.text = $"{Mathf.Ceil(healthManager.Health)} / {Mathf.Ceil(statisticsInfo.MaxHPValues[statisticsManager.HPLevel])}";
        energyText.text = $"{Mathf.Ceil(energyManager.Energy)} / {Mathf.Ceil(statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel])}";
        manaText.text = $"{Mathf.Ceil(manaManager.Mana)} / {Mathf.Ceil(statisticsInfo.ÑloseCombatAdditionalManaValues[statisticsManager.CloseCombatLevel] + statisticsInfo.RangedCombatAdditionalManaValues[statisticsManager.RangedCombatLevel] + statisticsInfo.MagicCombatAdditionalManaValues[statisticsManager.MagicCombatLevel])}";
        satietyText.text = $"{(satietyManager.Satiety / statisticsInfo.SatietyMaxValue * 100).ToString("f0")}%";
        if (levelManager.Level != statisticsInfo.MaxLevel)
        {
            _XPBar.fillAmount = Mathf.Ceil(levelManager.XP) / Mathf.Ceil(statisticsInfo.NessaseryXPValuesToLevelUp[levelManager.Level]);
            _XPText.text = $"{Mathf.Ceil(levelManager.XP)} / {Mathf.Ceil(statisticsInfo.NessaseryXPValuesToLevelUp[levelManager.Level])}";
        }
        else
        {
            _XPBar.fillAmount = 1f;
            _XPText.text = "Level is max";
        }        
        if (levelManager.Level != statisticsInfo.MaxLevel)
        {
            levelText.text = $"Level {levelManager.Level + 1}";
        }
        else
        {
            levelText.text = $"Level {levelManager.Level + 1} (max)";
        }
    }
}