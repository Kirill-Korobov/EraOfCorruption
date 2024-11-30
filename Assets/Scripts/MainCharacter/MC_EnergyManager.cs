using UnityEngine;

public class MC_EnergyManager : MonoBehaviour
{
    private float currentEnergy, maxEnergy;

    private void Awake()
    {
        // Set energy stats.
    }

    public float CurrentEnergy
    {
        get
        {
            return currentEnergy;
        }
        set
        {
            if (value <= 0)
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

    public float MaxEnergy
    {
        get
        {
            return maxEnergy;
        }
        set
        {
            if (value > 0)
            {
                maxEnergy = value;
            }
        }
    }

    public void SpendEnergy(float value)
    {
        CurrentEnergy -= value;
    }

    public void ReplenishEnergy(float value)
    {
        CurrentEnergy += value;
    }
}