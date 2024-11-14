using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_StatisticsManager : MonoBehaviour
{
    private int statisticsPoints, _HPLevel, energyLevel, movementLevel, _XPMultiplierLevel, closeCombatLevel, rangedCombatLevel, magicCombatLevel;

    private void Awake()
    {
        // Set statistics points stats.
    }

    public int StatisticsPoints
    {
        get
        {
            return statisticsPoints;
        }
        set
        {
            if (value < 0)
            {
                statisticsPoints = 0;
            }
            else
            {
                statisticsPoints = value;
            }
        }
    }

    public int HPLevel
    {
        get
        {
            return _HPLevel;
        }
        set
        {
            if (value < 0)
            {
                _HPLevel = 0;
            }
            else
            {
                _HPLevel = value;
            }
        }
    }

    public int EnergyLevel
    {
        get
        {
            return energyLevel;
        }
        set
        {
            if (value < 0)
            {
                energyLevel = 0;
            }
            else
            {
                energyLevel = value;
            }
        }
    }

    public int MovementLevel
    {
        get
        {
            return movementLevel;
        }
        set
        {
            if (value < 0)
            {
                movementLevel = 0;
            }
            else
            {
                movementLevel = value;
            }
        }
    }

    public int XPMultiplierLevel
    {
        get
        {
            return _XPMultiplierLevel;
        }
        set
        {
            if (value < 0)
            {
                _XPMultiplierLevel = 0;
            }
            else
            {
                _XPMultiplierLevel = value;
            }
        }
    }

    public int CloseCombatLevel
    {
        get
        {
            return closeCombatLevel;
        }
        set
        {
            if (value < 0)
            {
                closeCombatLevel = 0;
            }
            else
            {
                closeCombatLevel = value;
            }
        }
    }

    public int RangedCombatLevel
    {
        get
        {
            return rangedCombatLevel;
        }
        set
        {
            if (value < 0)
            {
                rangedCombatLevel = 0;
            }
            else
            {
                rangedCombatLevel = value;
            }
        }
    }

    public int MagicCombatLevel
    {
        get
        {
            return magicCombatLevel;
        }
        set
        {
            if (value < 0)
            {
                magicCombatLevel = 0;
            }
            else
            {
                magicCombatLevel = value;
            }
        }
    }

    public void LevelUpHP()
    {
        HPLevel += 1;
    }

    public void LevelUpEnergy()
    {
        EnergyLevel += 1;
    }

    public void LevelUpMovement()
    {
        MovementLevel += 1;
    }

    public void LevelUpXPMultiplier()
    {
        XPMultiplierLevel += 1;
    }

    public void LevelUpCloseCombat()
    {
        CloseCombatLevel += 1;
    }

    public void LevelUpRangedCombat()
    {
        RangedCombatLevel += 1;
    }

    public void LevelUpMagicCombat()
    {
        MagicCombatLevel += 1;
    }

    public void ResetStats()
    {
        statisticsPoints = _HPLevel + energyLevel + movementLevel + _XPMultiplierLevel + closeCombatLevel + rangedCombatLevel + magicCombatLevel;
        _HPLevel = 0;
        energyLevel = 0;
        movementLevel = 0;
        _XPMultiplierLevel = 0;
        closeCombatLevel = 0;
        rangedCombatLevel = 0;
        magicCombatLevel = 0;
    }
}