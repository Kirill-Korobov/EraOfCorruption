using UnityEngine;

public class MC_LevelManager : MonoBehaviour
{
    [SerializeField] private XPInfo _XPInfo;
    [SerializeField] private StatisticsInfo statisticsInfo;
    private int level, _XP;
    private MC_StatisticsManager statisticsManager;

    private void Awake()
    {
        // Set XP and level.
    }

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            if (value < 0)
            {
                level = 0;
            }
            else
            {
                level = value;
            }
        }
    }

    public int XP
    {
        get
        {
            return _XP;
        }
        set
        {
            if (value < 0)
            {
                _XP = 0;
            }
            else
            {
                _XP = value;
            }
            CheckForLevelUp();
        }
    }

    private void CheckForLevelUp()
    {
        if (XP >= _XPInfo.NessaseryXP[level - 1])
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        XP -= _XPInfo.NessaseryXP[level - 1];
        Level++;
        statisticsManager.StatisticPoints += statisticsInfo.StatisticPointsPerLevel;
    }
}