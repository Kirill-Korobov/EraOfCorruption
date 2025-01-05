using UnityEngine;

public class MC_EnergyManager : MonoBehaviour
{
    private float energy;
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private MC_StatisticsManager statisticsManager;

    private void Start()
    {
        if (statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel] == 0)
        {
            statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel] = 1;
        }
    }

    public float Energy
    {
        get
        {
            return energy;
        }
        set
        {
            if (value <= 0)
            {
                energy = 0;
            }
            else if (value > statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel])
            {
                energy = statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel];
            }
            else
            {
                energy = value;
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