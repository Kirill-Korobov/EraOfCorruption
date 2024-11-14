using UnityEngine;

public class MC_EnergyManager : MonoBehaviour
{
    private float currentEnergy, maxEnergy;

    private void Awake()
    {
        // Set energy stats.
    }

    private float Energy
    {
        get
        {
            return currentEnergy;
        }
        set
        {
            if (value < 0)
            {
                currentEnergy = 0;
            }
            else if (value > maxEnergy)
            {
                currentEnergy = maxEnergy;
            }
            else
            {
                currentEnergy = value;
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