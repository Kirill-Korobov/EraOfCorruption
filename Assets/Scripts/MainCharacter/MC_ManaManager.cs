using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_ManaManager : MonoBehaviour
{
    private float currentMana, maxMana;

    private void Awake()
    {
        // Set mana stats.
        if (MaxMana == 0)
        {
            MaxMana = 1;
        }
    }

    public float CurrentMana
    {
        get
        {
            return currentMana;
        }
        set
        {
            if (value <= 0)
            {
                currentMana = 0;
            }
            else if (value > maxMana)
            {
                currentMana = maxMana;
            }
            else
            {
                currentMana = value;
            }
        }
    }

    public float MaxMana
    {
        get
        {
            return maxMana;
        }
        set
        {
            if (value > 0)
            {
                maxMana = value;
            }
        }
    }

    public void SpendMana(float value)
    {
        CurrentMana -= value;
    }

    public void ReplenishMana(float value)
    {
        CurrentMana += value * StaticEffects.hunger;
    }
}