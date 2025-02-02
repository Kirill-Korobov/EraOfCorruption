using UnityEngine;

public class MC_StatisticsManager : MonoBehaviour
{
    [SerializeField] private GameStatsManager gameStatsManager;
    private GameStats currentGameStats;

    private void Awake()
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
        StatisticPoints = currentGameStats.mainCharacterStats.statisticPoints;
        HPLevel = currentGameStats.mainCharacterStats._HPLevel;
        EnergyLevel = currentGameStats.mainCharacterStats.energyLevel;
        MovementLevel = currentGameStats.mainCharacterStats.movementLevel;
        XPMultiplierLevel = currentGameStats.mainCharacterStats._XPMultiplierLevel;
        CloseCombatLevel = currentGameStats.mainCharacterStats.closeCombatLevel;
        RangedCombatLevel = currentGameStats.mainCharacterStats.rangedCombatLevel;
        MagicCombatLevel = currentGameStats.mainCharacterStats.magicCombatLevel;
    }

    public int StatisticPoints
    {
        get
        {
            return currentGameStats.mainCharacterStats.statisticPoints;
        }
        set
        {
            if (value < 0)
            {
                currentGameStats.mainCharacterStats.statisticPoints = 0;
            }
            else
            {
                currentGameStats.mainCharacterStats.statisticPoints = value;
            }
        }
    }

    public int HPLevel
    {
        get
        {
            return currentGameStats.mainCharacterStats._HPLevel;
        }
        set
        {
            if (value < 0)
            {
                currentGameStats.mainCharacterStats._HPLevel = 0;
            }
            else
            {
                currentGameStats.mainCharacterStats._HPLevel = value;
            }
        }
    }

    public int EnergyLevel
    {
        get
        {
            return currentGameStats.mainCharacterStats.energyLevel;
        }
        set
        {
            if (value < 0)
            {
                currentGameStats.mainCharacterStats.energyLevel = 0;
            }
            else
            {
                currentGameStats.mainCharacterStats.energyLevel = value;
            }
        }
    }

    public int MovementLevel
    {
        get
        {
            return currentGameStats.mainCharacterStats.movementLevel;
        }
        set
        {
            if (value < 0)
            {
                currentGameStats.mainCharacterStats.movementLevel = 0;
            }
            else
            {
                currentGameStats.mainCharacterStats.movementLevel = value;
            }
        }
    }

    public int XPMultiplierLevel
    {
        get
        {
            return currentGameStats.mainCharacterStats._XPMultiplierLevel;
        }
        set
        {
            if (value < 0)
            {
                currentGameStats.mainCharacterStats._XPMultiplierLevel = 0;
            }
            else
            {
                currentGameStats.mainCharacterStats._XPMultiplierLevel = value;
            }
        }
    }

    public int CloseCombatLevel
    {
        get
        {
            return currentGameStats.mainCharacterStats.closeCombatLevel;
        }
        set
        {
            if (value < 0)
            {
                currentGameStats.mainCharacterStats.closeCombatLevel = 0;
            }
            else
            {
                currentGameStats.mainCharacterStats.closeCombatLevel = value;
            }
        }
    }

    public int RangedCombatLevel
    {
        get
        {
            return currentGameStats.mainCharacterStats.rangedCombatLevel;
        }
        set
        {
            if (value < 0)
            {
                currentGameStats.mainCharacterStats.rangedCombatLevel = 0;
            }
            else
            {
                currentGameStats.mainCharacterStats.rangedCombatLevel = value;
            }
        }
    }

    public int MagicCombatLevel
    {
        get
        {
            return currentGameStats.mainCharacterStats.magicCombatLevel;
        }
        set
        {
            if (value < 0)
            {
                currentGameStats.mainCharacterStats.magicCombatLevel = 0;
            }
            else
            {
                currentGameStats.mainCharacterStats.magicCombatLevel = value;
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
        StatisticPoints += HPLevel + EnergyLevel + MovementLevel + XPMultiplierLevel + CloseCombatLevel + RangedCombatLevel + MagicCombatLevel;
        HPLevel = 0;
        EnergyLevel = 0;
        MovementLevel = 0;
        XPMultiplierLevel = 0;
        CloseCombatLevel = 0;
        RangedCombatLevel = 0;
        MagicCombatLevel = 0;
    }
}