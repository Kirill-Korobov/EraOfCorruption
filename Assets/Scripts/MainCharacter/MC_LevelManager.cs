using UnityEngine;

public class MC_LevelManager : MonoBehaviour
{
    [SerializeField] private XPInfo _XPInfo;
    private int level, currentXP;

    private void Awake()
    {
        // Set currentXP and level.
    }

    private int XP
    {
        get
        {
            return currentXP;
        }
        set
        {
            if (value < 0)
            {
                currentXP = 0;
            }
            else
            {
                currentXP = value;
            }
            CheckForLevelUp();
        }
    }

    private void CheckForLevelUp()
    {
        if (currentXP >= _XPInfo.NessaseryXP[level - 1])
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        XP -= _XPInfo.NessaseryXP[level - 1];
        level++;
        // Add statistics points.
    }
}