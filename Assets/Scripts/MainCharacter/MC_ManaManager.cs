using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_ManaManager : MonoBehaviour
{
    private float currentMana, maxMana;

    private void Awake()
    {
        // Set mana stats.
    }

    private float Mana
    {
        get
        {
            return currentMana;
        }
        set
        {
            if (value < 0)
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

    public void SpendMana(float value)
    {
        Mana -= value;
    }

    public void ReplenishMana(float value)
    {
        Mana += value;
    }
}