using UnityEngine;

public class MC_EnergyManager : MonoBehaviour
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
        Energy = currentGameStats.mainCharacterStats.energy;
    }

    public float Energy
    {
        get
        {
            return currentGameStats.mainCharacterStats.energy;
        }
        set
        {
            if (value <= 0)
            {
                currentGameStats.mainCharacterStats.energy = 0;
            }
            else if (value > statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel])
            {
                currentGameStats.mainCharacterStats.energy = statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel];
            }
            else
            {
                currentGameStats.mainCharacterStats.energy = value;
            }
        }
    }

    public void SpendEnergy(float value)
    {
        Energy -= value;
    }

    public void ReplenishEnergy(float value)
    {
        Energy += value;
    }
}