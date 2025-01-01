using UnityEngine;

public class MC_ManaManager : MonoBehaviour
{
    private float mana;
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private MC_StatisticsManager statisticsManager;

    private void Awake()
    {
        // Set mana stats.
    }

    public float Mana
    {
        get
        {
            return mana;
        }
        set
        {
            if (value <= 0)
            {
                mana = 0;
            }
            else if (value > statisticsInfo.ÑloseCombatAdditionalManaValues[statisticsManager.CloseCombatLevel] + statisticsInfo.RangedCombatAdditionalManaValues[statisticsManager.RangedCombatLevel] + statisticsInfo.MagicCombatAdditionalManaValues[statisticsManager.MagicCombatLevel])
            {
                mana = statisticsInfo.ÑloseCombatAdditionalManaValues[statisticsManager.CloseCombatLevel] + statisticsInfo.RangedCombatAdditionalManaValues[statisticsManager.RangedCombatLevel] + statisticsInfo.MagicCombatAdditionalManaValues[statisticsManager.MagicCombatLevel];
            }
            else
            {
                mana = value;
            }
        }
    }

    public void SpendMana(float value)
    {
        Mana -= value;
    }

    public void ReplenishMana(float value)
    {
        Mana += value;
    }
}