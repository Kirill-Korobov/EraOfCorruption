using UnityEngine;

public class MC_ManaManager : MonoBehaviour
{
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private MC_StatisticsManager statisticsManager;
    [SerializeField] private GameStatsManager gameStatsManager;
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
        Mana = currentGameStats.mainCharacterStats.mana;
    }

    public float Mana
    {
        get
        {
            return currentGameStats.mainCharacterStats.mana;
        }
        set
        {
            if (value <= 0)
            {
                currentGameStats.mainCharacterStats.mana = 0;
            }
            else if (value > statisticsInfo.ÑloseCombatAdditionalManaValues[statisticsManager.CloseCombatLevel] + statisticsInfo.RangedCombatAdditionalManaValues[statisticsManager.RangedCombatLevel] + statisticsInfo.MagicCombatAdditionalManaValues[statisticsManager.MagicCombatLevel])
            {
                currentGameStats.mainCharacterStats.mana = statisticsInfo.ÑloseCombatAdditionalManaValues[statisticsManager.CloseCombatLevel] + statisticsInfo.RangedCombatAdditionalManaValues[statisticsManager.RangedCombatLevel] + statisticsInfo.MagicCombatAdditionalManaValues[statisticsManager.MagicCombatLevel];
            }
            else
            {
                currentGameStats.mainCharacterStats.mana = value;
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