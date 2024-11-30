using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameCanvasOperator : MonoBehaviour
{
    [SerializeField] private MC_HealthManager healthManager;
    [SerializeField] private MC_EnergyManager energyManager;
    [SerializeField] private MC_ManaManager manaManager;
    [SerializeField] private MC_LevelManager levelManager;
    [SerializeField] private XPInfo _XPInfo;
    [SerializeField] private Image healthBar, energyBar, manaBar, _XPBar;
    [SerializeField] private TMP_Text healthText, energyText, manaText, _XPText, levelText;

    private void Update()
    {
        levelManager.Level = 2;
        healthBar.fillAmount = healthManager.CurrentHealth / healthManager.MaxHealth;
        energyBar.fillAmount = energyManager.CurrentEnergy / energyManager.MaxEnergy;
        manaBar.fillAmount = manaManager.CurrentMana / manaManager.MaxMana;
        _XPBar.fillAmount = levelManager.XP / _XPInfo.NessaseryXP[levelManager.Level - 1];
        healthText.text = $"{Mathf.Ceil(healthManager.CurrentHealth)} / {Mathf.Ceil(healthManager.MaxHealth)}";
        energyText.text = $"{Mathf.Ceil(energyManager.CurrentEnergy)} / {Mathf.Ceil(energyManager.MaxEnergy)}";
        manaText.text = $"{Mathf.Ceil(manaManager.CurrentMana)} / {Mathf.Ceil(manaManager.MaxMana)}";
        _XPText.text = $"{Mathf.Ceil(levelManager.XP)} / {Mathf.Ceil(_XPInfo.NessaseryXP[levelManager.Level - 1])}";
        levelText.text = $"Level {levelManager.Level.ToString()}";
    }
}